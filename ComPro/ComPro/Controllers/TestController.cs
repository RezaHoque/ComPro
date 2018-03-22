using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComPro.Interfaces;
using ComPro.Models;
using static ComPro.Models.Enums;

namespace ComPro.Controllers
{

   
    public class TestController : Controller
    {
        private MyPoint _v1;
        private MyPoint _v2;
        private MyPoint _v3;

        public TestController (MyPoint v1, MyPoint v2, MyPoint v3)
		{
			this._v1 = v1;
			this._v2 = v2;
			this._v3 = v3;
		}

        public TestController(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            this._v1 = new MyPoint
            {
                x = x1,
                y = y1
            };
            this._v2 = new MyPoint
            {
                x = x2,
                y = y2
            };
            this._v3 = new MyPoint
            {
                x = x3,
                y = y3
            };
        }



        	

    

    IUtility _Utility = new UtilityManager();
        public ActionResult Index()
        {
            double Perimeter = _Utility.GetPerimeter(_v1, _v2, _v3);

            string MyString = _Utility.GetString(_v1, _v2, _v3);

            var printtype = _Utility.printtype(_v1, _v2, _v3);

            return View();
           
        }
        

    }
}