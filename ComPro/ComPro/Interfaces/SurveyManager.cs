using ComPro.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ComPro.Interfaces
{
    public class SurveyManager : ISurvey
    {
        string Current_User_id = HttpContext.Current.User.Identity.GetUserId();

        ApplicationDbContext _data = new ApplicationDbContext();

        public bool CreatePoll(PollViewModel model, List<string> inviteesIds)
        {

            try
            {

                var start = model.StartDate.Date.ToString();
                var end = model.EndDate.Date.ToString();

                PollingAndSyrvayModel Data = new PollingAndSyrvayModel
                {
                    Name = "Poll",
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = DateTime.ParseExact(start, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture), // String to datetime
                    EndDate = DateTime.ParseExact(end, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture), // String to datetime
                    Creation = DateTime.Now.Date,
                    CreatorId = Current_User_id,

                    Status = true,
                    IsApproved = true,
                    ApprovalDate = DateTime.Now.Date,
                    IsPublic = inviteesIds.Any() ? false : true

                };


                _data.PollingAndSyrvays.Add(Data);
                _data.SaveChanges();


                QuestionModel qus = new QuestionModel
                {
                    ActivityName = "Poll",
                    ActivityId = Data.Id,
                    Question = model.Question

                };

                _data.Questions.Add(qus);
                _data.SaveChanges();


                List<AnswerModel> AnswerList = new List<AnswerModel>();
                AnswerModel answer = new AnswerModel
                {
                    QuestionId = qus.Id,
                    Answer = "Yes"

                };


                AnswerList.Add(answer);

                AnswerModel answer2 = new AnswerModel
                {
                    QuestionId = qus.Id,
                    Answer = "No"

                };

                AnswerList.Add(answer2);
                _data.Answers.AddRange(AnswerList);
                _data.SaveChanges();


                if (Data.IsPublic == false)
                {
                    model.Id = Data.Id;
                    Perticipents(model, inviteesIds);
                }

                return true;
            }

            catch (Exception ex)
            {

                Email_Service_Model email = new Email_Service_Model
                {
                    ToEmail = System.Configuration.ConfigurationManager.AppSettings["BccEmail"],
                    EmailSubject = "Failed to create notice.",
                    EMailBody = $"Description: {model.Description}. Title: {model.Title}. Exception: {ex.ToString()}"
                };

                var emailmanager = new UtilityManager();
                emailmanager.SendEmail(email);
                return false;
            }
        }



        public List<IndexViewModel2> AllPoll()
        {
            List<IndexViewModel2> POlls = new List<IndexViewModel2>();


            try
            {
                var AllPoll = _data.PollingAndSyrvays.Where(x => x.Name == "Poll").ToList();

                foreach (var item in AllPoll)
                {
                    IndexViewModel2 singlePoll = new IndexViewModel2
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Title = item.Title
                    };
                    POlls.Add(singlePoll);



                }
                return POlls;
            }

            catch
            {
                return POlls;
            }
        }

        public void Perticipents(PollViewModel Data, List<string> inviteesIds)
        {
            try
            {

                PerticipentModel member = new PerticipentModel();
                List<PerticipentModel> MemberList = new List<PerticipentModel>();


                foreach (var id in inviteesIds)
                {


                    MemberList.Add(new PerticipentModel()
                    {

                        //  ActivityId=Data.Id,
                        PerticipentId = id,
                        // AnswerId = 0

                    });

                }
                _data.Perticipents.AddRange(MemberList);
                _data.SaveChanges();


            }

            catch (Exception ex)
            {
                throw;
            }
        }


        public PollViewModel SinglePoll(int id)
        {
            PollViewModel poll = new PollViewModel();
            try
            {
                var y = _data.PollingAndSyrvays.FirstOrDefault(x => x.Id == id);
                var z = _data.Questions.FirstOrDefault(x => x.ActivityId == y.Id && x.ActivityName == "Poll");

                poll.Id = id;
                poll.Title = y.Title;
                poll.Description = y.Description;
                poll.Question = z.Question;
                poll.StartDate = y.StartDate;
                poll.EndDate = y.EndDate;

                return poll;
            }

            catch
            {
                return poll;
            }
        }

        public bool cust(string vote, int Id)
        {
            try
            {
                var P = _data.Perticipents.FirstOrDefault(y => y.ActivityId == Id && y.PerticipentId == Current_User_id);
                var poll = _data.PollingAndSyrvays.FirstOrDefault(x => x.Id == Id);
                var qus = _data.Questions.FirstOrDefault(y => y.ActivityId == poll.Id);
                var ans = _data.Answers.First(z => z.QuestionId == qus.Id && z.Answer == vote);

                P.AnswerId = ans.Id;

                _data.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }
        }

    }
}