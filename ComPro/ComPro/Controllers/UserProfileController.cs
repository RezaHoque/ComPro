using ComPro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using ComPro.Helpers;

namespace ComPro.Controllers
{

    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfile _userProfile;
        private readonly INoticeBoard _noticeBoardManager = new NoticeBoardManager();
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


        [HttpGet]
        public ActionResult MyPage()
        {

            return View();
        }

        

        [HttpGet]
        public ActionResult MyInfoPage()
        {
            return PartialView("_PartialMyPageView", _userProfile.EditUserProfile());

        }

        [HttpPost]
        public ActionResult MyInfoPage(UserInfo info)
        {

            var result= _userProfile.PostEditUserProfile(info);
            //return PartialView("_PartialMyPageView", _userProfile.EditUserProfile());
            return Content(result.ToString());
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = UserInformation.UserName(User.Identity.GetUserName());
                    var _comPath = Path.Combine(Server.MapPath("~/Content/images/Profile/"), _imgname + _ext);
                   

                    pic.SaveAs(_comPath);

                    var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    ApplicationDbContext _data = new ApplicationDbContext();
                    var userinfo1 = _data.Users.FirstOrDefault(x => x.Id == user);
                    UserInfo userinfo2 = _data.UserInfo.FirstOrDefault(y => y.Email == userinfo1.Email);

                    //userinfo2.Photo = "/Content/images/Profile/" + _imgname + _ext; 
                    userinfo2.Photo = "/Content/images/Profile/" + _imgname + _ext; 
                   
                    _data.SaveChanges();


                }
            }

            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }



        public ActionResult ProfilePicture()
        {

            return PartialView("_PartialProfilePictureView", _userProfile.CurrentUserDetail());


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