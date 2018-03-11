using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class MessageSendingModel
    {
        public int Id { get; set; }


        public string SenderID { get; set; }
        
        [Required]
        [Display(Name = "Massage")]
        public string Massage { get; set; }

        public System.DateTime? Date_Time { get; set; }

        public string MessageThreadID { get; set; }



    }
}

