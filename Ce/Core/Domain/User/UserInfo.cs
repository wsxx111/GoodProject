using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class UserInfo:PartUserInfo
    {
        private DateTime _lastvisittime = DateTime.Now;//最后访问时间
        private string _lastvisitip = "";//最后访问ip
        private int _lastvisitrgid = -1;//最后访问区域id

        private DateTime _registertime = DateTime.Now;//用户注册时间
        private string _registerip = "";//用户注册ip
        private int _registerrgid = -1;//用户注册区域id

        private int _gender = 0;//用户性别(0代表未知，1代表男，2代表女)
        private string _realname = "";//用户真实名称
        private DateTime _bday = DateTime.Now;//用户出生日期
        private string _idcard = "";//身份证号
        private int _regionid = 0;//区域id
        private string _address = "";//所在地
        private string _bio = "";//简介


        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastVisitTime
        {
            set { _lastvisittime = value; }
            get { return _lastvisittime; }
        }
        /// <summary>
        /// 最后访问ip
        /// </summary>
        public string LastVisitIP
        {
            set { _lastvisitip = value; }
            get { return _lastvisitip; }
        }
        /// <summary>
        /// 最后访问区域id
        /// </summary>
        public int LastVisitRgId
        {
            set { _lastvisitrgid = value; }
            get { return _lastvisitrgid; }
        }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime RegisterTime
        {
            set { _registertime = value; }
            get { return _registertime; }
        }
        /// <summary>
        /// 用户注册ip
        /// </summary>
        public string RegisterIP
        {
            set { _registerip = value; }
            get { return _registerip; }
        }
        /// <summary>
        /// 用户注册区域id
        /// </summary>
        public int RegisterRgId
        {
            set { _registerrgid = value; }
            get { return _registerrgid; }
        }
        ///<summary>
        ///用户性别(0代表未知，1代表男，2代表女)
        ///</summary>
        public int Gender
        {
            get { return _gender; }
            set { _gender = value; }
        }
        /// <summary>
        /// 用户真实名称
        /// </summary>
        public string RealName
        {
            set { _realname = value; }
            get { return _realname; }
        }
        ///<summary>
        ///用户出生日期
        ///</summary>
        public DateTime Bday
        {
            get { return _bday; }
            set { _bday = value; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard
        {
            set { _idcard = value; }
            get { return _idcard; }
        }
        ///<summary>
        ///区域id
        ///</summary>
        public int RegionId
        {
            get { return _regionid; }
            set { _regionid = value; }
        }
        ///<summary>
        ///所在地
        ///</summary>
        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        ///<summary>
        ///简介
        ///</summary>
        public string Bio
        {
            get { return _bio.TrimEnd(); }
            set { _bio = value; }
        }

    }
}
