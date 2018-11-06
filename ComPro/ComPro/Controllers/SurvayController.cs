using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Models;
using ComPro.Interfaces;
using System.Net;

namespace ComPro.Controllers
{
    public class SurveyController : Controller
    {

        ISurvey _surveyManager = new SurveyManager();
        // GET: Survey
        public ActionResult Index()
        {

            return View(_surveyManager.AllPoll());
        }

        
        public ActionResult Create(int Type)
        {
           if (Type == 2)
            {

                
                return View("CreateSurvey");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreatePoll(PollViewModel Poll, FormCollection frm)
        {
            var invitees = new List<string>();
            if (!string.IsNullOrEmpty(frm["invitees"]))
            {
                invitees = frm["invitees"].Split(',').ToList();
            }

            var result = _surveyManager.CreatePoll(Poll, invitees);

            return Content(result.ToString());
        }


        [HttpPost]
        public ActionResult Createsurvey(PollViewModel Poll, string [] Questions)
        {
           

            bool result = _surveyManager.CreateSurvey(Poll, Questions);

            return Content(result.ToString());
          
        }



        [HttpGet]
        public ActionResult Custpoll(int Id)
        {

            return View(_surveyManager.SinglePoll(Id));
        }

        [HttpPost]
        public ActionResult Custpoll(string Vote, int Id)
        {
            var result = _surveyManager.cust(Vote, Id);
            return Content(result.ToString());
        }

        [HttpGet]
        public ActionResult CustSurvey(int Id)
        {

            return View(_surveyManager.SingleSurvey(Id));
        }

        [HttpPost]
        public ActionResult CustSurvey(int[] Vote, int Id)
        {
            var result = _surveyManager.CustSurvey(Vote, Id);
            return Content(result.ToString());
        }

        [HttpGet]
        public ActionResult ShowResult(int Id)
        {

            return View(_surveyManager.ShowResult(Id));
        }

        //[Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View(_surveyManager.GetEdit(id.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(SurveyViewModel Poll, string[] Questions)
        {
            bool Result = false;

            //if (ModelState.IsValid)
            //{
            //    //Result = _surveyManager.PostEdit(Model);
          

            //}
            return Content(Result.ToString());
        }

       
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            bool Result = _surveyManager.Delete(id.Value);
            return Content(Result.ToString());

        }



    }
}