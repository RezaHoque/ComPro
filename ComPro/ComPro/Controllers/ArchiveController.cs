using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using ComPro.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ComPro.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly IArchive _archive;

        public ArchiveController(IArchive archive)
        {
            _archive = archive;
        }
        // GET: Archive
        public ActionResult Index()
        {
            var files = _archive.GetArchiveContents().OrderByDescending(x=>x.UploadDate).ToList();
            return View(files);
        }
        [Authorize(Roles = "Administrator")]
        public ActionResult Upload()
        {
            return View();
        }

        [Authorize (Roles = "Administrator")]
        [HttpPost]
        public ActionResult Upload(IEnumerable<HttpPostedFileBase> files, ArchiveModel model)
        {
            if (model != null && files.Any())
            {
                var arc = _archive.CreateArchive(model);
                _archive.UploadFiles(files,arc);
            }
           
            return RedirectToAction("Index");
        }

        public ActionResult Download(string file)
        {
            _archive.DownloadFile(file);
            return new EmptyResult();
        }
     
        
    }
}