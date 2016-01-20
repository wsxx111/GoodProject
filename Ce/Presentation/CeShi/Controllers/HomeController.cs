using Services;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Helper;

namespace Ce.Controllers
{
    public class HomeController : BaseWebController
    {       
        public ActionResult Index()
        {                           
            return View(WorkContext);                       
        }

        public ActionResult CheckLogin()
        {         
            string getUsername = WebHelper.GetRequestString("username");
            string getUserpwd = WebHelper.GetRequestString("userpwd");
            //判断是不是含有该用户名
            if (Sv_Login.CheckExistThisUser(getUsername))
            {
                IOHelper.WriteLog(getUsername+"登录成功");
                return Content("登录成功，用户名为"+getUsername+",密码为"+ getUserpwd);               
            }
            return Content("不存在此用户");                    
        }
   
    }

}
