using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ComPro.Models;

namespace ComPro.Interfaces
{
    public class FeedManager:IFeed
    {
        private readonly ApplicationDbContext _db;

        public FeedManager()
        {
            _db=new ApplicationDbContext();
        }
        public void AddToFeeds(FeedModel feed)
        {
            if (feed != null)
            {
                _db.Feeds.Add(feed);
                _db.SaveChanges();
            }
        }
    }
}