using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class MessageRecieveModel
    {

        public int Id { get; set; }


        public string RecieverID { get; set; }

        public string HasRead { get; set; }

       public System.DateTime? ReadingDateTime { get; set; }

        public string MessageThreadID { get; set; }



    }
}
