using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class Current_Status_Model
    {

        [Key]
        public int StatusId { get; set; }

        public string Status { get; set; }

        
    }
}