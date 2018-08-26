using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class CommentViewModel
    {
        public PublicComment Comment { get; set; }
        public UserInfo CommentUser { get; set; }
    }
}