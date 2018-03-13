using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class SiteImage
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int TypeId { get; set; }
        public string Type { get; set; }
        public virtual ApplicationUser Uploader { get; set; }
        public string UploaderId { get; set; }

        public DateTime UploadDate { get; set; }

    }
}