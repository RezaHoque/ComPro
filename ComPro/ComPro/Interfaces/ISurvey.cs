using ComPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComPro.Interfaces
{
    interface ISurvey
    {
        bool CreatePoll(PollViewModel model, List<string> inviteesIds);
        bool CreateSurvey(PollViewModel model, string[] Questions);
        List<PollingAndSyrvayModel> AllPoll();
        PollViewModel SinglePoll(int id);
        bool cust(string vote, int Id);

        SurveyViewModel SingleSurvey(int id);
        bool CustSurvey(int[] Vote, int Id);

        SurveyViewModel ShowResult(int id);

        SurveyViewModel GetEdit(int Id);
        bool PostEdit(SurveyViewModel model);
        bool Delete(int id);


    }
}
