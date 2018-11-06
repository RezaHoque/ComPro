using ComPro.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Models;
using System.IO;
using Microsoft.AspNet.Identity;
using static ComPro.Helpers.UserInformation;
using System.Data.Entity;

namespace ComPro.Controllers
{

    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly IUserProfile _userProfile;
        private readonly IFeed _feedManager;
        private readonly INoticeBoard _noticeBoardManager = new NoticeBoardManager();
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UserProfileController(IUserProfile UserProfileManager,IFeed feed)
        {
            _userProfile = UserProfileManager;
            _feedManager = feed;
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

        public ActionResult ChangeId(string id)
        {
            var result = _userProfile.UserDetail(id);
            
            return Content(result.Id.ToString());
        }

        public ActionResult NewUser()
        {
             return PartialView("_NewUser_PartialView", _userProfile.NewUserforApproval());
        }

        public ActionResult Approve(int id)
        {

            var result = _userProfile.ApproveNewUser(id);
        
            var feed=new FeedModel();
            feed.Id = Guid.NewGuid().ToString();
            feed.UserId = result.UserId;
            feed.UserName = result.Name;
            feed.Action = FeedVerb.HasJoined;
            feed.ActionDate = (DateTime) result.ApprovalDate;
            feed.ImagePath = result.Photo;
            
            _feedManager.AddToFeeds(feed);
                
            return Content(result.ToString());
           
        }


        [HttpGet]
        public ActionResult MyPage()
        {

            return View(_userProfile.EditUserProfile());
        }


        [HttpPost]
        public ActionResult MyPage(UserInfo info, FormCollection frm)
        {
            try
            {
                if (!string.IsNullOrEmpty(frm["Date"]))
                {

                    info.BirthDate = DateTime.Parse(frm["Date"]);
                }

                var result = _userProfile.PostEditUserProfile(info);
                

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                Email_Service_Model email = new Email_Service_Model();
                email.ToEmail = System.Configuration.ConfigurationManager.AppSettings["BccEmail"];
                email.EmailSubject = $"Failed to save my page data. {info.UserId}";
                email.EMailBody = $"Name: {info.Name}. Id:{info.UserId}. Exception: {ex.ToString()}";

                var emailmanager = new UtilityManager();
                emailmanager.SendEmail(email);
            }

            return View(_userProfile.EditUserProfile());
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

                    _imgname = Helpers.UserInformation.UserName(User.Identity.GetUserName());
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



        //public ActionResult ProfilePicture()
        //{

        //    return PartialView("_PartialProfilePictureView", _userProfile.CurrentUserDetail());


        //}


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