﻿using System;
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
                    Notice = n,
                    TotalComments = totalComnts,
                    NoticeImage = _noticeBoardManager.GetNoticeImage(n.Id,"Notice")
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
            model.CreatorId = User.Identity.GetUserId();
           
            var notice = _noticeBoardManager.PostNotices(model);

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid()+"_"+Path.GetFileName(file.FileName);
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