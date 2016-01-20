using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeShi.Controllers.Admin
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult AddProduct()
        {
            return View();
        }

    }
}
