﻿using ComPro.Models;
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
        List<IndexViewModel2> AllPoll();
        PollViewModel SinglePoll(int id);
        bool cust(string vote, int Id);
    }
}
