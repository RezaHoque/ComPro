using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class User_Feedback_Model
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public int Phone { get; set; }

        public string Message { get; set; }
        public bool Status { get; set; }
               
        public DateTime SubmitDate { get; set; }
        

    }
}