using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ComPro.Helpers;
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
            

            try {

            List<ChatModel> Reciever = new List<ChatModel>();
              

            var AllPartner = _data.SendMessage.Where(x =>( x.MessageThreadID.Contains(Current_User_id)) && (x.SenderID != Current_User_id));
            
                foreach (var item in AllPartner)
                {

                    Reciever.Add(new ChatModel() { PartnerName = UserInformation.UserName(item.SenderID), PartnerId = item.SenderID });
                }

             List<MessageRecieveModel> AllPartnerForReciever = _data.RecieveMessage.Where(x => (x.MessageThreadID.Contains(Current_User_id)) && (x.RecieverID != Current_User_id)).ToList();

                foreach (var item in AllPartnerForReciever)
                {
                    Reciever.Add(new ChatModel() { PartnerName = UserInformation.UserName(item.RecieverID), PartnerId = item.RecieverID });
                }

                    
                
            return Reciever.DistinctBy(x => x.PartnerName).ToList();
                
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
                Reciever.Add(new ChatModel() { PartnerName = UserInformation.UserName(user2.Id), PartnerId = user2.Id });


            }

            return Reciever.DistinctBy(x => x.PartnerName).ToList();

        }

        public ChatModel Chat(string Id)
        {
            ChatModel User = new ChatModel();
            try
            {
                User.PartnerName = UserInformation.UserName(Id);
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
                     MessageThreadID = senderID+ RecieverID;
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
        public IEnumerable<ChatHisytoryModel> Chat_History(string email)
        {
            List<ChatHisytoryModel> ChatHistory = new List<ChatHisytoryModel>(); 
            try
            {
               var allMessage = _data.SendMessage.Where(x => (x.MessageThreadID.Contains(Current_User_id)) && (x.MessageThreadID.Contains(email)));


                foreach (var item in allMessage)
                {
                    ChatHistory.Add(new ChatHisytoryModel() {
                        SenderName = UserInformation.UserName(item.SenderID),
                        Message = item.Massage,
                        Date_Time = item.Date_Time });
                }
                return ChatHistory;

                   
                
            }


            catch
            {
                return ChatHistory;
            }


        }

        



    }


}