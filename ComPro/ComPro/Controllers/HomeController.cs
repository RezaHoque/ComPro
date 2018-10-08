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
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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

        public JsonResult LatestNotice()
        {
            var latestNotices= _Home.LatestNotice(6);

            return Json(latestNotices,JsonRequestBehavior.AllowGet);

        }

        public JsonResult LatestEvent()
        {
            var latestEvents = _Home.LatestEvent(3);
            return Json(latestEvents, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            
            return View();
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
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

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(FormCollection message)
        {
            bool Result = _Home.Contac_Admin(message);
            return Content(Result.ToString());
        }

        public ActionResult Contributer()
        {
            var model = _Home.GetContributers();
            return View(model);
        }

        public ActionResult Privacy()
        {
            return View();
        }
        public ActionResult Feature()
        {
            return View();
        }
        public ActionResult Survey()
        {
            return View();
        }
    }
}