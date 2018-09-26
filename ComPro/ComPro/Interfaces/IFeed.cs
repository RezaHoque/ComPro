using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    public interface IFeed
    {
        void AddToFeeds(FeedModel feed);
    }
}
