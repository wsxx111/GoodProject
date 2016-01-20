using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    [Serializable]
    public class WebConfig
    {
        #region 访问控制

        private static int _isclosed = 0;//是否关闭商城(0代表打开，1代表关闭)
        private static string _closereason = "";//商城关闭原因
        private static string _banaccesstime = "";//禁止访问时间
        private static string _banaccessip = "";//禁止访问ip
        private static string _allowaccessip = "";//允许访问ip
        private static string _adminallowaccessip = "";//后台允许访问ip
        private static string _secretkey = "12345678";//密钥
        private static string _cookiedomain = "";//cookie的有效域
        private static string _randomlibrary = "";//随机库
        private static string _verifypages = "";//使用验证码的页面
        private static string _ignorewords = "";//忽略词
        private static string _allowemailprovider = "";//允许的邮箱提供者
        private static string _banemailprovider = "";//禁止的邮箱提供者

        /// <summary>
        /// 是否关闭商城(0代表打开，1代表关闭)
        /// </summary>
        public int IsClosed
        {
            get { return _isclosed; }
            set { _isclosed = value; }
        }

        /// <summary>
        /// 商城关闭原因
        /// </summary>
        public string CloseReason
        {
            get { return _closereason; }
            set { _closereason = value; }
        }

        /// <summary>
        /// 禁止访问时间
        /// </summary>
        public string BanAccessTime
        {
            get { return _banaccesstime; }
            set { _banaccesstime = value; }
        }

        /// <summary>
        /// 禁止访问ip
        /// </summary>
        public string BanAccessIP
        {
            get { return _banaccessip; }
            set { _banaccessip = value; }
        }

        /// <summary>
        /// 允许访问ip
        /// </summary>
        public string AllowAccessIP
        {
            get { return _allowaccessip; }
            set { _allowaccessip = value; }
        }

        /// <summary>
        /// 后台允许访问ip
        /// </summary>
        public string AdminAllowAccessIP
        {
            get { return _adminallowaccessip; }
            set { _adminallowaccessip = value; }
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public string SecretKey
        {
            get { return _secretkey; }
            set { _secretkey = value; }
        }

        /// <summary>
        /// cookie的有效域
        /// </summary>
        public string CookieDomain
        {
            get { return _cookiedomain; }
            set { _cookiedomain = value; }
        }

        /// <summary>
        /// 随机库
        /// </summary>
        public string RandomLibrary
        {
            get { return _randomlibrary; }
            set { _randomlibrary = value; }
        }

        /// <summary>
        /// 使用验证码的页面
        /// </summary>
        public string VerifyPages
        {
            get { return _verifypages; }
            set { _verifypages = value; }
        }

        /// <summary>
        /// 忽略词
        /// </summary>
        public string IgnoreWords
        {
            get { return _ignorewords; }
            set { _ignorewords = value; }
        }

        /// <summary>
        /// 允许的邮箱提供者
        /// </summary>
        public string AllowEmailProvider
        {
            get { return _allowemailprovider; }
            set { _allowemailprovider = value; }
        }

        /// <summary>
        /// 禁止的邮箱提供者
        /// </summary>
        public string BanEmailProvider
        {
            get { return _banemailprovider; }
            set { _banemailprovider = value; }
        }

        #endregion

        #region 性能设置

        private static string _imagecdn = "";//图片cdn(不能以"/"结尾)
        private static string _csscdn = "";//csscdn(不能以"/"结尾)
        private static string _scriptcdn = "";//脚本cdn(不能以"/"结尾)
        private static int _onlineuserexpire = 10;//在线用户过期时间(单位为分钟)
        private static int _maxonlinecount = 1000;//最大在线人数
        private static int _onlinecountexpire = 5;//在线人数缓存时间(单位为分钟,0代表即时数量)
        private static int _updateonlinetimespan = 5;//更新用户在线时间间隔(单位为分钟,0代表不更新)
        private static string _runstate ="";   //Develope=开发，Publish=发布
        private static string _version = "";

        /// <summary>
        /// 图片cdn(不能以"/"结尾)
        /// </summary>
        public string ImageCDN
        {
            get { return _imagecdn; }
            set { _imagecdn = value; }
        }

        /// <summary>
        /// csscdn(不能以"/"结尾)
        /// </summary>
        public string CSSCDN
        {
            get { return _csscdn; }
            set { _csscdn = value; }
        }

        /// <summary>
        /// 脚本cdn(不能以"/"结尾)
        /// </summary>
        public string ScriptCDN
        {
            get { return _scriptcdn; }
            set { _scriptcdn = value; }
        }

        /// <summary>
        /// 在线用户过期时间(单位为分钟)
        /// </summary>
        public int OnlineUserExpire
        {
            get { return _onlineuserexpire; }
            set { _onlineuserexpire = value; }
        }

        /// <summary>
        /// 最大在线人数
        /// </summary>
        public int MaxOnlineCount
        {
            get { return _maxonlinecount; }
            set { _maxonlinecount = value; }
        }

        /// <summary>
        /// 在线人数缓存时间(单位为分钟,0代表即时数量)
        /// </summary>
        public int OnlineCountExpire
        {
            get { return _onlinecountexpire; }
            set { _onlinecountexpire = value; }
        }

        /// <summary>
        /// 更新用户在线时间间隔(单位为分钟,0代表不更新)
        /// </summary>
        public int UpdateOnlineTimeSpan
        {
            get { return _updateonlinetimespan; }
            set { _updateonlinetimespan = value; }
        }

        public string RunState
        {
            get { return _runstate; }
            set { _runstate = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        #endregion

    }
}
