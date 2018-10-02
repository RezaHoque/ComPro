using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComPro.Models
{
    public class FeedModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Verb { get; set; }
        public string Action { get; set; }
        public string ActionId { get; set; }
        public string ActionTitle { get; set; }
        public string UrlToAction { get; set; }
        public DateTime ActionDate { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
    }
    public static class FeedVerb
    {
        public static string HasCreated = "has created";

        public static string HasJoined = "has joined";

        public static string HasPosted = "has posted";
    }

}