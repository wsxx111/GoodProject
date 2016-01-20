using Core;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;

namespace Services
{
    public class Sv_OnlineUsers
    {
        //锁对象
        private static object _locker = new object();
        //最后一次删除过期在线用户的时间
        private static int _lastdeleteexpiredonlineuserstime = 0;

        #region 创建
        /// <summary>
        /// 创建在线会员
        /// </summary>
        /// <param name="uid">会员id</param>
        /// <param name="sid">sessionId</param>
        /// <param name="nickName">会员昵称</param>
        /// <param name="updateTime">更新时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        public static int CreateOnlineMember(int uid, string sid, string nickName, DateTime updateTime, string ip, int regionId)
        {
            OnlineUserInfo onlineUserInfo = new OnlineUserInfo();
            onlineUserInfo.Uid = uid;
            onlineUserInfo.Sid = sid;
            onlineUserInfo.NickName = nickName;
            onlineUserInfo.IP = ip;
            onlineUserInfo.RegionId = regionId;
            onlineUserInfo.UpdateTime = updateTime;

            int olid = DA_OnlineUsers.CreateOnlineUser(onlineUserInfo);

            //更新用户最后访问信息
        //    Sv_UsersManager.UpdateUserLastVisit(uid, updateTime, ip, regionId);
            return olid;
        }

        /// <summary>
        /// 创建在线游客
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <param name="updateTime">更新时间</param>
        /// <param name="ip">ip</param>
        /// <param name="regionId">区域id</param>
        /// <returns></returns>
        public static int CreateOnlineGuest(string sid, DateTime updateTime, string ip, int regionId)
        {
            OnlineUserInfo onlineUserInfo = new OnlineUserInfo();
            onlineUserInfo.Uid = -1;
            onlineUserInfo.Sid = sid;
            onlineUserInfo.NickName = "游客";
            onlineUserInfo.IP = ip;
            onlineUserInfo.RegionId = regionId;
            onlineUserInfo.UpdateTime = updateTime;

            return DA_OnlineUsers.CreateOnlineUser(onlineUserInfo);
        }

        #endregion

        #region 更新

        /// <summary>
        /// 根据老的uid，更新在线用户uid
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">uid</param>
        public static void UpdateOnlineUserUid(int olId, int uid)
        {
            DA_OnlineUsers.UpdateOnlineUserUid(olId, uid);
        }

        

        /// <summary>
        /// 更新在线用户
        /// </summary>
        /// <param name="state">UpdateOnlineUserState类型对象</param>
        public static void UpdateOnlineUser(object state)
        {
            lock (_locker)
            {
                UpdateOnlineUserState updateOnlineUserState = (UpdateOnlineUserState)state;

                OnlineUserInfo onlineUserInfo = GetOnlineUserBySid(updateOnlineUserState.Sid);
                if (onlineUserInfo != null)
                {
                    if (onlineUserInfo.IP != updateOnlineUserState.IP)
                        UpdateOnlineUserIP(onlineUserInfo.OlId, updateOnlineUserState.IP);

                    if (onlineUserInfo.Uid != updateOnlineUserState.Uid)
                        UpdateOnlineUserUid(onlineUserInfo.OlId, updateOnlineUserState.Uid);

                    DeleteExpiredOnlineUser();
                }
                else
                {
                    int olid = 0;
                    if (updateOnlineUserState.Uid > 0)
                    {
                        olid = CreateOnlineMember(updateOnlineUserState.Uid, updateOnlineUserState.Sid, updateOnlineUserState.NickName, updateOnlineUserState.UpdateTime, updateOnlineUserState.IP, updateOnlineUserState.RegionId);
                    }
                    else
                    {
                        olid = CreateOnlineGuest(updateOnlineUserState.Sid, updateOnlineUserState.UpdateTime, updateOnlineUserState.IP, updateOnlineUserState.RegionId);
                    }
                    if (olid < 2147000000)
                    {
                        DeleteExpiredOnlineUser();
                    }
                    else
                    {
                        ResetOnlineUserTable();
                    }
                }
            }
        }

        /// <summary>
        /// 更新在线用户ip
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">ip</param>
        public static void UpdateOnlineUserIP(int olId, string ip)
        {
            DA_OnlineUsers.UpdateOnlineUserIP(olId, ip);
        }       

        /// <summary>
        /// 重置在线用户表
        /// </summary>
        public static void ResetOnlineUserTable()
        {
            try
            {
                DA_OnlineUsers.ResetOnlineUserTable();
            }
            catch
            {
                try
                {
                    DA_OnlineUsers.ResetOnlineUserTable();
                }
                catch
                {
                }
            }
        }

        #endregion

        #region 获取

        /// <summary>
        /// 获得全部在线用户数量
        /// </summary>
        /// <returns></returns>
        public static int GetOnlineUserCount()
        {
            int onlineUserExpire = SystemConfigData.WebConfig.OnlineCountExpire;
            if (onlineUserExpire == 0)
            {
                return GetOnlineUserCount(0);
            }
            int onlineAllUserCount = TypeHelper.ObjectToInt(SysCacheData.Get(CacheKeys.Sys_ONLINEUSER_ALLUSERCOUNT), -1);
            if (onlineAllUserCount == -1)
            {
                onlineAllUserCount = GetOnlineUserCount(0);
                SysCacheData.Insert(CacheKeys.Sys_ONLINEUSER_ALLUSERCOUNT, onlineAllUserCount);
            }
            return onlineAllUserCount;
        }

