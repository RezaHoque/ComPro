using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;




namespace ComPro.Interfaces
{
    interface IMeetings
    {
        List<MeetingViewModel> AllMeetingss();
        MeetingDetailsModel Meeting(int id);
        void Meeting_Information(Meetings_Models model);
        MeetingEditModel GetMeeting(int id);
        void PostMeeting(MeetingEditModel model);
        void RemoveMeeting(int id);
    }
}
