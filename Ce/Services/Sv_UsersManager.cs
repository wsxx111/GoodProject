using Core;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class Sv_UsersManager
    {
        /// <summary>
        /// 创建部分用户
        /// </summary>
        /// <returns></returns>
        public static PartUserInfo CreatePartGuest()
        {
            return new PartUserInfo
            {
                Uid = -1,
                UserName = "guest",
                Email = "",
                Mobile = "",
                Password = "",               
                NickName = "游客",
                Avatar = "",                              
                Salt = ""
            };
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserByUidAndPwd(int uid, string password)
        {
            PartUserInfo partUserInfo = null;
            if (uid > 0)
            {
              partUserInfo= DA_UsersManager.GetPartUserById(uid);
            }
            if (partUserInfo != null && partUserInfo.Password == password)
            {
                return partUserInfo;
            }
            return null;
        }       


        /// <summary>
        /// 更新用户最后访问
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="visitTime">访问时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        public static void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId)
        {
            DA_UsersManager.UpdateUserLastVisit(uid, visitTime, ip, regionId);
        }
    }
}
