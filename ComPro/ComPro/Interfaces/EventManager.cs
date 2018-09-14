using ComPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComPro.Models.Enums;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using ComPro.Helpers;
using System.IO;

namespace ComPro.Interfaces
{
    public class EventManager : IEvent
    {

        private ApplicationDbContext _data = new ApplicationDbContext();
        string Current_User_id = HttpContext.Current.User.Identity.GetUserId();
        IUserProfile _userProfile = new UserProfileManager();


        public IEnumerable<EventViewModel> AllEvent()
        {
            List<EventViewModel> Result = new List<EventViewModel>();
            List<EventModel> events = new List<EventModel>();

            try
            {

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





                /*

                var AllResult = _data.Event.Where(a => a.EventStatus == true && a.IsApproved == true)
                       .AsEnumerable().Select(p => new EventViewModel
                       {
                           Id = p.EventId,
                           EventTitel = p.Title,
                           Approval = p.IsApproved,
                           CreatorID = p.CreatorId,
                           Activity = null,
                           Description=p.Description,
                           Place=p.Place,
                           EventDate=p.Date,
                           Images=_data.SiteImages.Where(x=>x.Type=="Event" && x.TypeId==p.EventId).ToList(),
                           TotalYes=_data.EventMember.Where(x=>x.EventId==p.EventId && x.PerticipetingType== "Going").Count(),
                           Members=_data.EventMember.Where(x => x.EventId == p.EventId).ToList(),
                           CreatorName= UserInformation.UserNameById(p.CreatorId)
                       });
                */


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

                Result = events.AsEnumerable().Select(p => new EventViewModel
                {
                    Id = p.EventId,
                    EventTitel = p.Title,
                    Approval = p.IsApproved,
                    CreatorID = p.CreatorId,
                    Activity = _data.EventMember.FirstOrDefault(x => x.EventId == p.EventId && x.MemberID == Current_User_id) != null ? _data.EventMember.FirstOrDefault(x => x.EventId == p.EventId && x.MemberID == Current_User_id).PerticipetingType : null,
                    Description = p.Description,
                    Place = p.Place,
                    EventDate = p.Date,
                    Images = _data.SiteImages.Where(x => x.Type == "Event" && x.TypeId == p.EventId).ToList(),
                    TotalYes = _data.EventMember.Where(x => x.EventId == p.EventId && x.PerticipetingType == "Going").Count(),
                    Members = _data.EventMember.Where(x => x.EventId == p.EventId).ToList(),
                    CreatorName = UserInformation.UserNameById(p.CreatorId),
                    EventEndDate = p.End


                }).ToList();


                /*
                    if (_data.EventMember.Any(x => ((x.EventId == item.Id) && (x.MemberID == Current_User_id))) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                    {
                        if (!(HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                        {
                            var check = _data.EventMember.FirstOrDefault(x => x.EventId == item.Id && x.MemberID == Current_User_id);
                            item.Activity = check.PerticipetingType;
                        }
                        Result.Add(item);
                    }

                */


                return Result.OrderBy(x => x.EventDate);


            }

            catch
            {
                return Result;

            }
        }

