using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        private readonly IEvent _eventManager;
        private readonly INoticeBoard _noticeBoardManager;
        

        public EventController(IEvent eventarg,INoticeBoard noticeboardManager)
        {
           _eventManager = eventarg;
            _noticeBoardManager = noticeboardManager;

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
            var result = _eventManager.AllEvent();
            return PartialView("_EventList_Partialview", result);
        }

        public ActionResult MyEvent()
        {
           
            var result = _eventManager.MyEvent();
            return PartialView("_EventList_Partialview", result);
        }
        public ActionResult NewEvent()
        {
            var result = _eventManager.NewEvent();
            return PartialView("_EventList_Partialview", result);

        }
        // GET: Event/Details/5
        
        public ActionResult EventDetails(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            return PartialView("_EventDetails", _eventManager.Detail(id));
            //return View(_event.Detail(id.Value));

        }


        // GET: Event/Create
        public ActionResult Create()
        {
            //return PartialView("_Create_PartialView");
            return View();
        }
        
        
        [HttpPost]
        public ActionResult Create(EventModel eventModel,FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                var invitees = new List<string>();
                if (!string.IsNullOrEmpty(frm["invitees"]))
                {
                    invitees = frm["invitees"].Split(',').ToList();
                }
                var result = _eventManager.Create(eventModel,invitees);
                if (result!=null)
                {
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file != null && file.ContentLength > 0)
                        {
                            var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                            var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                            file.SaveAs(path);
                            var image = new SiteImage
                            {
                                ImagePath = "/Content/images/" + fileName,
                                Type = "Event",
                                TypeId = result.EventId,
                                UploadDate = DateTime.Now,
                                UploaderId = result.CreatorId

                            };
                            _noticeBoardManager.SaveImage(image);
                        }
                    }
                    return RedirectToAction("Index");
                }
                    
                //return Content(.ToString());

            }
            return View();

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
           bool result = _eventManager.ApproveEvent(Id.Value);
            return Content(result.ToString());
          

        }


       // GET: Event/Edit/5
        public ActionResult Edit(int id)
        {
           
            var Result = _eventManager.GetEdit(id);
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
                var x = _eventManager.PostEdit(eventModel).ToString();
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
            bool res = _eventManager.GetDelete(id.Value);
            return Content(res.ToString());
            
        }

        

        protected override void Dispose(bool disposing)
        {
            _eventManager.Disposing(disposing);
            base.Dispose(disposing);
        }

        [HttpGet]
       public ActionResult MemberResponse(int ID, string Response)
        {
        
        bool res = _eventManager.MemberResponse(ID, Response);
            return Content(res.ToString());
            
            //return RedirectToAction("EventDetails", new { id = ID });
            
            
        }

    }
}
