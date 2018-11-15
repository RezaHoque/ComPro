using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using ComPro.Models;

namespace ComPro.Controllers
{
    public class MeetingsController : Controller
    {


        private readonly IMeetings _MeetingManager;
       


        // GET: Meetings
        public ActionResult Index()
        {
            var Result = _MeetingManager.AllMeetingss();

            return View(Result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Meetings_Models model )
        {

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            var Result = _MeetingManager.Meeting(id);
            return View(Result);
        }

        public ActionResult Edit()
        {
            return View();
        }

       
        public ActionResult Delete()
        {
            return View();
        }


    }
}