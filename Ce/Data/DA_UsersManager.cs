using Core;
using Core.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DA_UsersManager
    {
        private static IUserNOSQLStrategy _usernosql = RDDBData.UserNOSQL;//用户非关系型数据库

        /// <summary>
        /// 更新用户最后访问
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <param name="visitTime">访问时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        public static void UpdateUserLastVisit(int uid, DateTime visitTime, string ip, int regionId)
        {
        //    SysData.UpdateUserLastVisit(uid, visitTime, ip, regionId);
        //    if (_usernosql != null)
        //        _usernosql.UpdateUserLastVisit(uid, visitTime, ip, regionId);
        }

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public static PartUserInfo GetPartUserById(int uid)
        {
            PartUserInfo partUserInfo = null;

            if (_usernosql != null)
            {
                partUserInfo = _usernosql.GetPartUserById(uid);
                if (partUserInfo == null)
                {
                    IDataReader reader = RDDBData.ReadDatabase.GetPartUserById(uid);
                    if (reader.Read())
                    {
                        partUserInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
                        partUserInfo.UserName = reader["username"].ToString();
                        partUserInfo.Email = reader["email"].ToString();
                        partUserInfo.Mobile = reader["mobile"].ToString();
                        partUserInfo.Password = reader["password"].ToString();                      
                        partUserInfo.NickName = reader["nickname"].ToString();
                        partUserInfo.Avatar = reader["avatar"].ToString();                       
                        partUserInfo.Salt = reader["salt"].ToString();
                    }
                    reader.Close();
                    if (partUserInfo != null)
                    {
                        _usernosql.CreatePartUser(partUserInfo);
                    }
                }
            }
            else
            {
                IDataReader reader = RDDBData.ReadDatabase.GetPartUserById(uid);
                if (reader.Read())
                {
                    partUserInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
                    partUserInfo.UserName = reader["username"].ToString();
                    partUserInfo.Email = reader["email"].ToString();
                    partUserInfo.Mobile = reader["mobile"].ToString();
                    partUserInfo.Password = reader["password"].ToString();
                    partUserInfo.NickName = reader["nickname"].ToString();
                    partUserInfo.Avatar = reader["avatar"].ToString();
                    partUserInfo.Salt = reader["salt"].ToString();
                }
                reader.Close();
            }
            return partUserInfo;
        }


    }
}
