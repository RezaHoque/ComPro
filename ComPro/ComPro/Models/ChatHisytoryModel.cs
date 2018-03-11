using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class ChatHisytoryModel
    {
        public string SenderName { get; set; }

        public string Message { get; set; }

        public System.DateTime? Date_Time { get; set; }
    }
}