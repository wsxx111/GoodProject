using Core;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Sv_Emails
    {
        private static object _locker = new object();//锁对象
        private static IEmailStrategy _iemailstrategy = null;//邮件策略
        private static EmailConfigInfo _emailconfiginfo = null;//邮件配置信息

        static Sv_Emails()
        {
            _iemailstrategy = SysEmail.Instance;
            _emailconfiginfo = SystemConfigData.EmailConfig;

            _iemailstrategy.Host = _emailconfiginfo.Host;
            _iemailstrategy.Port = _emailconfiginfo.Port;
            _iemailstrategy.UserName = _emailconfiginfo.UserName;
            _iemailstrategy.Password = _emailconfiginfo.Password;
            _iemailstrategy.From = _emailconfiginfo.From;
            _iemailstrategy.FromName = _emailconfiginfo.FromName;
        }

        /// <summary>
        /// 重置邮件配置信息
        /// </summary>
        public static void ResetEmail()
        {
            lock (_locker)
            {
                _emailconfiginfo = SystemConfigData.EmailConfig;
                _iemailstrategy.Host = _emailconfiginfo.Host;
                _iemailstrategy.Port = _emailconfiginfo.Port;
                _iemailstrategy.UserName = _emailconfiginfo.UserName;
                _iemailstrategy.Password = _emailconfiginfo.Password;
                _iemailstrategy.From = _emailconfiginfo.From;
                _iemailstrategy.FromName = _emailconfiginfo.FromName;
            }
        }

        /// <summary>
        /// 发送找回密码邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <param name="userName">接收人</param>
        /// <param name="url">url</param>
        public static bool SendFindPwdEmail(string to, string userName, string url)
        {
            //标题
            string subject ="找回密码邮件测试";

            StringBuilder body = new StringBuilder(_emailconfiginfo.FindPwdBody);
            body.Replace("{shopname}", "网上商城");
            body.Replace("{siteurl}", "http://www.ddd.com");
            body.Replace("{username}", userName);
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", url);

            return _iemailstrategy.Send(to, subject, body.ToString());
        }

        /// <summary>
        /// 安全中心发送验证邮箱邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <param name="userName">接收人</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static bool SendSCVerifyEmail(string to, string userName, string url)
        {
            string subject = "安全中心邮箱验证提醒测试";

            StringBuilder body = new StringBuilder(_emailconfiginfo.SCVerifyBody);
            body.Replace("{shopname}", "网上商城");
            body.Replace("{siteurl}", "http://www.ddd.com");
            body.Replace("{username}", userName);
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", url);

            return _iemailstrategy.Send(to, subject, body.ToString());
        }

        /// <summary>
        /// 安全中心发送确认更新邮箱邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <param name="userName">接收人</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        public static bool SendSCUpdateEmail(string to, string userName, string url)
        {
            string subject = "安全中心邮箱确认提醒测试";

            StringBuilder body = new StringBuilder(_emailconfiginfo.SCUpdateBody);
            body.Replace("{shopname}","网上商城");
            body.Replace("{siteurl}", "http://www.ddd.com");
            body.Replace("{username}", userName);
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", url);

            return _iemailstrategy.Send(to, subject, body.ToString());
        }

        /// <summary>
        /// 发送注册欢迎邮件
        /// </summary>
        /// <param name="to">接收邮箱</param>
        /// <returns></returns>
        public static bool SendWebcomeEmail(string to)
        {
            string subject = "恭喜您成功注册为{0}会员测试";

            StringBuilder body = new StringBuilder(_emailconfiginfo.WebcomeBody);
            body.Replace("{shopname}", "网上商城");
            body.Replace("{regtime}", CommonHelper.GetDateTime());
            body.Replace("{email}", to);

            return _iemailstrategy.Send(to, subject, body.ToString());

        }
    }
}
