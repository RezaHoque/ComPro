using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class InternalRegisterViewModel
    {

        [Required]
        [Display(Name = "Full name")]
        public string Name { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string PostCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public int Phone { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        
        [Display(Name = "Date of Birth")]
        public System.DateTime? BirthDate { get; set; }

       
        [Display(Name = "Current Job Title")]
        public string CurrentJobTitle { get; set; }

        
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        
        [Display(Name = "Skills")]
        public string Skills { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}