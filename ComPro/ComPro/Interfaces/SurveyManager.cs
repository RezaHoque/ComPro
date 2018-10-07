﻿using ComPro.Models;
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
                var AllPoll = _data.PollingAndSyrvays.ToList();

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

                        ActivityId=Data.Id,
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
                var y = _data.PollingAndSyrvays.FirstOrDefault(x => x.Id == id && x.Name == "Poll");
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
                var poll = _data.PollingAndSyrvays.FirstOrDefault(x => x.Id == Id);
                var qus = _data.Questions.FirstOrDefault(y => y.ActivityId == poll.Id);
                var ans = _data.Answers.First(z => z.QuestionId == qus.Id && z.Answer == vote);

                if (poll.IsPublic)
                {
                    PerticipentModel member = new PerticipentModel
                    {
                        ActivityId = Id,
                        PerticipentId=Current_User_id,
                        AnswerId=ans.Id
                    };
                    _data.Perticipents.Add(member);
                }
                else
                {
                var P = _data.Perticipents.FirstOrDefault(y => y.ActivityId == Id && y.PerticipentId == Current_User_id);
               
                P.AnswerId = ans.Id;

                }

                _data.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }
        }


       public bool CreateSurvey(PollViewModel model, string[] Questions)
        {
            try
            {
                Array.Reverse(Questions);

                var start = model.StartDate.Date.ToString();
                var end = model.EndDate.Date.ToString();

                PollingAndSyrvayModel Data = new PollingAndSyrvayModel
                {
                    Name = "Survey",
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = DateTime.ParseExact(start, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture), // String to datetime
                    EndDate = DateTime.ParseExact(end, "dd-MM-yyyy hh:mm:ss", CultureInfo.InvariantCulture), // String to datetime
                    Creation = DateTime.Now.Date,
                    CreatorId = Current_User_id,

                    Status = true,
                    IsApproved = true,
                    ApprovalDate = DateTime.Now.Date,
                    IsPublic = true,

                };


                _data.PollingAndSyrvays.Add(Data);
                _data.SaveChanges();

                List<QuestionModel> QuestionList = new List<QuestionModel>();
                foreach (var i in Questions)
                {
                    if(i!="")
                    {
                        

                        if (Char.IsDigit(i[0]) && i[i.Length - 1]=='?')
                        {
                            QuestionModel qus = new QuestionModel
                            {
                                ActivityName = "Survey",
                                ActivityId = Data.Id,
                                Question = i.Remove(i.Length - 1),

                            };

                            QuestionList.Add(qus);
                        }
                        
                    }
                   
                    
                }

               

                _data.Questions.AddRange(QuestionList);
                _data.SaveChanges();


                List<AnswerModel> AnswerList = new List<AnswerModel>();
                int j = 0;
                foreach(var z in QuestionList)
                {
                    
                   
                        for (int i =j; i<Questions.Length;)
                    {
                        if (Questions[i] != "" && Questions[i].Remove(Questions[i].Length - 1) == z.Question)
                        {
                            i++;
                            if (Questions[i] != "")
                            {
                                var x = Questions[i][0];
                                bool x1 = Char.IsDigit(Questions[i][0]);

                                var y1 = Questions[i][Questions[i].Length - 1];
                                bool y11 = Questions[i][Questions[i].Length - 1] == '?';


                                while ((Questions[i] != "") && ((!Char.IsDigit(Questions[i][0])) || !(Questions[i][Questions[i].Length - 1] == '?')))
                                {
                                    AnswerModel answer = new AnswerModel
                                    {
                                        QuestionId = z.Id,
                                        Answer = Questions[i]

                                    };
                                    AnswerList.Add(answer);
                                    i++;
                                    if (i >= Questions.Length)
                                    { break; }


                                }
                                j = i;
                                break;
                            }
                            else
                            {i++;
                            if (i >= Questions.Length)
                            { break; }
                            } 


                        }
                          else
                           {
                            i++;
                            if (i >= Questions.Length)
                            { break; }
                        }

                    }


                }
                
                
                _data.Answers.AddRange(AnswerList);
                _data.SaveChanges();


               

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


        public SurveyViewModel SingleSurvey(int id)
        {
            SurveyViewModel Survey = new SurveyViewModel();
           
            List<QA> QusAnsList = new List<QA>();
            try
            {
                var y = _data.PollingAndSyrvays.FirstOrDefault(x => x.Id == id && x.Name=="Survey");

                var questions = _data.Questions.Where(x => x.ActivityId == y.Id && x.ActivityName == "Survey").ToList();

                Survey.Id = id;
                Survey.Title = y.Title;
                Survey.Description = y.Description;

                foreach (var q in questions)
                {
                    QA QueAns = new QA
                    {
                        Id= q.Id,
                        Type = "Question",
                        Q_A = q.Question
                    };

                    QusAnsList.Add(QueAns);

                    var ans = _data.Answers.Where(P => P.QuestionId == q.Id).Select(x=> new { x.Id, x.Answer}).ToList();  


                    foreach (var a in ans)
                    {
                        QA QueAns2 = new QA
                        {   Id= a.Id,
                            Type = "Answer",
                            Q_A = a.Answer
                        };

                        QusAnsList.Add(QueAns2);
                    }
                    

                }
                Survey.QA = QusAnsList;
               
                Survey.StartDate = y.StartDate;
                Survey.EndDate = y.EndDate;

                return Survey;
            }

            catch
            {
                return Survey;
            }
        }

        public bool CustSurvey(int[] Vote, int Id)
        {
            try
            {
                List<PerticipentModel> members = new List<PerticipentModel>();
                foreach (var i in Vote)
                {
                    PerticipentModel member = new PerticipentModel
                    {
                        ActivityId = Id,
                        PerticipentId = Current_User_id,
                        AnswerId = i
                    };
                    members.Add(member);
                }

                _data.Perticipents.AddRange(members);
                _data.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }
        }


        public SurveyViewModel ShowResult(int id)
        {

            SurveyViewModel Survey = new SurveyViewModel();

            List<QA> QusAnsList = new List<QA>();
            try
            {
                var y = _data.PollingAndSyrvays.FirstOrDefault(x => x.Id == id );

                var questions = _data.Questions.Where(x => x.ActivityId == y.Id && x.ActivityName == y.Name).ToList();

                Survey.Id = id;
                Survey.Title = y.Title;
                Survey.Description = y.Description;

                foreach (var q in questions)
                {
                    QA QueAns = new QA
                    {
                        Id = q.Id,
                        Type = "Question",
                        Q_A = q.Question
                    };

                    QusAnsList.Add(QueAns);

                    var ans = _data.Answers.Where(P => P.QuestionId == q.Id).Select(x => new { x.Id, x.Answer }).ToList();


                    foreach (var a in ans)
                    {
                        QA QueAns2 = new QA
                        {
                            Id = a.Id,
                            Type = "Answer",
                            Q_A = a.Answer,
                            Result = _data.Perticipents.Where(x => x.AnswerId == a.Id && x.ActivityId==id).Count()
                        };

                        QusAnsList.Add(QueAns2);
                    }


                }
                Survey.QA = QusAnsList;

                Survey.StartDate = y.StartDate;
                Survey.EndDate = y.EndDate;

                return Survey;
            }

            catch
            {
                return Survey;
            }
        }

    }
}