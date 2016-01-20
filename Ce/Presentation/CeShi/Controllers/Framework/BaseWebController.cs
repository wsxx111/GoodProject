using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Core.Helper;
using Core;

namespace Ce
{
    public class BaseWebController : Controller
    {
        public WebWorkContext WorkContext = new WebWorkContext();

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.ValidateRequest = false;

            WorkContext.IsHttpAjax = WebHelper.IsAjax();
            WorkContext.IP = WebHelper.GetIP();
            WorkContext.Url = WebHelper.GetUrl();
            WorkContext.UrlReferrer = WebHelper.GetUrlReferrer();

            //获得用户唯一标示符sid
            WorkContext.Sid = Sv_OnlineUsers.GetSidCookie();
            if (WorkContext.Sid.Length == 0)
            {
                //生成sid
                WorkContext.Sid = Sessions.GenerateSid();
                //将sid保存到cookie中
                Sv_OnlineUsers.SetSidCookie(WorkContext.Sid);
            }

            PartUserInfo partUserInfo=new PartUserInfo();

            //获得用户id
            int uid = Sv_OnlineUsers.GetUidCookie();
            if (uid < 1)//当用户为游客时
            {
                //创建游客
                partUserInfo = Sv_UsersManager.CreatePartGuest();
            }
            else {
                //获得保存在cookie中的密码
                string encryptPwd = Sv_OnlineUsers.GetCookiePassword();
                //防止用户密码被篡改为危险字符
                if (encryptPwd.Length == 0 || !SecureHelper.IsBase64String(encryptPwd))
                {
                    //创建游客
                    partUserInfo = Sv_UsersManager.CreatePartGuest();
                    encryptPwd = string.Empty;
                    Sv_OnlineUsers.SetUidCookie(-1);
                    Sv_OnlineUsers.SetCookiePassword("");
                }
                else
                {
                    partUserInfo = Sv_UsersManager.GetPartUserByUidAndPwd(uid, Sv_OnlineUsers.DecryptCookiePassword(encryptPwd));
                }
                WorkContext.EncryptPwd = encryptPwd;
            }
            WorkContext.PartUserInfo = partUserInfo;

            WorkContext.Uid = partUserInfo.Uid;
            WorkContext.UserName = partUserInfo.UserName;
            WorkContext.UserEmail = partUserInfo.Email;
            WorkContext.UserMobile = partUserInfo.Mobile;
            WorkContext.Password = partUserInfo.Password;
            WorkContext.NickName = partUserInfo.NickName;
            WorkContext.Avatar = partUserInfo.Avatar;

            //在线总人数
            WorkContext.OnlineUserCount = Sv_OnlineUsers.GetOnlineUserCount();
            //在线游客数
            WorkContext.OnlineGuestCount = Sv_OnlineUsers.GetOnlineGuestCount();
            //在线会员数
            WorkContext.OnlineMemberCount = WorkContext.OnlineUserCount - WorkContext.OnlineGuestCount;            
        }

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
                return;
            
            //商城已经关闭
            if (WorkContext.WebSysConfig.IsClosed == 1)
            {
                filterContext.Result = PromptView(WorkContext.WebSysConfig.CloseReason);                
                return;
            }

            //当前时间为禁止访问时间
            if (ValidateHelper.BetweenPeriod(WorkContext.WebSysConfig.BanAccessTime))
            {
                filterContext.Result = PromptView("当前时间不能访问本商城");
                return;
            }

            //    //当用户ip不在允许的后台访问ip列表时
            if (!string.IsNullOrEmpty(WorkContext.WebSysConfig.AdminAllowAccessIP) && !ValidateHelper.InIPList(WorkContext.IP, WorkContext.WebSysConfig.AdminAllowAccessIP))
            {
                if (WorkContext.IsHttpAjax)
                {
                    filterContext.Result = AjaxResult("404", "您访问的网址不存在");
                }
                else
                {
                    // filterContext.Result = new RedirectResult("/");
                    filterContext.Result = PromptView("您的ip不在网址运行范围之内！");
                }
                return;
            }

            //    //当用户IP被禁止时
            if (Sv_BannedIPs.CheckIP(WorkContext.IP))
            {
                if (WorkContext.IsHttpAjax)
                    filterContext.Result = AjaxResult("404", "您访问的网址不存在");
                else
                {
                    //  filterContext.Result = new RedirectResult("/");
                    filterContext.Result = PromptView("您的ip被网址禁止！");
                }
                return;
            }


            //       //判断目前访问人数是否达到允许的最大人数
            if (WorkContext.OnlineUserCount > WorkContext.WebSysConfig.MaxOnlineCount)
            {
                filterContext.Result = PromptView("人数达到访问上限, 请稍等一会再访问！");
                return;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
            {
                return;
            }
            //页面开始执行时间
            WorkContext.StartExecuteTime = DateTime.Now;

        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //不能应用在子方法上
            if (filterContext.IsChildAction)
            {
                return;
            }
            //页面执行时间
            WorkContext.ExecuteTime = DateTime.Now.Subtract(WorkContext.StartExecuteTime).TotalMilliseconds / 1000;
        }


        protected override void OnException(ExceptionContext filterContext)
        {
            IOHelper.WriteLogFile(filterContext.Exception);
            if (WorkContext.IsHttpAjax)
            {
                filterContext.Result = AjaxResult("error", "系统错误,请联系管理员");
            }
            else
            {
                filterContext.Result = new ViewResult() { ViewName = "error" };
            }
        }


        #region 辅助方法

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string message)
        {
            return View("prompt", new PromptModel(message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected ViewResult PromptView(string backUrl, string message)
        {
            return View("prompt", new PromptModel(backUrl, message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <param name="isAutoBack">是否自动返回</param>
        /// <returns></returns>
        protected ViewResult PromptView(string backUrl, string message, bool isAutoBack)
        {
            return View("prompt", new PromptModel(backUrl, message) { IsAutoBack = isAutoBack });
        }

        /// <summary>
        /// ajax请求结果
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        protected ActionResult AjaxResult(string state, string content)
        {
            return Content(string.Format("{0}\"state\":\"{1}\",\"content\":\"{2}\"{3}", "{", state, content, "}"));
        }

        #endregion

    }
}