using ComPro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Models;


namespace ComPro.Controllers
{

    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfile _userProfile;
        public UserProfileController(IUserProfile UserProfileManager)
        {
            _userProfile = UserProfileManager;
        }
        
        // GET: UserProfile
        public ActionResult Index ()
        {
            return View();
        }

        public ActionResult Members()
        {
            return PartialView("_Members_PartialView");
        }

        public ActionResult AllMembers()
        {
            return PartialView("_AllMember_PertialView", _userProfile.AllUser());
            
        }

        //public ActionResult Index1()
        //{

        //    return View(_userProfile.AllUser());
        //}

        //[HttpGet]
        //public ActionResult Edit( int? id )
        //{

        //    return View(_userProfile.EditUserProfile(id.Value));
        //}

        //[HttpPost]
        //public ActionResult Edit(UserInfo info)
        //{
        //    ViewBag.EditData=_userProfile.PostEditUserProfile(info);
        //    return View(info);

        //}
        public ActionResult Delete( int ? id)
        {
            ViewBag.DeleteProfile = _userProfile.DeleteUserProfile(id.Value);
            return RedirectToAction("Index");
        }

        public ActionResult Details( int? id)
        {
            return PartialView("_MemberInformation_PartialView", _userProfile.DetailProfile(id.Value));
        }


        public ActionResult NewUser()
        {
             return PartialView("_NewUser_PartialView", _userProfile.NewUserforApproval());
        }

        public ActionResult Approve(int id)
        {
           
            bool result = _userProfile.ApproveNewUser(id);
            return Content(result.ToString());
        }


        public ActionResult MyPage()
        {

            return View();

        }

        public ActionResult CurrentUser()
        {
            return PartialView("_CurrentUser_Partialview");
            //return View(_userProfile.CurrentUserDetail());

        }
        public ActionResult CurrentUserDetails ()
        {

            //return View(_userProfile.CurrentUserDetail());
            return PartialView("_MemberInformation_PartialView", _userProfile.CurrentUserDetail());


        }

        [AllowAnonymous]
        public ActionResult CheckLink(string email)
        {
            TempData["NewLogin"] = _userProfile.CheckLink(email);

            return RedirectToAction("Login","Account");


        }
        [AllowAnonymous]
        public JsonResult UserList()
        {
            var alluser = _userProfile.AllUser();
            return Json(alluser, JsonRequestBehavior.AllowGet);
        }

    }
}