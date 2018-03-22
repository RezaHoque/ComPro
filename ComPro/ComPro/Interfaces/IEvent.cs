using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    public interface IEvent
    {
        IEnumerable<EventViewModel> AllEvent();
        IEnumerable<EventViewModel> MyEvent();
        IEnumerable<EventViewModel> NewEvent();

        DetailViewModel Detail(int Id);
        EventModel Create(EventModel model,List<string> inviteesIds);
        bool ApproveEvent(int Id);
        EventModel GetEdit(int Id);
        bool PostEdit(EventModel model);

        bool GetDelete(int Id);

        void Disposing(bool Disposing);

        bool MemberResponse(int Id, string Response);
    }
}
