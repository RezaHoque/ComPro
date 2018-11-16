using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComPro.Models;
using Microsoft.AspNet.Identity;
using static ComPro.Models.Enums;

namespace ComPro.Interfaces
{
    public class MeetingsManager : IMeetings
    {
        ApplicationDbContext _data = new ApplicationDbContext();

        public List<MeetingViewModel> AllMeetingss()
        {
            List<MeetingViewModel> Result = new List<MeetingViewModel>();

            try
            {
                Result = _data.Meetings_Models.Select(item => new MeetingViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Creator_Id = item.Creator_Id,
                    Meeting_Date = item.Meeting_Date,
                }).OrderBy(b=>b.Meeting_Date).ToList();

                return Result;
            }

            catch
            {
                return Result;
            }
        }


       public MeetingDetailsModel Meeting(int id)
       {
            MeetingDetailsModel Result = new MeetingDetailsModel();

            try
            {
                var Meeting = _data.Meetings_Models.FirstOrDefault(a => a.Id ==id);

                Result.Id = Meeting.Id;
                Result.Title = Meeting.Title;
                Result.Creation_Date = Meeting.Creation_Date;
                Result.Creator_Name = Meeting.Creator_Name;
                Result.Meeting_Date = Meeting.Meeting_Date;
                Result.Description = Meeting.Description;
                Result.Perticipents_Name = Meeting.Perticipents_Name;
               
                return Result;
            }

            catch
            {
                return Result;
            }
        }

      public void Meeting_Information(Meetings_Models model)
        {           
                model.Creation_Date = DateTime.Now.Date;
                model.Creator_Id = HttpContext.Current.User.Identity.GetUserId();
                model.Creator_Name= _data.UserInfo.FirstOrDefault(x => x.ApprovalDate != null && x.UserId== model.Creator_Id).Name;
                model.Perticipents_Name = null;

            if (model.Perticipents_Id!=null)
            {
                var invitees = model.Perticipents_Id.Split(',').ToList();

                     foreach (var i in invitees)
                     {
                       string name = _data.UserInfo.FirstOrDefault(x => x.UserId == i).Name;
                       if (model.Perticipents_Name!= null)
                       {
                            model.Perticipents_Name = model.Perticipents_Name +','+ name;
                       }
                       else
                        model.Perticipents_Name = name;

                     }
            }
           


            _data.Meetings_Models.Add(model);
                _data.SaveChanges();
                                   
        }

        public MeetingEditModel GetMeeting(int id)
        {
            MeetingEditModel Result = new MeetingEditModel();
            try
            {
                var Meeting = _data.Meetings_Models.FirstOrDefault(a => a.Id == id);
                if ((@HttpContext.Current.User.Identity.GetUserId() == Meeting.Creator_Id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                    Result.Id = Meeting.Id;
                    Result.Meeting_Date = Meeting.Meeting_Date;
                    Result.Title = Meeting.Title;
                    Result.Description = Meeting.Description;
                }

                return Result;
            }

            catch
            {
                return Result;
            }
        }

        public void PostMeeting(MeetingEditModel model)
        {
            var Meeting = _data.Meetings_Models.FirstOrDefault(a => a.Id == model.Id);

            if ((@HttpContext.Current.User.Identity.GetUserId() == Meeting.Creator_Id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
            {
                Meeting.Meeting_Date = model.Meeting_Date;
                Meeting.Title = model.Title;
                Meeting.Description = model.Description;

                _data.SaveChanges();
            }

        }


        public void RemoveMeeting(int id)
        {
            var Meeting = _data.Meetings_Models.FirstOrDefault(a => a.Id == id);
            if ((@HttpContext.Current.User.Identity.GetUserId() == Meeting.Creator_Id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
            {
                _data.Meetings_Models.Remove(Meeting);
            }
        }
    }
}