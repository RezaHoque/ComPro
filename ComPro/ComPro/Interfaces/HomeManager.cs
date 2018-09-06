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

        public HomeManager()
        {
            _data=new ApplicationDbContext();
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

        public IEnumerable<NoticeBoard> LatestNotice(int length)
        {
            // ApplicationDbContext _data = new ApplicationDbContext();
            // List<ChatModel> LatestNotice = new List<ChatModel>();
            List<NoticeBoard> LatestNotice = new List<NoticeBoard>();

            try
            {

                var PinUpNotice = _data.Notice.FirstOrDefault(x => x.IsApproved == true && x.PinUp==true);

                if(PinUpNotice==null)
                {
                    length = length + 1;
                }
                else
                {
                    LatestNotice.Add(PinUpNotice);
                }
                var NewNotice = _data.Notice.Where(x => x.IsApproved).OrderByDescending(x => x.SubmitDate).Take(length);

                LatestNotice.AddRange(NewNotice);

                return LatestNotice.OrderByDescending(x=>x.PinUp);
            }


            catch (Exception ex)
            {
                //return LatestNotice;

                //log the exception.
                return LatestNotice;
            }
        }

        public IEnumerable<ChatModel> LatestEvent(int length)
        {
            //ApplicationDbContext _data = new ApplicationDbContext();
            List<ChatModel> LatestEvent = new List<ChatModel>();
            string Current_User_id = HttpContext.Current.User.Identity.GetUserId();
            try
            {

                List<EventViewModel> Result = new List<EventViewModel>();
                List<EventModel> events = new List<EventModel>();

               
                    var AllEvent = _data.Event.Where(x => x.EventStatus && x.Date >= DateTime.Now && x.IsApproved);


                    //    foreach (var item in AllEvent)
                    //    {
                    //        if (DateTime.Now > item.End)
                    //        {
                    //            item.EventStatus = false;

                    //        }
                    //    _data.Entry(item).State = EntityState.Modified;

                    //    }

                    //_data.SaveChanges();



                    if (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString()))
                    {

                        foreach (var item in AllEvent)
                        {
                            events.Add(item);

                        }

                        
                    }
                    else
                    {
                        foreach (var item in AllEvent)
                        {
                            if (item.IsPublic || item.CreatorId == Current_User_id)
                            {
                                events.Add(item);
                            }
                            else
                            {
                                if (_data.EventMember.Any(x => x.EventId == item.EventId && x.MemberID == Current_User_id) || HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString()))
                                {
                                    events.Add(item);
                                }
                            }
                        }



                    }

                    

                    //var AllEvent = _data.Event.Where(x => x.EventStatus == true);

                    //foreach (var item in AllEvent)
                    //{
                    //    if (DateTime.Now < item.Creation)
                    //    {
                    //        item.EventStatus = false;

                    //    }
                    //}
                    //_data.SaveChanges();



                    //LatestEvent = _data.Event.Where(a => a.EventStatus == true && a.IsApproved == true).OrderByDescending(i => i.ApprovalDate)
                    //                              .Take(length).AsEnumerable().Select(p => new ChatModel
                    //                              {
                    //                                  PartnerName = p.Title,
                    //                                  PartnerId = p.EventId.ToString()
                    //                              }).ToList();

                    LatestEvent = events.OrderByDescending(x=>x.ApprovalDate).Take(length).AsEnumerable().Select(p => new ChatModel
                                              {
                                                  PartnerName = p.Title,
                                                  PartnerId = p.EventId.ToString()
                     }).ToList();


                    return LatestEvent;
            }


                catch
                {
                    return LatestEvent;
                }
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