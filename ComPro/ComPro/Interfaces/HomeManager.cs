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

namespace ComPro.Interfaces
{
    public class HomeManager : IHome
    {
        Email_Service_Model obj = new Email_Service_Model();
        IUtility _utility = new UtilityManager();

        public IEnumerable<ChatModel> LatestMember(int length)
        {
            ApplicationDbContext _data = new ApplicationDbContext();
            List<ChatModel> LatestMember = new List<ChatModel>();

          
            try
            {
                IUserProfile _userProfile = new UserProfileManager();
                
                 LatestMember = _userProfile.AllUser().OrderByDescending(i => i.ApprovalDate)
                                               .Take(length).AsEnumerable().Select(p => new ChatModel
                                               {
                                                   PartnerName = p.Name,
                                                   PartnerId = p.Id.ToString()
                                               }).ToList();

                
               
                return LatestMember;
            }
            

            catch
            {
                return LatestMember;
            }
        }

        public IEnumerable<ChatModel> LatestNotice(int length)
        {
            ApplicationDbContext _data = new ApplicationDbContext();
            List<ChatModel> LatestNotice = new List<ChatModel>();
            try
            {


                LatestNotice = _data.Notice.Where(x => x.IsApproved == true).OrderByDescending(i => i.SubmitDate)
                                              .Take(length).AsEnumerable().Select(p => new ChatModel
                                              {
                                                  PartnerName = p.Title,
                                                  PartnerId = p.Id.ToString()
                                              }).ToList();



                return LatestNotice;
            }


            catch
            {
                return LatestNotice;
            }
        }

        public IEnumerable<ChatModel> LatestEvent(int length)
        {
            ApplicationDbContext _data = new ApplicationDbContext();
            List<ChatModel> LatestEvent = new List<ChatModel>();
            try
            {
                var AllEvent = _data.Event.Where(x => x.EventStatus == true);

                foreach (var item in AllEvent)
                {
                    if (DateTime.Now < item.Creation)
                    {
                        item.EventStatus = false;

                    }
                }
                _data.SaveChanges();



                LatestEvent = _data.Event.Where(a => a.EventStatus == true && a.IsApproved == true).OrderByDescending(i => i.ApprovalDate)
                                              .Take(length).AsEnumerable().Select(p => new ChatModel
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


                obj.ToEmail = System.Configuration.ConfigurationManager.AppSettings["From"];
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
    }
}