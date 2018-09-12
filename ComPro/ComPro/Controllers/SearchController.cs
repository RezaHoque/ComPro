using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using static ComPro.Models.Enums;

namespace ComPro.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearch _searchManager;
        public SearchController(ISearch searchManager)
        {
            _searchManager = searchManager;
        }


        // GET: Search
        public ActionResult Index(string Text)
        {
           
            if (Text == null)
                ViewBag.search = Helpers.Constants.EmptyText;

            else
            {
             ViewBag.SearchData = Text;
            return View(_searchManager.SearchData(Text));
            }
                


            return View();
                }

        
    }
}