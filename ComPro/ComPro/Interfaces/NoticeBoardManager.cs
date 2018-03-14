using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
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
        public SiteImage GetNoticeImage(int id)
        {
            var image = _data.SiteImages.FirstOrDefault(x => x.TypeId == id);
            return image;
        }
        public IEnumerable<NoticeBoard> GetApprovedNotices()
        {
            try
            {
                
                
                return _data.Notice.Where(x => x.IsApproved == true);

            }

            catch
            {
                throw;

            }
        }

        public IEnumerable<NoticeBoard> GetNewNotices()
        {
            try
            {
                
                 
                
                IEnumerable<NoticeBoard> Notice; 
                if (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString()))
                {
                   
                    return _data.Notice.Where(x => x.IsApproved == false);
                }
                else
                 Notice = _data.Notice.Where(x=>x.IsApproved==true);
                return Notice.OrderByDescending(x=>x.SubmitDate);

            }

            catch
            {
                throw;
                
                

            }
        }

        public NoticeBoard GetDetails(int id)
        {
            var noticeDetails = _data.Notice.FirstOrDefault(x => x.Id == id && x.IsApproved);
            if (noticeDetails != null)
            {
                return noticeDetails;
            }
            return new NoticeBoard();
        }

        public NoticeBoard PostNotices(NoticeBoard model)
        {
            try
            {
                
                model.IsApproved = true;
                model.SubmitDate = DateTime.Now;
                model.CreatorId = model.CreatorId;
                model.ActionDate = DateTime.Now;

                _data.Notice.Add(model);
                _data.SaveChanges();

                return model;
            }

            catch
            {
                throw;
            }
        }

        public void ApproveNotice(int id)
        {
            var notice = _data.Notice.FirstOrDefault(x => x.Id == id);
            notice.IsApproved = true;

            _data.Entry(notice).State = EntityState.Modified;
            _data.SaveChanges();

        }

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

        public ICollection<PublicComment> GetComments(int noticeId)
        {
            var comments = _data.PublicComments.Where(x => x.IsBlocked == false && x.NoticeId == noticeId);
            return comments.OrderByDescending(x => x.CommentDateTime).ToList();
        }

        public NoticeBoard GetEdit(int id)
        {
            NoticeBoard notice = new NoticeBoard();
            try
            {
               var  notice2 = _data.Notice.FirstOrDefault(x => (x.Id == id));
                if ( (notice2.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                    notice = notice2;
                }
                return notice;
                    
            }
            catch
            {
                return notice;
            }
        }

        public string PostEdit(NoticeBoard model)
        {
            try
            {
                var notice = _data.Notice.FirstOrDefault(x => (x.Id == model.Id));

                if ((notice.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                    notice.Title = model.Title;
                    notice.Description = model.Description;
                    notice.WebLink = model.WebLink;
                    notice.ActionDate = model.ActionDate;

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

        public string PostDelete(int id)
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

                    return Helpers.Constants.Delete;
                }
                else
                {
                    return Helpers.Constants.DeleteFail;
                }
                
                }
               
           
            catch
            {
                return Helpers.Constants.DeleteFail;
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