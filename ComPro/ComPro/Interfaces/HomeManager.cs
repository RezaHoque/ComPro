using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using ComPro.Helpers;
using ComPro.Interfaces;
using ComPro.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using static ComPro.Models.Enums;

namespace ComPro.Interfaces
{
    public class HomeManager : IHome
    {
        Email_Service_Model obj = new Email_Service_Model();
        IUtility _utility = new UtilityManager();
        private readonly ApplicationDbContext _data;
        private NoticeBoardManager _noticeManager;
        public HomeManager()
        {
            _data=new ApplicationDbContext();
            _noticeManager=new NoticeBoardManager();

        }

        public IEnumerable<UserInfo> LatestMember(int length)
        {
           // ApplicationDbContext _data = new ApplicationDbContext();
            List<UserInfo> LatestMembers = new List<UserInfo>();

          
            try
            {
                IUserProfile _userProfile = new UserProfileManager();
                
                LatestMembers = _userProfile.AllUser().OrderByDescending(i => i.ApprovalDate)
                                               .Take(length).ToList();


                
               
                return LatestMembers;
            }
            

            catch
            {
                return LatestMembers;
            }
        }

        public IEnumerable<NoticeBoardViewModel> LatestNotice(int length)
        {
            List<NoticeBoardViewModel> noticeList = new List<NoticeBoardViewModel>();

            try
            {

                var orderedNotices = _data.Notice.Where(x => x.IsApproved).OrderBy(x => x.PinUp)
                    .ThenByDescending(x => x.SubmitDate).Take(length);


                foreach (var n in orderedNotices)
                {
                    var nvm=new NoticeBoardViewModel();
                    var noticeBoard=new NoticeBoard();
                    noticeBoard.UniqueUrl = n.UniqueUrl;
                    noticeBoard.SubmitDate = n.SubmitDate;
                    noticeBoard.Description = n.Description;
                    noticeBoard.Title = n.Title;


                    nvm.Notice = noticeBoard;
                    nvm.TotalComments=_noticeManager.GetComments(n.Id).Count();
                    nvm.NoticeImage = _noticeManager.GetNoticeImage(n.Id, "Notice");
                    nvm.PostedBy= n.Creator.UserName.UserName();

                    noticeList.Add(nvm);
                }

            }
            catch (Exception ex)
            {
                //log the exception.
              
            }

            return noticeList;
        }

        public IEnumerable<EventViewModel> LatestEvent(int length)
        {
            List<EventViewModel> latestEvents = new List<EventViewModel>();

            try
            {
                var events = _data.Event.Where(x => x.Date >= DateTime.Now && x.IsApproved && x.IsPublic).OrderByDescending(x=>x.Date).Take(length);
                foreach (var e in events)
                {
                    var evm=new EventViewModel();
                    evm.UniqueUrl = e.UniqueUrl;
                    evm.EventTitel = e.Title;
                    evm.Description = e.Description;
                    evm.EventDate = e.Date;
                    evm.Images = _data.SiteImages.Where(x => x.Type == "Event" && x.TypeId == e.EventId).ToList();

                    latestEvents.Add(evm);

                }
            }
            catch
            {
               
            }

            return latestEvents;
        }


        public bool Contac_Admin(FormCollection Message)
        {
            try
            {
                User_Feedback_Model feedback = new User_Feedback_Model
                {
                    Name = Message["Name"],
                    Phone= Int32.Parse(Message["Phone"]),
                    Email= Message["Email"],
                    Message= Message["Message"],
                    Status= false,
                    SubmitDate= DateTime.Now,
                };

                ApplicationDbContext _data = new ApplicationDbContext();
                _data.User_Feedback.Add(feedback);
                _data.SaveChanges();


                obj.ToEmail = System.Configuration.ConfigurationManager.AppSettings["Admin"];
                obj.EmailSubject = Helpers.Constants.User_Feedback;

                obj.EMailBody = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Email_Templets/") + "User_Feedback" + ".cshtml").Replace("UserName", Message["Name"]).Replace("UserPhone", Message["Phone"]).Replace("UserEmail", Message["Email"]).Replace("UserMessage", Message["Message"]).ToString();

                var result = _utility.SendEmail(obj);
                return true;
            }

            catch
            {
                return false;
            }
        }

        public IEnumerable<SiteContibuter> GetContributers()
        {
            var contributers = _data.SiteContibuters.ToList();
            foreach (var c in contributers)
            {
                c.PhotoPath = c.PhotoPath ?? "/Content/images/Profile/male.png";
            }
            return contributers;
        }
    }
}