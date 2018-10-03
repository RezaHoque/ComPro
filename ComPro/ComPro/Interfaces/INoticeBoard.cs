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
        List<NoticeBoardViewModel> GetMyNotice();
        //IEnumerable<NoticeBoard> GetNewNotices();
        NoticeBoard PostNotices(NoticeBoard model);
        NoticeBoardViewModel GetDetails(string id);
      //  void ApproveNotice(int id);

        NoticeBoardViewModel GetEdit(int id);
        string PostEdit(NoticeBoardViewModel model);
        bool DeleteNotice(int id);

        bool PostComment(PublicComment comment);
        ICollection<CommentViewModel> GetComments(int noticeId);
        bool SaveImage(SiteImage image);
        SiteImage GetNoticeImage(int id,string type);
    }
}
