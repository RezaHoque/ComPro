using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using ComPro.Models;
using static ComPro.Models.Enums;

namespace ComPro.Controllers
{

   
    public class TestController : Controller
    {

        private readonly IUtility _Utility = new UtilityManager();
        public ActionResult Index()
        {
            Email_Service_Model obj = new Email_Service_Model();

            obj.ToEmail = "pori468@yahoo.com";
            obj.EmailSubject = "Testing html";
            //string text = http://localhost:59835/Test/CheckLink?email=pori468@yahoo.com;
            
            obj.EMailBody = "";


           
            var result = _Utility.SendEmail(obj);

            return View();
           
        }
        

    }
}