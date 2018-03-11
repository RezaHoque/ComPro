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
        public ActionResult Index(string Search)
        {
           
            if (Search == null)
                ViewBag.search = Helpers.Constants.EmptyText;

            else
            {
             ViewBag.SearchData = Search;
            return View(_searchManager.SearchData(Search));
            }
                


            return View();
                }

        
    }
}