using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ajax.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var ddd = Request["multiple"];
            string result = "successData";
            return Json(result);
        }

    }
}
