using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static ComPro.Helpers.UserInformation;
using ComPro.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using static ComPro.Models.Enums;

namespace ComPro.Interfaces
{
    public class ChatManager : IChat
    {
        ApplicationDbContext _data = new ApplicationDbContext();

        IUserProfile _userProfile = new UserProfileManager();
        IUtility _utility = new UtilityManager();

        string Current_User_id = HttpContext.Current.User.Identity.GetUserId();


        public IEnumerable<ChatModel> All_reciever()
        {


            try
            {

                List<ChatModel> Reciever = new List<ChatModel>();


                var AllPartner = _data.SendMessage.Where(x => x.MessageThreadID.Contains(Current_User_id));

                var Recievers = new List<string>();
                string MessageTitel = null;
                

                foreach (var item in AllPartner)
                {
                    Recievers = item.MessageThreadID.Split(',').ToList();

                    MessageTitel = null;
                    foreach (var partner in Recievers)
                    {
                        if(partner!= Current_User_id)
                        {
                            MessageTitel = Helpers.UserInformation.UserName(partner) + "," + MessageTitel;
                        }
                    }
                    MessageTitel = MessageTitel.Remove(MessageTitel.Length - 1);

                    Reciever.Add(new ChatModel() { PartnerName = MessageTitel, PartnerId = item.MessageThreadID });
                }

               
                var result = Reciever.DistinctBy(x => x.PartnerName).ToList();
                return result;

            }

            catch
            {
                return null;
            }


        }

        public IEnumerable<ChatModel> Allmember()
        {
            List<ChatModel> Reciever = new List<ChatModel>();
           

            var allUser= _userProfile.AllUser();
            foreach ( var User in allUser)
            {
                var user2 = _data.Users.FirstOrDefault(x=>x.Email==User.Email);
                Reciever.Add(new ChatModel() { PartnerName = Helpers.UserInformation.UserName(user2.Id), PartnerId = user2.Id });


            }

            return Reciever.DistinctBy(x => x.PartnerName).ToList();

        }

        public ChatModel Chat(string Id)
        {
            ChatModel User = new ChatModel();
            try
            {
                //User.PartnerName = Helpers.UserInformation.UserName(Id);
                List<ChatModel> Reciever = new List<ChatModel>();


                 var Recievers = new List<string>();
                string MessageTitel = null;


               
                    Recievers = Id.Split(',').ToList();

                    foreach (var partner in Recievers)
                    {
                        if (partner != Current_User_id)
                        {
                            MessageTitel = Helpers.UserInformation.UserName(partner) + "," + MessageTitel;
                        }
                    }
                MessageTitel = MessageTitel.Remove(MessageTitel.Length - 1);

                User.PartnerName = MessageTitel;
               
                User.PartnerId = Id ;

                
                return User;
            }


            catch
            {
                return User;
            }


        }

        public bool Save_Message(Chat_Data_Pass model)
        {

            try
            {
                string senderID = Current_User_id;
                string RecieverID = model.PartnerId;
                string MessageThreadID;

                if (_data.SendMessage.Any(x => (x.MessageThreadID.Contains(senderID)) && (x.MessageThreadID.Contains(RecieverID))))
                {
                    var allMessage = _data.SendMessage.FirstOrDefault(x => (x.MessageThreadID.Contains(senderID)) && (x.MessageThreadID.Contains(RecieverID)));
                    MessageThreadID = allMessage.MessageThreadID;
                }

                else
                {
                     MessageThreadID = senderID+','+RecieverID;
                }
                


                MessageSendingModel sendmessage = new MessageSendingModel();
                MessageRecieveModel recivemessage = new MessageRecieveModel();


                sendmessage.SenderID = senderID;
                sendmessage.Massage = model.Message;
                sendmessage.MessageThreadID = MessageThreadID;
                sendmessage.Date_Time = DateTime.Now;
                _data.SendMessage.Add(sendmessage);

                recivemessage.RecieverID = RecieverID;
                recivemessage.MessageThreadID = MessageThreadID;
                _data.RecieveMessage.Add(recivemessage);
                _data.SaveChanges();

                return true;

            }
            catch
            {
                return false;
            }
        }
        public IEnumerable<ChatHisytoryModel> Chat_History(string MessageThreadID)
        {
            List<ChatHisytoryModel> ChatHistory = new List<ChatHisytoryModel>(); 
            try
            {
                //var allMessage = _data.SendMessage.Where(x => (x.MessageThreadID.Contains(Current_User_id)) && (x.MessageThreadID.Contains(email)));
                var allMessage = _data.SendMessage.Where(x => x.MessageThreadID== MessageThreadID);


                foreach (var item in allMessage)
                {
                    ChatHistory.Add(new ChatHisytoryModel() {
                        SenderName = Helpers.UserInformation.UserName(item.SenderID),
                        Message = item.Massage,
                        Date_Time = item.Date_Time });
                }

                         return ChatHistory.OrderByDescending(x => x.Date_Time);



            }


            catch
            {
                return ChatHistory;
            }


        }



        public bool CreateNewChat (string Message, List<string> Recievers)
        {
            try
            {
                

              

                List<string> Names = new List<string>();
                Names = Recievers;
                Names.Add(Current_User_id);
                Names.Sort();
                string ChatThread = null;

                foreach (var reciever in Names)
                {
                   // ChatThread = ChatThread +","+ reciever;
                   if(ChatThread==null)
                        {
                            ChatThread = reciever;
                        }
                    else
                        {
                        ChatThread = ChatThread + "," + reciever;
                    }
                }


                MessageSendingModel sendmessage = new MessageSendingModel();

                sendmessage.SenderID = Current_User_id;
                sendmessage.Massage = Message;
                sendmessage.MessageThreadID = ChatThread;
                sendmessage.Date_Time = DateTime.Now;
                _data.SendMessage.Add(sendmessage);


                List<MessageRecieveModel> recivemessage = new List<MessageRecieveModel>();
                foreach (var reciever in Recievers)
                {
                    recivemessage.Add(new MessageRecieveModel()
                    {
                        RecieverID = reciever,
                    MessageThreadID = ChatThread,
                    HasRead= null,
                    ReadingDateTime=null,
                });


                }
                   

                _data.RecieveMessage.AddRange(recivemessage);
                _data.SaveChanges();


                return true;
            }

            catch (Exception ex)
            {
               return false;
            }
        }



    }


}