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
    {IEvent _event = new EventManager();
        public ActionResult Index(string s)
        {
            
            var reuslt = _event.AllEvent();
            return View(reuslt);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var Result = _event.GetEdit(id);
            if (Result == null)
            {

                ViewBag.Permission = Helpers.Constants.EventEditPermission;
                return RedirectToAction("EventDetails", new { id = id });

            }
            return PartialView("_editpartialviewmodel", Result);

        }

        [HttpPost]
        public ActionResult Edit(EventModel eventModel)

        {
            if (ModelState.IsValid)
            {
                var x = _event.PostEdit(eventModel).ToString();
                return Content(x.ToString());
            }
            return PartialView("_editpartialviewmodel", eventModel);
        }

    }
}