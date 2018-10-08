using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class NoticeBoardViewModel
    {
        public NoticeBoard Notice { get; set; }
        public int TotalComments { get; set; }
        public SiteImage NoticeImage { get; set; }
        public string PostedBy { get; set; }
    }
}