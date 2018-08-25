using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ComPro.Models;

namespace ComPro.Interfaces
{
    interface IHome
    {
        IEnumerable<UserInfo> LatestMember(int length);
        IEnumerable<NoticeBoard> LatestNotice(int length);
        IEnumerable<ChatModel> LatestEvent(int length);

        bool Contac_Admin(FormCollection Message);
        IEnumerable<SiteContibuter> GetContributers();
    }
}
