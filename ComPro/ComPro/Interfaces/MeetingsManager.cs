using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComPro.Models;
using Microsoft.AspNet.Identity;

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
           

            try
            {
                model.Creation_Date = DateTime.Now.Date;
                model.Creator_Id = HttpContext.Current.User.Identity.GetUserId();
                model.Creator_Name= _data.UserInfo.FirstOrDefault(x => x.ApprovalDate != null && x.UserId== model.Creator_Id).Name;

                _data.Meetings_Models.Add(model);
                _data.SaveChanges();

               
            }

            catch
            {
               
            }
        }

    }
}