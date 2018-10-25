using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using ComPro.Helpers;
using ComPro.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using static ComPro.Models.Enums;


namespace ComPro.Interfaces
{
    public class NoticeBoardManager : INoticeBoard
    {
        private readonly ApplicationDbContext _data;
        string Current_User_id = HttpContext.Current.User.Identity.GetUserId();
        public NoticeBoardManager()
        {
            _data = new ApplicationDbContext();
        }
        public SiteImage GetNoticeImage(int id,string type)
        {
            var image = _data.SiteImages.FirstOrDefault(x => x.TypeId == id && x.Type==type);
            return image;
        }
        public IEnumerable<NoticeBoard> GetApprovedNotices()
        {
            try
            {
                
                
                return _data.Notice.Where(x => x.IsApproved == true).ToList();

            }

            catch
            {
                throw;

            }
        }


        public List<NoticeBoardViewModel> GetMyNotice()
        {
            try
            { var MynoticeList = new List<NoticeBoardViewModel>();

               var Mynotice=  _data.Notice.Where(x => x.IsApproved == true&& x.CreatorId== Current_User_id);
               
                foreach (var n in Mynotice)
                {
                    var totalComnts = GetComments(n.Id).Count();
                    MynoticeList.Add(new NoticeBoardViewModel
                    {
                        Notice = n,
                        TotalComments = totalComnts,
                        NoticeImage = GetNoticeImage(n.Id, "Notice")
                    });
                }

                return MynoticeList.OrderByDescending(y => y.Notice.SubmitDate).ToList();
            }

            catch
            {
                throw;
            }
        }
       

        public NoticeBoardViewModel GetDetails(string uniqueUrl)
        {
            if (!string.IsNullOrEmpty(uniqueUrl))
            {
                var noticeDetails = _data.Notice.FirstOrDefault(x => x.UniqueUrl == uniqueUrl && x.IsApproved);
                if (noticeDetails != null)
                {
                    var nvm = new NoticeBoardViewModel();
                    nvm.Notice = noticeDetails;

                    nvm.NoticeImage = _data.SiteImages.FirstOrDefault(x => x.TypeId == noticeDetails.Id && x.Type == "Notice");


                    return nvm;
                }
            }
          
            return new NoticeBoardViewModel();
        }

        public NoticeBoard PostNotices(NoticeBoard model)
        {
            try
            {
                
                model.IsApproved = true;
                model.SubmitDate = DateTime.Now;
                model.CreatorId = model.CreatorId;
                model.ActionDate = DateTime.Now;
                model.Description = model.Description.Replace(System.Environment.NewLine, "<br/>");

                if (model.PinUp)
                {
                    var PreviousPinUpNotice = _data.Notice.FirstOrDefault(x => (x.PinUp == true));
                    if (PreviousPinUpNotice != null)
                    {
                        PreviousPinUpNotice.PinUp = false;
                        _data.Entry(PreviousPinUpNotice).State = EntityState.Modified;
                    }

                }
               

                _data.Notice.Add(model);
                _data.SaveChanges();

                model.UniqueUrl= $"{model.Id}-{model.Title.Replace(" ", "-")}";
                _data.SaveChanges();

                return model;
            }

            catch
            {
                throw;
            }
        }

        // Currently Admin is free to approve new notice but we will impliment it very soon.

        //public void ApproveNotice(int id)
        //{
        //    var notice = _data.Notice.FirstOrDefault(x => x.Id == id);
        //    notice.IsApproved = true;

        //    _data.Entry(notice).State = EntityState.Modified;
        //    _data.SaveChanges();

        //}

        public bool PostComment(PublicComment comment)
        {
            try
            {

                _data.PublicComments.Add(comment);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Have to log the error.
                return false;
            }

        }

        public ICollection<CommentViewModel> GetComments(int noticeId)
        {
            var comments = _data.PublicComments.Where(x => x.IsBlocked == false && x.NoticeId == noticeId).ToList().OrderByDescending(x=>x.CommentDateTime);
            var commentList = new List<CommentViewModel>();
            foreach (var pc in comments)
            {
                var comment=new CommentViewModel();
                comment.Comment = pc;

                var userinfo=_data.UserInfo.FirstOrDefault(x => x.UserId == pc.CommentUserId);
                comment.CommentUser = userinfo;

                commentList.Add(comment);
            }

            return commentList;
        }

        public NoticeBoardViewModel GetEdit(int id)
        {
            NoticeBoard notice = new NoticeBoard();
            NoticeBoardViewModel noticeVMList = new NoticeBoardViewModel();
            try
            {
               var  notice2 = _data.Notice.FirstOrDefault(x => (x.Id == id));
                if ( (notice2.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {

                    noticeVMList.Notice = notice2;
                    noticeVMList.NoticeImage = GetNoticeImage(notice2.Id, "Notice");
                   
                    
                }
                return noticeVMList;
                    
            }
            catch
            {
                return noticeVMList;
            }
        }

        public string PostEdit(NoticeBoardViewModel model)
        {
            try
            {
                var notice = _data.Notice.FirstOrDefault(x => (x.Id == model.Notice.Id));

                if ((notice.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                    notice.Title = model.Notice.Title;
                    notice.Description = model.Notice.Description;
                    notice.WebLink = model.Notice.WebLink;
                    //notice.ActionDate = model.ActionDate;

                    if (notice.PinUp == false && model.Notice.PinUp== true)
                    {
                        var PreviousPinUpNotice = _data.Notice.FirstOrDefault(x => (x.PinUp == true));
                        if (PreviousPinUpNotice!=null)
                        {
                        PreviousPinUpNotice.PinUp = false;
                        _data.Entry(PreviousPinUpNotice).State = EntityState.Modified;
                        }
                       
                    }
                    notice.PinUp = model.Notice.PinUp;

                    

                    _data.Entry(notice).State = EntityState.Modified;
                    _data.SaveChanges();
                    return Helpers.Constants.PostEdit;
                }
                else
                {
                    return Helpers.Constants.PostEditFail;
                }
            }
            catch
            {
                return Helpers.Constants.PostEditFail;
            }

        }

        public bool DeleteNotice(int id)
        {

            NoticeBoard notice = new NoticeBoard();
            try
            { notice = _data.Notice.FirstOrDefault(x => (x.Id == id));
                if ((notice.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                      var comment = _data.PublicComments.Where(x => x.NoticeId == id);

                    _data.PublicComments.RemoveRange(comment);
                    _data.Notice.Remove(notice);

                   
                    _data.SaveChanges();
                   

                    return true;
                }
                else
                {
                    return false;
                }
                
                }
               
           
            catch
            {
               return false;
            }
        }

        public bool SaveImage(SiteImage image)
        {
            try
            {
                _data.SiteImages.Add(image);
                _data.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                throw;
            }
            
        }
    }
}