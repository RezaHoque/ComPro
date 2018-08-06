using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class Contac_View_Model
    {
        

            [Required]
            public string Name { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            public int Phone { get; set; }
            [EmailAddress]
            [Required]
            public string Email { get; set; }

            [Required]
            public string Message { get; set; }
        
    }
}