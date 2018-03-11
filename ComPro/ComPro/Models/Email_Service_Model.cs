using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComPro.Models
{
    public class Email_Service_Model
    {

        [DataType(DataType.EmailAddress), Display(Name = "To")]
        [Required]
        public string ToEmail { get; set; }

        [Display(Name = "Body")]
        [Required]
        [DataType(DataType.MultilineText)]
        public string EMailBody { get; set; }

        [Required]
        [Display(Name = "Subject")]
        public string EmailSubject { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "CC")]
        public string EmailCC { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "BCC")]
        public string EmailBCC { get; set; }

    }
}



