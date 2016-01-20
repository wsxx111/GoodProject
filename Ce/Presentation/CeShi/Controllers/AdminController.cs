using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeShi.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 导航栏
        /// </summary>
        public ActionResult NavBar()
        {      
            return View();
        }

        /// <summary>
        /// 菜单栏
        /// </summary>
        public ActionResult Menu()
        {           
            return View();
        }

        /// <summary>
        /// 商城运行信息
        /// </summary>
        public ActionResult RunInfo()
        {
         
            return View();
        }
        /// <summary>
        /// 底部
        /// </summary>
        /// <returns></returns>
        public ActionResult Bottom()
        {
            return View();
        }

    }
}
