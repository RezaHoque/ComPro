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

namespace ComPro.Controllers
{
    [Authorize]
    public class NoticeBoardController : Controller
    {
        private readonly INoticeBoard _noticeBoardManager;
        private ApplicationUserManager _userManager;
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
                    Notice=n,
                    TotalComments=totalComnts
                });
            }

            return View(noticeVMList);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(NoticeBoard model)
        {
                        
            if (model.Title == null || model.Description == null)
            {
                ViewBag.massage = Helpers.Constants.WarningToNoticeCreatorMessage;
                return View();
            }
            else
            {
                model.CreatorId = User.Identity.GetUserId();
                TempData["Massage"] = _noticeBoardManager.PostNotices(model);
            }



            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Notice(int? id)
        {
            ViewBag.NoticeId = id;
            return View(_noticeBoardManager.GetDetails(id.Value));
        }

        public ActionResult ApproveNotice(int id)
        {
            if (User.IsInRole(UserRole.Administrator.ToString()))
            {
                _noticeBoardManager.ApproveNotice(id);
                return RedirectToAction("NewNotice");
            }
            else
                return RedirectToAction("Index");

        }

        public ActionResult NewNotice()
        {
            if(User.IsInRole(UserRole.Administrator.ToString()))
                return View(_noticeBoardManager.GetNewNotices());
            else
                return RedirectToAction("Index");
        }
        [HttpPost]
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
        [Authorize]
        public ActionResult PostComment()
        {
           
            return PartialView("_CommentPartialView");
        }
        [Authorize]
        public ActionResult GetComments(int noticeId)
        {
            var result = _noticeBoardManager.GetComments(noticeId);
            return PartialView("_commentsListPartial", result);
        }

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
        public ActionResult Edit(NoticeBoard Model)

        {
            if (ModelState.IsValid)
            {
                ViewBag.NoticeEdit = _noticeBoardManager.PostEdit(Model);

                return RedirectToAction("Index");
            }
            return View(Model);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(_noticeBoardManager.GetEdit(id.Value));
            
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           ViewBag.Delete = _noticeBoardManager.PostDelete(id);
           return RedirectToAction("Index");

        }


    }
}