        public IEnumerable<EventViewModel> NewEvent()
        {
            List<EventViewModel> Result = new List<EventViewModel>();

            try
            {


                var AllEvent = _data.Event.Where(x => x.EventStatus == true);

                foreach (var item in AllEvent)
                {
                    if (DateTime.Now > item.End)
                    {
                        item.EventStatus = false;

                    }
                }
                _data.SaveChanges();



                if (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString()))
                {
                    var AllResult = _data.Event.Where(a => a.EventStatus == true && a.IsApproved == false)
                               .AsEnumerable().Select(p => new EventViewModel
                               {
                                   Id = p.EventId,
                                   EventTitel = p.Title,
                                   Approval = p.IsApproved,
                                   CreatorID = p.CreatorId,
                                   Activity = null
                               });

                    return AllResult;
                }

                else
                {


                    var AllResult = _data.Event.Where(a => a.EventStatus == true && a.IsApproved == true)
                        .AsEnumerable().Select(p => new EventViewModel
                        {
                            Id = p.EventId,
                            EventTitel = p.Title,
                            Approval = p.IsApproved,
                            CreatorID = p.CreatorId,
                            Activity = null
                        });


                    foreach (var item in AllResult)
                    {
                        if (_data.EventMember.Any(x => ((x.EventId == item.Id) && (x.MemberID == Current_User_id))) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                        {
                            if (!(HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                            {
                                var check = _data.EventMember.FirstOrDefault(x => (x.EventId == item.Id));
                                item.Activity = check.PerticipetingType;
                            }
                            Result.Add(item);
                        }
                    }
                    return Result;


                }
            }

            catch
            {
                return Result;

            }
        }


        public IEnumerable<EventViewModel> MyEvent()
        {

            List<EventViewModel> Result = new List<EventViewModel>();
            List<EventModel> events = new List<EventModel>();

            try
            {

                var AllEvent = _data.Event.Where(x => x.EventStatus && x.Date >= DateTime.Now && x.IsApproved);


                foreach (var item in AllEvent)
                {
                    if (item.CreatorId == Current_User_id)
                    {
                        events.Add(item);
                    }

                }





                Result = events.AsEnumerable().Select(p => new EventViewModel
                {
                    Id = p.EventId,
                    EventTitel = p.Title,
                    Approval = p.IsApproved,
                    CreatorID = p.CreatorId,
                    Activity = _data.EventMember.FirstOrDefault(x => x.EventId == p.EventId && x.MemberID == Current_User_id) != null ? _data.EventMember.FirstOrDefault(x => x.EventId == p.EventId && x.MemberID == Current_User_id).PerticipetingType : null,
                    Description = p.Description,
                    Place = p.Place,
                    EventDate = p.Date,
                    Images = _data.SiteImages.Where(x => x.Type == "Event" && x.TypeId == p.EventId).ToList(),
                    TotalYes = _data.EventMember.Where(x => x.EventId == p.EventId && x.PerticipetingType == "Going").Count(),
                    Members = _data.EventMember.Where(x => x.EventId == p.EventId).ToList(),
                    CreatorName = UserInformation.UserNameById(p.CreatorId),
                    EventEndDate = p.End


                }).ToList();





                return Result.OrderBy(x => x.EventDate);


            }

            catch
            {
                return Result;

            }
        }


        public DetailViewModel Detail(int Id)
        {
            DetailViewModel Result = new DetailViewModel();

            try
            {

                EventModel eventModel = _data.Event.Find(Id);

                if (eventModel == null)
                {
                    return null;
                }

                else
                {



                    Result.MembersList = _data.EventMember.Where(x => x.EventId == Id).ToList();

                    int Going = (int)PerticipentType.Going;
                    int NotGoing = (int)PerticipentType.NotGoing;
                    int Maybe = (int)PerticipentType.Maybe;
                    int Seen = (int)PerticipentType.Seen;


                    foreach (var item in Result.MembersList)
                    {
                        switch (item.PerticipetingType)
                        {
                            case "Going":
                                Going++;
                                break;

                            case "NotGoing":
                                NotGoing++;
                                break;

                            case "Maybe":
                                Maybe++;
                                break;

                            case "Seen":
                                Seen++;
                                break;


                        }

                    }

                    Result.Id = Id;
                    Result.Title = eventModel.Title;
                    Result.Description = eventModel.Description;
                    Result.Date = eventModel.Date;
                    Result.Place = eventModel.Place;

                    Result.Members = Result.MembersList.Count();
                    Result.Going = Going;
                    Result.NotGoing = NotGoing;
                    Result.Maybe = Maybe;
                    Result.Seen = Seen;


                    Result.Creation = eventModel.Creation;
                    Result.CreatorName = Helpers.UserInformation.UserName(eventModel.CreatorId);
                    Result.CreatorId = eventModel.CreatorId;
                    Result.ApprovalDate = eventModel.ApprovalDate;
                    Result.IsApproved = eventModel.IsApproved;
                    Result.EndDate = eventModel.End;


                    if (Result.MembersList.Any(x => x.MemberID == Current_User_id) && !(Result.CreatorId == Current_User_id))
                    {
                        var user = Result.MembersList.FirstOrDefault(x => x.MemberID == Current_User_id);
                        if (user.PerticipetingType == null)
                        {
                            Result.UserActivity = PerticipentType.NotResponsed.ToString();
                        }
                        else
                        {
                            Result.UserActivity = user.PerticipetingType;
                        }

                    }

                    else if (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString()))
                    {
                        Result.UserActivity = UserRole.Administrator.ToString();
                    }
                    else if (Result.CreatorId == Current_User_id)
                    {
                        Result.UserActivity = EventType.Creator.ToString();
                    }




                    return Result;

                }

            }

            catch
            {
                return Result;
            }
        }


        public EventModel Create(EventModel model, List<string> inviteesIds)
        {
            EventModel Data = new EventModel();
            try
            {

                Data.Title = model.Title;
                Data.Description = model.Description;
                Data.Date = model.Date;
                Data.Place = model.Place;

                Data.Creation = DateTime.Now;
                Data.CreatorId = Current_User_id;

                Data.EventStatus = true;
                Data.IsApproved = true;
                Data.End = model.End;
                Data.ApprovalDate = DateTime.Now;
                Data.IsPublic = inviteesIds.Any() ? false : true;



                _data.Event.Add(Data);
                _data.SaveChanges();
                if (Data.IsPublic == false)
                {
                    CreateMember(Data, inviteesIds);
                }



                return Data;
            }

            catch (Exception ex)
            {

                Email_Service_Model email = new Email_Service_Model();
                email.ToEmail = System.Configuration.ConfigurationManager.AppSettings["BccEmail"];
                email.EmailSubject = "Failed to create notice.";
                email.EMailBody = $"Description: {model.Description}. Title: {model.Title}. Exception: {ex.ToString()}";

                var emailmanager = new UtilityManager();
                emailmanager.SendEmail(email);
                return Data;
            }
        }


        public void CreateMember(EventModel Data, List<string> inviteesIds)
        {
            try
            {

                EventMember member = new EventMember();
                List<EventMember> MemberList = new List<EventMember>();


                foreach (var id in inviteesIds)
                {
                    //ID = Int32.Parse(id);

                    //var user = _data.UserInfo.FirstOrDefault(x=>x.Id==ID);
                    //var info = _data.Users.FirstOrDefault(x => x.Id == user.Email);
                    //member.MemberID = info.Id;
                    MemberList.Add(new EventMember()
                    {
                        MemberID = id,
                        EventId = Data.EventId,
                        ResponseDate = DateTime.Now,
                        PerticipetingType = null,

                    });



                }
                _data.EventMember.AddRange(MemberList);
                _data.SaveChanges();


            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ApproveEvent(int Id)
        {

            try
            {
                var Event = _data.Event.FirstOrDefault(x => x.EventId == Id);
                Event.IsApproved = false;
                Event.ApprovalDate = DateTime.Now;

                _data.SaveChanges();

                return true;
                //return Helpers.Constants.EventApprove;
            }
            catch
            {
                return false;
                //return Helpers.Constants.EventNotApprove;
            }
        }

        public EventModel GetEdit(int Id)
        {
            try
            {
                EventModel eventModel = _data.Event.Find(Id);

                if ((eventModel.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                    return eventModel;
                }
                return null;

            }
            catch
            {
                return null;
            }

        }

        public bool PostEdit(EventModel model)
        {
            try
            {
                EventModel eventModel = _data.Event.Find(model.EventId);

                eventModel.Title = model.Title;
                eventModel.Description = model.Description;
                eventModel.Date = model.Date;
                eventModel.Place = model.Place;

                _data.SaveChanges();

                SendMessage(model.EventId, Helpers.Constants.EventEditMessage);


                //return Helpers.Constants.EventEditSuccess;
                return true;
            }

            catch
            {
                //return Helpers.Constants.EventEditFail;
                return false;
            }

        }

        //public EventModel GetDelete(int Id)
        //{
        //    try
        //    {
        //        EventModel eventModel = _data.Event.Find(Id);
        //        if ((eventModel.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
        //        {
        //            return eventModel;
        //        }
        //        return null;

        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //}

        public bool GetDelete(int Id)
        {
            try
            {
                EventModel eventModel = _data.Event.Find(Id);
                if ((eventModel.CreatorId == Current_User_id) || (HttpContext.Current.User.IsInRole(UserRole.Administrator.ToString())))
                {
                    var Members = _data.EventMember.Where(x => x.EventId == Id).ToList();

                    if (Members.Count() > 0)
                    {
                        SendMessage(Id, Helpers.Constants.EventDeleteMessage);

                        foreach (var item in Members)
                        {
                            _data.EventMember.Remove(item);
                        }


                    }




                    var Image = _data.SiteImages.FirstOrDefault(x => x.Type == "Event" && x.TypeId == Id);

                    if (Image != null)
                    {

                        var filepath = System.Web.HttpContext.Current.Server.MapPath(Image.ImagePath);
                        if (File.Exists(filepath))
                        {
                            File.Delete(filepath);

                        }

                        _data.SiteImages.Remove(Image);

                    }


                    _data.Event.Remove(eventModel);


                    _data.SaveChanges();




                }
                return true;
                //return Helpers.Constants.EventDeletetSuccess;
            }
            catch
            {
                return false;
                //return Helpers.Constants.EventDeleteFail;
            }

        }


        public void Disposing(bool Disposing)
        {
            if (Disposing)
            {
                _data.Dispose();
            }


        }


        public bool MemberResponse(int Id, string Response)
        {

            try
            {
                var perticipent = _data.EventMember.FirstOrDefault(x => x.EventId == Id && x.MemberID == Current_User_id);
                if (perticipent != null)
                {
                    perticipent.PerticipetingType = Response;
                    perticipent.ResponseDate = DateTime.Now;
                    _data.SaveChanges();
                }
                else
                {

                    EventMember NewPerticipent = new EventMember();
                    NewPerticipent.EventId = Id;
                    NewPerticipent.MemberID = Current_User_id;
                    NewPerticipent.PerticipetingType = Response;
                    NewPerticipent.ResponseDate = DateTime.Now;
                    _data.EventMember.Add(NewPerticipent);
                    _data.SaveChanges();

                }

                //if (perticipent.PerticipetingType == null)
                //{
                //    NewPerticipent.EventId = Id;
                //    NewPerticipent.MemberID = Current_User_id;
                //    NewPerticipent.PerticipetingType = Response;
                //    NewPerticipent.ResponseDate = DateTime.Now;

                //    _data.EventMember.Add(NewPerticipent);

                //}
                //else
                //{
                //    perticipent.PerticipetingType = Response;
                //    perticipent.ResponseDate = DateTime.Now;
                //    _data.Entry(perticipent).State = EntityState.Modified;

                //}

                //_data.SaveChanges();
                return true;
            }

            catch
            {
                return false;
            }

        }


        private void SendMessage(int Id, string Message)
        {

            var Members = _data.EventMember.Where(x => x.EventId == Id);

            string senderID = System.Configuration.ConfigurationManager.AppSettings["MessageSender"];

            List<MessageSendingModel> SendMessageList = new List<MessageSendingModel>();
            List<MessageRecieveModel> MessageRecieveList = new List<MessageRecieveModel>();
            string MessageThreadID = null;
            string AulterMessageThreadID = null;
            string RecieverID = null;


            var count = Members.Count();
            int check = 0;


            foreach (var item in Members)
            {


                RecieverID = item.MemberID;
                MessageThreadID = senderID + RecieverID;
                AulterMessageThreadID = RecieverID + ',' + senderID;

                if (_data.SendMessage.Any(x => x.MessageThreadID == AulterMessageThreadID))
                {
                    MessageThreadID = AulterMessageThreadID;
                }



                SendMessageList.Add(new MessageSendingModel()
                {
                    SenderID = senderID,
                    Massage = Message,
                    MessageThreadID = MessageThreadID,
                    Date_Time = DateTime.Now

                });

                MessageRecieveList.Add(new MessageRecieveModel()
                {
                    RecieverID = RecieverID,
                    MessageThreadID = MessageThreadID

                });


                check++;

            }

            _data.SendMessage.AddRange(SendMessageList);
            _data.RecieveMessage.AddRange(MessageRecieveList);
            _data.SaveChanges();



        }



    }
}