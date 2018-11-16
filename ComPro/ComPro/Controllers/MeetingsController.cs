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

        IMeetings _MeetingManager = new MeetingsManager();

       

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
        public ActionResult Create(Meetings_Models model, FormCollection frm)
        {
            model.Perticipents_Id = null;
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(frm["Perticipent"]))
                {
                    model.Perticipents_Id = frm["Perticipent"];
                }
                
            }

            _MeetingManager.Meeting_Information(model);
           
            return View();
        }

        public ActionResult Details(int id)
        {
            var Result = _MeetingManager.Meeting(id);
            return View(Result);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var Result = _MeetingManager.GetMeeting(id);
            return View(Result);
        }

        [HttpPost]
        public ActionResult Edit(MeetingEditModel model)
        {
            _MeetingManager.PostMeeting(model);
            return View(model);
        }


        public ActionResult Delete(int id )
        {
            _MeetingManager.RemoveMeeting(id);

            return View("Index",_MeetingManager.AllMeetingss());
        }


    }
}