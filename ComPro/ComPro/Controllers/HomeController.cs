using ComPro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ComPro.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private readonly IUserProfile _userProfile;
        public HomeController(IUserProfile userProfile)
        {
            _userProfile = userProfile;
        }

        IUtility _utility = new UtilityManager();
        IHome _Home = new HomeManager();

        public ActionResult Index()
        { 
           return View();
        }
        public ActionResult LatestMember()
        {

            return PartialView("_LatestMember",_Home.LatestMember(3));
        }

        public ActionResult LatestNotice()
        {

            return PartialView("_LatestNoticePartialView", _Home.LatestNotice(3));
        }

        public ActionResult LatestEvent()
        {

            return PartialView("_LatestEventPartialView", _Home.LatestEvent(3));
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Member(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                //havce to fix this
                //var userProfile = _userProfile.GetUser(name);
                return View();
            }
            return View();
        }

        public ActionResult EmaiConfirmationn (string Email)
        {
            _utility.ConfirmEmai(Email);

            return RedirectToAction("Login");


        }
        public ActionResult Terms()
        {
            return View();
        }
    }
}