using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class PublicComment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDateTime { get; set; }
        public int NoticeId { get; set; }
        public virtual NoticeBoard Notice { get; set; }
        public bool IsBlocked { get; set; }
        public string CommentUserId { get; set; }
        public virtual ApplicationUser CommentUser { get; set; }
    }
}