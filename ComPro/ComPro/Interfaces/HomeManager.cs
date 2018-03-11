using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComPro.Helpers;
using ComPro.Interfaces;
using ComPro.Models;
using Microsoft.Ajax.Utilities;

namespace ComPro.Interfaces
{
    public class HomeManager : IHome
    {
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
    }
}