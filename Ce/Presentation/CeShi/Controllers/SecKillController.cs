using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Core.Helper;

namespace CeShi.Controllers
{
    // localhost:6335/SecKill/Index?Pid=1764&city=sh

    public class SecKillController : Controller
    {
        #region 活动信息相关成员
        //spikeProj表
        protected string ProjId = "";
        protected string ProjName = "";
        //spikeActivity表
        protected string RunningId = "";
        protected string RunningspikeTime = "";
        protected string RunningpicLink = "";
        protected string RunningheadImgUrl = "";
        protected string RunningpicUrl = "";
        protected string RunningselNum = "";
        protected string Runningprize = "";
        protected string RunningwinnerShow = "";
        protected string RunningnowinnerShow = "";
        //其他
        protected string errorMsg = "";
        protected string hidPhone = "";

        #endregion

        #region 活动中判断相关状态成员
        protected string Cmd = "";
        protected bool IsRunning = false; //是否活动正在进行中
        protected bool IsUserlogin = false;//是否用户已报名，处于登录状态
        protected bool IsJustEnd = false;//是否上次活动刚结束，且结束还没超过10分钟
        protected bool IsCheckCode = false;//是否启用验证码
        #endregion

        public ActionResult Index()
        {          
            return View();
        }

      
        public ActionResult ReturnNowTime()
        {
            string timeStr = "", nowtime = "";
            if (WebHelper.GetRequestString("id").Trim() != "" && Regex.IsMatch(WebHelper.GetRequestString("id").Trim(), @"^[1-9][0-9]*$"))
            {
                RunningId = WebHelper.GetRequestString("id").Trim();
            }         
            if (Regex.IsMatch(RunningId, @"^[1-9][0-9]*$"))
            {
                DataTable dt = DBHelper.GetDataTable("select top 1 *,getdate() as nowtime from spikeActivity where id=" + RunningId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    timeStr = dt.Rows[0]["spikeTime"].ToString();
                    nowtime = dt.Rows[0]["nowtime"].ToString();

                    if (!String.IsNullOrEmpty(timeStr))
                    {
                        bool flag = true;
                        if (DateTime.Parse(nowtime) <= DateTime.Parse(timeStr))
                        {
                            flag = false;
                            return Content((DateTime.Parse(timeStr) - DateTime.Parse(nowtime)).TotalSeconds.ToString("f0") + ",1");
                        }
                        if (flag) //已结束
                        {
                            return Content("-1");
                        }
                    }
                    return Content("db");
                }
                else
                {
                    return Content("db");
                }
            }
            else
            {
                return Content("err");
            }
        }     
    }
}
