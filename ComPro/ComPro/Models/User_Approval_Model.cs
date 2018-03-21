using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class User_Approval_Model
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Photo { get; set; }

    }
}