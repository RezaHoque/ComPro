using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComPro.Models;

namespace ComPro.Interfaces
{
    interface IUtility
    {
        bool CheckEmailAddressFormat(string Data);
        string SendEmail(Email_Service_Model Obj);
        void ConfirmEmai(string email);
        //string ConvertUrlsToLinks(string msg);

        
    }
}
