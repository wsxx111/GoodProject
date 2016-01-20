using Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;

namespace Data
{
    public class DA_OnlineUsers
    {
        #region 创建

        /// <summary>
        /// 从IDataReader创建OnlineUserInfo
        /// </summary>
        public static OnlineUserInfo BuildOnlineUserFromReader(IDataReader reader)
        {
            OnlineUserInfo onlineUserInfo = new OnlineUserInfo();

            onlineUserInfo.OlId = TypeHelper.ObjectToInt(reader["olid"]);
            onlineUserInfo.Uid = TypeHelper.ObjectToInt(reader["uid"]);
            onlineUserInfo.Sid = reader["sid"].ToString();
            onlineUserInfo.NickName = reader["nickname"].ToString();
            onlineUserInfo.IP = reader["ip"].ToString();
            onlineUserInfo.RegionId = TypeHelper.ObjectToInt(reader["regionid"]);
            onlineUserInfo.UpdateTime = TypeHelper.ObjectToDateTime(reader["updatetime"]);

            return onlineUserInfo;
        }

        

        /// <summary>
        /// 创建在线用户
        /// </summary>
        public static int CreateOnlineUser(OnlineUserInfo onlineUserInfo)
        {
            return RDDBData.ReadDatabase.CreateOnlineUser(onlineUserInfo);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新在线用户ip
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">ip</param>
        public static void UpdateOnlineUserIP(int olId, string ip)
        {
            RDDBData.ReadDatabase.UpdateOnlineUserIP(olId, ip);
        }

        /// <summary>
        /// 更新在线用户uid
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">uid</param>
        public static void UpdateOnlineUserUid(int olId, int uid)
        {
            RDDBData.ReadDatabase.UpdateOnlineUserUid(olId, uid);
        }

        /// <summary>
        /// 重置在线用户表
        /// </summary>
        public static void ResetOnlineUserTable()
        {
            RDDBData.ReadDatabase.ResetOnlineUserTable();
        }



        #endregion

        #region 删除

        /// <summary>
        /// 删除过期在线用户
        /// </summary>
        /// <param name="onlineUserExpire">过期时间</param>
        public static void DeleteExpiredOnlineUser(int onlineUserExpire)
        {
            RDDBData.ReadDatabase.DeleteExpiredOnlineUser(onlineUserExpire);
        }
        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        public static void DeleteOnlineUserBySid(string sid)
        {
            RDDBData.ReadDatabase.DeleteOnlineUserBySid(sid);
        }

        #endregion

        #region 获取
        /// <summary>
        /// 根据会话Id获得在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <returns></returns>
        public static OnlineUserInfo GetOnlineUserBySid(string sid)
        {
            OnlineUserInfo onlineUserInfo = null;
            IDataReader reader = RDDBData.ReadDatabase.GetOnlineUserBySid(sid);
            if (reader.Read())
            {
                onlineUserInfo = BuildOnlineUserFromReader(reader);
            }

            reader.Close();
            return onlineUserInfo;
        }

        /// <summary>
        /// 根据usertype获得在线用户数量
        /// </summary>
        /// <param name="userType">在线用户类型</param>
        /// <returns></returns>
        public static int GetOnlineUserCount(int userType)
        {
            return RDDBData.ReadDatabase.GetOnlineUserCount(userType);
        }



        /// <summary>
        /// 获得在线用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetOnlineUserListSort(string sortColumn, string sortDirection)
        {
            return RDDBData.ReadDatabase.GetOnlineUserListSort(sortColumn, sortDirection);
        }

        /// <summary>
        /// 根据location获得在线用户数量
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        public static int GetOnlineUserCount(int locationType, int locationId)
        {
            return RDDBData.ReadDatabase.GetOnlineUserCount(locationType, locationId);
        }

        /// <summary>
        /// 获得在线用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        public static List<OnlineUserInfo> GetOnlineUserList(int pageSize, int pageNumber, int locationType, int locationId, string sort)
        {
            List<OnlineUserInfo> onlineUserList = new List<OnlineUserInfo>();
            IDataReader reader = RDDBData.ReadDatabase.GetOnlineUserList(pageSize, pageNumber, locationType, locationId, sort);
            while (reader.Read())
            {
                OnlineUserInfo onlineUserInfo = BuildOnlineUserFromReader(reader);
                onlineUserList.Add(onlineUserInfo);
            }

            reader.Close();
            return onlineUserList;
        }


        #endregion


    }
}
