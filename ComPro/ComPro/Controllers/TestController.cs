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
       
        public ActionResult Index()
        {
           

            return View();
           
        }
        

    }
}