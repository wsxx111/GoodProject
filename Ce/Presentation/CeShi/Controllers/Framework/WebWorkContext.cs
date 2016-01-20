using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ce
{
    public class WebWorkContext
    {
        public bool IsHttpAjax;//当前请求是否为ajax请求

        public string IP;//用户ip

        public string Url;//当前url

        public string UrlReferrer;//上一次访问的url

        public WebConfig WebSysConfig = SystemConfigData.WebConfig;//网站配置信息  

        public string Sid;//用户sid

        public int Uid = -1;//用户id

        public string UserName;//用户名      

        public string NickName;//用户昵称

        public string UserEmail;//用户邮箱

        public string UserMobile;//用户手机号      

        public string Avatar;//用户头像

        public string Password;//用户密码

        public PartUserInfo PartUserInfo;//用户信息

        public int OnlineUserCount = 0;//在线总人数

        public int OnlineMemberCount = 0;//在线会员数

        public int OnlineGuestCount = 0;//在线游客数

        public DateTime StartExecuteTime;//页面开始执行时间

        public double ExecuteTime;//页面执行时间

        public string EncryptPwd;//加密密码
        
    }
}