using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class SiteContibuter
    {
        public string Id { get; set; }
        public string ContributingUserId { get; set; }
        public virtual ApplicationUser ContributingUser { get; set; }
        public string Contribution { get; set; }
        public DateTime ActionDate { get; set; }
        public string PhotoPath { get; set; }

    }
}