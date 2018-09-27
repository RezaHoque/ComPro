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

        ISurvey _surveyManager = new SurveyManager();
        // GET: Survey
        public ActionResult Index()
        {
            return View(_surveyManager.AllPoll());
        }

        
        public ActionResult Create(int Type)
        {
            //if (Type == 1)
            //{
            //    return View();
            //}
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



    }
}