        /// <summary>
        /// 根据sessionid，获得在线用户信息
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <returns></returns>
        public static OnlineUserInfo GetOnlineUserBySid(string sid)
        {
            return DA_OnlineUsers.GetOnlineUserBySid(sid);
        }

        /// <summary>
        /// 根据用户类型userType获得在线用户数量
        /// </summary>
        /// <param name="userType">在线用户类型</param>
        /// <returns></returns>
        public static int GetOnlineUserCount(int userType)
        {
            return DA_OnlineUsers.GetOnlineUserCount(userType);
        }

        /// <summary>
        /// 根据LocationId和LocationType获得在线用户数量
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        public static int GetOnlineUserCount(int locationType, int locationId)
        {
            return DA_OnlineUsers.GetOnlineUserCount(locationType, locationId);
        }

        /// <summary>
        /// 获得在线游客数量 游客的usertype为-1
        /// </summary>
        /// <returns></returns>
        public static int GetOnlineGuestCount()
        {
           // 读取配置文件，获得在线游客的最大数量
            int onlineUserExpire = SystemConfigData.WebConfig.OnlineCountExpire;
            if (onlineUserExpire == 0)
            {
                //查数据表
                return GetOnlineUserCount(-1);
            }
            //先从缓存中看是否有在线游客的数量
            int onlineGuestCount = TypeHelper.ObjectToInt(SysCacheData.Get(CacheKeys.Sys_ONLINEUSER_GUESTCOUNT), -1);
            //如果没有，查数据表，并插入缓存中
            if (onlineGuestCount == -1)
            {
                onlineGuestCount = GetOnlineUserCount(-1);
                SysCacheData.Insert(CacheKeys.Sys_ONLINEUSER_GUESTCOUNT, onlineGuestCount);
            }
            return onlineGuestCount;
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
            return DA_OnlineUsers.GetOnlineUserList(pageSize, pageNumber, locationType, locationId, sort);
        }

        /// <summary>
        /// 获得在线用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public static string GetOnlineUserListSort(string sortColumn, string sortDirection)
        {
            return DA_OnlineUsers.GetOnlineUserListSort(sortColumn, sortDirection);
        }


        #endregion 

        #region 删除

        /// <summary>
        /// 根据sessionid删除在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        public static void DeleteOnlineUserBySid(string sid)
        {
            DA_OnlineUsers.DeleteOnlineUserBySid(sid);
        }

        /// <summary>
        /// 删除过期在线用户
        /// </summary>
        public static void DeleteExpiredOnlineUser()
        {
            if (_lastdeleteexpiredonlineuserstime < (Environment.TickCount - SystemConfigData.WebConfig.OnlineUserExpire * 1000 * 60) || _lastdeleteexpiredonlineuserstime == 0)
            {
                DA_OnlineUsers.DeleteExpiredOnlineUser(SystemConfigData.WebConfig.OnlineUserExpire);
                _lastdeleteexpiredonlineuserstime = Environment.TickCount;
            }
        }

        #endregion

        #region 

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">加密字符串</param>
        public static string AESEncrypt(string encryptStr)
        {
            return SecureHelper.AESEncrypt(encryptStr, SystemConfigData.WebConfig.SecretKey);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">解密字符串</param>
        public static string AESDecrypt(string decryptStr)
        {
            return SecureHelper.AESDecrypt(decryptStr, SystemConfigData.WebConfig.SecretKey);
        }

        /// <summary>
        /// 获得用户sid
        /// </summary>
        /// <returns></returns>
        public static string GetSidCookie()
        {
            return WebHelper.GetCookie("websid");
        }


        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <returns></returns>
        public static int GetUidCookie()
        {
            return TypeHelper.StringToInt(WebHelper.GetCookie("Web", "webuid"), -1);
        }

        /// <summary>
        /// 设置用户sid
        /// </summary>
        public static void SetSidCookie(string sid)
        {
            WebHelper.SetCookie("websid", sid, TypeHelper.StringToInt(WebHelper.GetCookieExpire("Web")));
        }
       

        /// <summary>
        /// 设置用户id
        /// </summary>
        public static void SetUidCookie(int uid)
        {
            WebHelper.SetCookie("Web", "webuid", uid.ToString(), TypeHelper.StringToInt(WebHelper.GetCookieExpire("Web")));
        }

        /// <summary>
        /// 获得cookie密码
        /// </summary>
        /// <returns></returns>
        public static string GetCookiePassword()
        {
            return WebHelper.UrlDecode(WebHelper.GetCookie("Web", "password"));
        }

        /// <summary>
        /// 解密cookie密码
        /// </summary>
        /// <param name="cookiePassword">cookie密码</param>
        /// <returns></returns>
        public static string DecryptCookiePassword(string cookiePassword)
        {
            return AESDecrypt(cookiePassword).Trim();
        }

        /// <summary>
        /// 设置cookie密码
        /// </summary>
        public static void SetCookiePassword(string password)
        {
            WebHelper.SetCookie("Web", "password", WebHelper.UrlEncode(AESEncrypt(password)), TypeHelper.StringToInt(WebHelper.GetCookieExpire("Web")));
        }

        #endregion

    }
}
