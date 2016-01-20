using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CeShi.Controllers.Admin
{
    public class ParameterController : Controller
    {
        public List<ParameterInfo> paralist = new List<ParameterInfo>();
        public ActionResult Index()
        {
            init();
            return View(paralist);
        }

        public void init()
        {
            ParameterInfo para1 = new ParameterInfo() { ParaID=1,ParaName = "service", ParaType = "string(20)", ParaIsNull = 0, ParaDescription = "服务名，必填，且必须在User_ServiceInfo表里存在该服务名（ServiceName字段）", ParaExample = "" };
            ParameterInfo para2 = new ParameterInfo() { ParaID=2,ParaName = "username", ParaType = "string(20)", ParaIsNull = 1, ParaDescription = "用户名，userid和username至少传一个，不能有单引号", ParaExample = "" };
            ParameterInfo para3 = new ParameterInfo() { ParaID = 3, ParaName = "userid", ParaType = "string(20)", ParaIsNull = 1, ParaDescription = "用户ID，userid和username至少传一个，纯数字", ParaExample = "" };
            ParameterInfo para4 = new ParameterInfo() { ParaID = 4, ParaName = "soufuncard", ParaType = "string(20)", ParaIsNull = 1, ParaDescription = "是否返回搜房卡信息，1是，可选", ParaExample = "" };
            paralist.Add(para1);
            paralist.Add(para2);
            paralist.Add(para3);
            paralist.Add(para4);
        }

    }



    public class ParameterInfo
    {
        public int ParaID { get; set; }
        public string ParaName { get; set; }
        public string ParaType { get; set; }
        public int ParaIsNull { get; set; }
        public string ParaDescription { get; set; }
        public string ParaExample { get; set; }
    }


}
