using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    interface IChat
    {
        IEnumerable<ChatModel> All_reciever();
        IEnumerable<ChatModel> Allmember();
        IEnumerable<ChatHisytoryModel> Chat_History(string email);
        //IEnumerable<ChatModel> Save_Message(string ID2, string Message);
        ChatModel Chat(string Id);
        bool Save_Message(Chat_Data_Pass model);
    }
}
