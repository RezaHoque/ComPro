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

        string printtype(MyPoint a, MyPoint b, MyPoint c);
        double GetPerimeter(MyPoint a, MyPoint b, MyPoint c);
        string GetString(MyPoint a, MyPoint b, MyPoint c);
    }
}
