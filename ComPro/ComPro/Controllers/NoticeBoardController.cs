using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using ComPro.Models;
using static ComPro.Models.Enums;
using Microsoft.AspNet.Identity;
using System.Net;
using System.IO;

namespace ComPro.Controllers
{
    //[Authorize]
    public class NoticeBoardController : Controller
    {
        ApplicationDbContext _data = new ApplicationDbContext();
        private readonly INoticeBoard _noticeBoardManager;
        private ApplicationUserManager _userManager;
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public NoticeBoardController(INoticeBoard noticeBoardManager, ApplicationUserManager userManager)
        {
            _noticeBoardManager = noticeBoardManager;
            _userManager = userManager;
        }
        // GET: NoticeBoard
        public ActionResult Index()
        {
            var noticeVMList = new List<NoticeBoardViewModel>();
            var notices = _noticeBoardManager.GetApprovedNotices();
           
            foreach(var n in notices)
            {
                var totalComnts = _noticeBoardManager.GetComments(n.Id).Count();
                noticeVMList.Add(new NoticeBoardViewModel
                {
                    Notice = n,
                    TotalComments = totalComnts,
                    NoticeImage = _noticeBoardManager.GetNoticeImage(n.Id,"Notice")
                });
            }

            return View(noticeVMList.OrderByDescending(y=>y.Notice.SubmitDate).OrderByDescending(x => x.Notice.PinUp).ToList());
        }

        public ActionResult MyNotice()
        {
           
            return View("Index",_noticeBoardManager.GetMyNotice());
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(NoticeBoard model)
        {
            try
            {
                model.CreatorId = User.Identity.GetUserId();

                var notice = _noticeBoardManager.PostNotices(model);

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
                            Type = "Notice",
                            TypeId = notice.Id,
                            UploadDate = DateTime.Now,
                            UploaderId = notice.CreatorId

                        };
                        _noticeBoardManager.SaveImage(image);
                    }

                    else
                    {
                        var image = new SiteImage
                        {
                            ImagePath = "/Content/images/Event/defaultNotice.png",
                            Type = "Notice",
                            TypeId = notice.Id,
                            UploadDate = DateTime.Now,
                            UploaderId = notice.CreatorId

                        };
                        _noticeBoardManager.SaveImage(image);

                    }


                }
            }
            catch (Exception ex)
            {
                logger.Error($"Error creating notice. {ex}");
                Email_Service_Model email = new Email_Service_Model();
                email.ToEmail = System.Configuration.ConfigurationManager.AppSettings["BccEmail"];
                email.EmailSubject = $"Failed to create notice. User- {model.Creator.UserName}";
                email.EMailBody = $"Description: {model.Description}. Title: {model.Title}. Exception: {ex.ToString()}";

                var emailmanager = new UtilityManager();
                emailmanager.SendEmail(email);
            }





            return RedirectToAction("Index");
        }
        //[Authorize]
        public ActionResult Notice(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ViewBag.NoticeId = id;
                return View(_noticeBoardManager.GetDetails(id));
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        [Authorize]
        public ActionResult PostComment(PublicComment model)
        {

            if (model != null)
            {
                model.CommentDateTime = DateTime.Now;
                model.CommentUserId = User.Identity.GetUserId();

                var result = _noticeBoardManager.PostComment(model);

                return Content(result.ToString());
            }
            return Content(Boolean.FalseString);
        }
       // [Authorize]
        public ActionResult PostComment()
        {
           
            return PartialView("_CommentPartialView");
        }
       // [Authorize]
        public ActionResult GetComments(int noticeId)
        {
            var result = _noticeBoardManager.GetComments(noticeId);
            return PartialView("_commentsListPartial", result);
        }

        [Authorize]
        public ActionResult Edit(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View(_noticeBoardManager.GetEdit(id.Value));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(NoticeBoardViewModel Model)

        {
            if (ModelState.IsValid)
            {
                ViewBag.NoticeEdit = _noticeBoardManager.PostEdit(Model);


                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                        file.SaveAs(path);
                        var image = _data.SiteImages.FirstOrDefault(x => x.TypeId == Model.Notice.Id && x.Type == "Notice");

                        path = Path.Combine(Server.MapPath(image.ImagePath));
                        
                        System.IO.File.Delete(path);
                       
                        image.ImagePath = "/Content/images/" + fileName;

                        _data.SaveChanges();


                    }

                    


                }
                return RedirectToAction("Index");
            }
            return View(Model);
        }

        // GET: Notice/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           bool Result = _noticeBoardManager.DeleteNotice(id.Value);
            if (Result)
            {
                var image = _data.SiteImages.FirstOrDefault(x => x.TypeId == id && x.Type == "Notice");

                if (image != null && !image.ImagePath.Contains("defaultNotice"))
                {
                    var path = Path.Combine(Server.MapPath(image.ImagePath));

                    System.IO.File.Delete(path);

                    _data.SiteImages.Remove(image);
                    _data.SaveChanges();

                }
            }

            return Content(Result.ToString());
            
        }

       


    }
}