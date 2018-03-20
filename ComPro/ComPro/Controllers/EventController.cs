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

    [Authorize]
    public class EventController : Controller
    {
        IEvent _event;
        

        public EventController()
        {
            _event = new EventManager();

        }


        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Events()
        {
            return PartialView("_Events_PartialView");
        }

        public ActionResult GetEvents()
        {
            var result = _event.AllEvent();
            return PartialView("_EventList_Partialview", result);
        }

        public ActionResult MyEvent()
        {
           
            var result = _event.MyEvent();
            return PartialView("_EventList_Partialview", result);
        }
        public ActionResult NewEvent()
        {
            var result = _event.NewEvent();
            return PartialView("_EventList_Partialview", result);

        }
        // GET: Event/Details/5
        
        public ActionResult EventDetails(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            return PartialView("_EventDetails", _event.Detail(id));
            //return View(_event.Detail(id.Value));

        }


        // GET: Event/Create
        public ActionResult Create()
        {
            return PartialView("_Create_PartialView");
        }
        
        
        [HttpPost]
        public ActionResult Create(EventModel eventModel)
        {
            if (ModelState.IsValid)
            {
                
                return Content(_event.Create(eventModel).ToString());
                    
            }
         return Content(Boolean.FalseString);

        }

            
        


        public ActionResult Members()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Members(EventModel eventModel)
        {
            // We must return to event Index as EventType.All
           return View(eventModel);
        }

        public ActionResult ApproveEvent( int ? Id)
        {
           bool result = _event.ApproveEvent(Id.Value);
            return Content(result.ToString());
          

        }


       // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
           
            var Result = _event.GetEdit(id);
            if (Result == null)
            {

                ViewBag.Permission = Helpers.Constants.EventEditPermission;
                return RedirectToAction("EventDetails", new { id = id });

            }
            return PartialView("_EditPartialView", Result);
            
        }

        [HttpPost]
         public ActionResult Edit(EventModel eventModel)

        {
            if (ModelState.IsValid)
            {
                var x = _event.PostEdit(eventModel).ToString();
                return Content(x.ToString());
            }
            return View(eventModel);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool res = _event.GetDelete(id.Value);
            return Content(res.ToString());
            
        }

        

        protected override void Dispose(bool disposing)
        {
            _event.Disposing(disposing);
            base.Dispose(disposing);
        }

        [HttpGet]
       public ActionResult MemberResponse(int ID, string Response)
        {
        
        bool res = _event.MemberResponse(ID, Response);
            return Content(res.ToString());
            
            //return RedirectToAction("EventDetails", new { id = ID });
            
            
        }

    }
}
