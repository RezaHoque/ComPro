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

        bool CreateNewChat (string Message, List<string> Recievers);

        IEnumerable<ChatHisytoryModel> Chat_History(string MessageThreadID);
        //IEnumerable<ChatModel> Save_Message(string ID2, string Message);
        ChatModel Chat(string Id);
        bool Save_Message(Chat_Data_Pass model);
    }
}
