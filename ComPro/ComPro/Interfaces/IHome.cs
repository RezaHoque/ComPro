using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    interface IHome
    {
        IEnumerable<ChatModel> LatestMember(int length);
        IEnumerable<ChatModel> LatestNotice(int length);
        IEnumerable<ChatModel> LatestEvent(int length);
    }
}
