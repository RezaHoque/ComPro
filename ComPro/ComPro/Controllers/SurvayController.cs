using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Models;
using ComPro.Interfaces;

namespace ComPro.Controllers
{
    public class SurveyController : Controller
    {

        ISurvey _chat = new SurveyManager();
        // GET: Survey
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult Create()
        //{
        //    return View();
        //}



    }
}