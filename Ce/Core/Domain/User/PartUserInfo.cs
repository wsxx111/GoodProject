using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class PartUserInfo
    {
        private int _uid;//用户id
        private string _username = "";//用户名称
        private string _email = "";//用户邮箱
        private string _mobile = "";//用户手机
        private string _password = "";//用户密码
        private string _nickname = "";//用户昵称
        private string _avatar;//用户头像
        private string _salt;//盐值

        /// <summary>
        ///用户id
        /// </summary>
        public int Uid
        {
            set { _uid = value; }
            get { return _uid; }
        }
        /// <summary>
        ///用户名称
        /// </summary>
        public string UserName
        {
            set { _username = value.TrimEnd(); }
            get { return _username; }
        }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email
        {
            set { _email = value.TrimEnd(); }
            get { return _email; }
        }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string Mobile
        {
            set { _mobile = value.TrimEnd(); }
            get { return _mobile; }
        }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string Password
        {
            set { _password = value.TrimEnd(); }
            get { return _password; }
        }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value.TrimEnd(); }
            get { return _nickname; }
        }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value.TrimEnd(); }
        }
        ///<summary>
        ///盐值
        ///</summary>
        public string Salt
        {
            get { return _salt; }
            set { _salt = value; }
        }
    }
}
