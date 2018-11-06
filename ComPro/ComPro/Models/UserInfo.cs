using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ComPro.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        [Required]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

    
        [Display(Name = "Address")]
        public string Address { get; set; }

    
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Contact number")]
        public int Phone { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

    
        [Display(Name = "Date of Birth")]
        public System.DateTime? BirthDate { get; set; }

        [Display(Name = "Current Status")]
        public string CurrentStatus { get; set; }

        [Display(Name = "Current Position")]
        public string CurrentJobTitle { get; set; }

   
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [Display(Name = "Expectation to BPID")]
        public string Expectation { get; set; }

        [Display(Name = "Contribution to BPID")]
        public string Contribution { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
        public System.DateTime? ApprovalDate { get; set; }

    }

    
}