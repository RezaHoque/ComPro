using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    public interface INoticeBoard
    {
        IEnumerable<NoticeBoard> GetApprovedNotices();
        IEnumerable<NoticeBoard> GetNewNotices();
        string PostNotices(NoticeBoard model);
        NoticeBoard GetDetails(int id);
        void ApproveNotice(int id);

        NoticeBoard GetEdit(int id);
        string PostEdit(NoticeBoard model);
        string PostDelete(int id);

        bool PostComment(PublicComment comment);
        ICollection<PublicComment> GetComments(int noticeId);
    }
}
