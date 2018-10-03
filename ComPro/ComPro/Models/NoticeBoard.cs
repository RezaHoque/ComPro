using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ComPro.Models
{
    public class NoticeBoard
    {

        
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string WebLink { get; set; }

        public bool IsApproved { get; set; }
        public bool PinUp { get; set; }


        public DateTime ActionDate { get; set; }
        public DateTime SubmitDate { get; set; }
        

        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
        public string UniqueUrl { get; set; }
    }
}