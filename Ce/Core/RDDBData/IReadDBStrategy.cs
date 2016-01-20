using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{    
    public interface IReadDBStrategy
    {
     
        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        IDataReader GetBannedIPList();

        IDataReader GetUserByUserName(string username);

          /// <summary>
        /// 创建在线用户
        /// </summary>
        int CreateOnlineUser(OnlineUserInfo onlineUserInfo);

        /// <summary>
        /// 更新在线用户ip
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">ip</param>
        void UpdateOnlineUserIP(int olId, string ip);

        /// <summary>
        /// 更新在线用户uid
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">uid</param>
        void UpdateOnlineUserUid(int olId, int uid);

        /// <summary>
        /// 获得在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <returns></returns>
        IDataReader GetOnlineUserBySid(string sid);

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="userType">在线用户类型</param>
        /// <returns></returns>
        int GetOnlineUserCount(int userType);

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        void DeleteOnlineUserBySid(string sid);

        /// <summary>
        /// 删除过期在线用户
        /// </summary>
        /// <param name="onlineUserExpire">过期时间</param>
        void DeleteExpiredOnlineUser(int onlineUserExpire);

        /// <summary>
        /// 重置在线用户表
        /// </summary>
        void ResetOnlineUserTable();

        /// <summary>
        /// 获得在线用户列表
        /// </summary>
        /// <param name="pageSize">每页数</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <param name="sort">排序</param>
        /// <returns></returns>
        IDataReader GetOnlineUserList(int pageSize, int pageNumber, int locationType, int locationId, string sort);

        /// <summary>
        /// 获得在线用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        string GetOnlineUserListSort(string sortColumn, string sortDirection);

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        int GetOnlineUserCount(int locationType, int locationId);

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        IDataReader GetPartUserById(int uid);

    }
}
