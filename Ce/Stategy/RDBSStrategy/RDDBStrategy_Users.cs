using Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;

namespace ReadDataBaseStrategy
{
    public partial class RDDBStrategy : IReadDBStrategy
    {
        #region 在线用户

        /// <summary>
        /// 创建在线用户
        /// </summary>
        public int CreateOnlineUser(OnlineUserInfo onlineUserInfo)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4,onlineUserInfo.Uid),
									   GenerateInParam("@sid",SqlDbType.Char,16,onlineUserInfo.Sid),
                                       GenerateInParam("@nickname",SqlDbType.NChar,20,onlineUserInfo.NickName),	
                                       GenerateInParam("@ip",SqlDbType.Char,15,onlineUserInfo.IP),	
                                       GenerateInParam("@regionid",SqlDbType.SmallInt,2,onlineUserInfo.RegionId),	
									   GenerateInParam("@updatetime",SqlDbType.DateTime,8,onlineUserInfo.UpdateTime)
								   };
            return TypeHelper.ObjectToInt(DBUniversalHelper.ExecuteScalar(CommandType.StoredProcedure,
                                                                   string.Format("{0}createonlineuser", DBUniversalHelper.RDBSTablePre),
                                                                   parms));
        }

        /// <summary>
        /// 更新在线用户ip
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">ip</param>
        public void UpdateOnlineUserIP(int olId, string ip)
        {
            DbParameter[] parms = {
									   GenerateInParam("@ip",SqlDbType.Char,15,ip),
									   GenerateInParam("@olid",SqlDbType.Int,4,olId)
								   };
            DBUniversalHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{}updateonlineuserip", DBUniversalHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 更新在线用户uid
        /// </summary>
        /// <param name="olId">在线用户id</param>
        /// <param name="ip">uid</param>
        public void UpdateOnlineUserUid(int olId, int uid)
        {
            DbParameter[] parms = {
									   GenerateInParam("@uid",SqlDbType.Int,4,uid),
									   GenerateInParam("@olid",SqlDbType.Int,4,olId)
								   };
            DBUniversalHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}updateonlineuseruid", DBUniversalHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 获得在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        /// <returns></returns>
        public IDataReader GetOnlineUserBySid(string sid)
        {
            DbParameter[] parms = {
                                    GenerateInParam("@sid",SqlDbType.Char,16,sid)
                                  };

            return DBUniversalHelper.ExecuteReader(CommandType.StoredProcedure,
                                            string.Format("{0}getonlineuserbysid", DBUniversalHelper.RDBSTablePre),
                                            parms);
        }

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="userType">在线用户类型</param>
        /// <returns></returns>
        public int GetOnlineUserCount(int userType)
        {
            DbParameter[] parms = {GenerateInParam("@usertype",SqlDbType.Int,4,userType)};
            return TypeHelper.ObjectToInt(DBUniversalHelper.ExecuteScalar(CommandType.StoredProcedure,string.Format("{0}getonlineuercount", DBUniversalHelper.RDBSTablePre),parms));
        }

        /// <summary>
        /// 删除在线用户
        /// </summary>
        /// <param name="sid">sessionId</param>
        public void DeleteOnlineUserBySid(string sid)
        {
            DbParameter[] parms = { 
                                        GenerateInParam("@sid", SqlDbType.Char, 16, sid)
                                    };
            DBUniversalHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}deleteonlineuserbysid", DBUniversalHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 删除过期在线用户
        /// </summary>
        /// <param name="onlineUserExpire">过期时间</param>
        public void DeleteExpiredOnlineUser(int onlineUserExpire)
        {
            DbParameter[] parms = { 
                                    GenerateInParam("@expiretime", SqlDbType.DateTime, 8, DateTime.Now.AddMinutes(onlineUserExpire * -1))
                                  };
            DBUniversalHelper.ExecuteNonQuery(CommandType.StoredProcedure,
                                       string.Format("{0}deleteexpiredonlineuser", DBUniversalHelper.RDBSTablePre),
                                       parms);
        }

        /// <summary>
        /// 重置在线用户表
        /// </summary>
        public void ResetOnlineUserTable()
        {
            DBUniversalHelper.ExecuteNonQuery(CommandType.Text,
                                       string.Format("TRUNCATE TABLE [{0}onlineusers]",
                                       DBUniversalHelper.RDBSTablePre));
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
        public IDataReader GetOnlineUserList(int pageSize, int pageNumber, int locationType, int locationId, string sort)
        {
            string condition = GetOnlineUserListCondition(locationType, locationId);
            bool noCondition = string.IsNullOrWhiteSpace(condition);
            string commandText;
            if (pageNumber == 1)
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {2} FROM [{1}onlineusers] ORDER BY {3}",
                                                pageSize,
                                                DBUniversalHelper.RDBSTablePre,
                                                RDBSFields.ONLINE_USERS,
                                                sort);

                else
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}onlineusers] WHERE {2} ORDER BY {4}",
                                                pageSize,
                                                DBUniversalHelper.RDBSTablePre,
                                                condition,
                                                RDBSFields.ONLINE_USERS,
                                                sort);
            }
            else
            {
                if (noCondition)
                    commandText = string.Format("SELECT TOP {0} {3} FROM [{1}onlineusers] WHERE [olid] NOT IN (SELECT TOP {2} [olid] FROM [{1}onlineusers] ORDER BY {4}) ORDER BY {4}",
                                                pageSize,
                                                DBUniversalHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                RDBSFields.ONLINE_USERS,
                                                sort);
                else
                    commandText = string.Format("SELECT TOP {0} {4} FROM [{1}onlineusers] WHERE [olid] NOT IN (SELECT TOP {2} [olid] FROM [{1}onlineusers] WHERE {3} ORDER BY {5}) AND {3} ORDER BY {5}",
                                                pageSize,
                                                DBUniversalHelper.RDBSTablePre,
                                                (pageNumber - 1) * pageSize,
                                                condition,
                                                RDBSFields.ONLINE_USERS,
                                                sort);
            }

            return DBUniversalHelper.ExecuteReader(CommandType.Text, commandText);
        }

        /// <summary>
        /// 获得在线用户列表排序
        /// </summary>
        /// <param name="sortColumn">排序列</param>
        /// <param name="sortDirection">排序方向</param>
        /// <returns></returns>
        public string GetOnlineUserListSort(string sortColumn, string sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortColumn))
                sortColumn = "[olid]";
            if (string.IsNullOrWhiteSpace(sortDirection))
                sortDirection = "DESC";

            return string.Format("{0} {1} ", sortColumn, sortDirection);
        }

        /// <summary>
        /// 获得在线用户数量
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        public int GetOnlineUserCount(int locationType, int locationId)
        {
            string condition = GetOnlineUserListCondition(locationType, locationId);
            string commandText;
            if (string.IsNullOrWhiteSpace(condition))
                commandText = string.Format("SELECT COUNT(olid) FROM [{0}onlineusers]", DBUniversalHelper.RDBSTablePre);
            else
                commandText = string.Format("SELECT COUNT(olid) FROM [{0}onlineusers] WHERE {1}", DBUniversalHelper.RDBSTablePre, condition);
            return TypeHelper.ObjectToInt(DBUniversalHelper.ExecuteScalar(CommandType.Text, commandText));
        }

        /// <summary>
        /// 获得在线用户列表条件
        /// </summary>
        /// <param name="locationType">位置类型(0代表省,1代表市,2代表区或县)</param>
        /// <param name="locationId">位置id</param>
        /// <returns></returns>
        private string GetOnlineUserListCondition(int locationType, int locationId)
        {
            if (locationId > 0)
            {
                if (locationType == 0)
                {
                    return string.Format(" [regionid] IN (SELECT [regionid] FROM [{0}regions] WHERE [provinceid]={1})", DBUniversalHelper.RDBSTablePre, locationId);
                }
                else if (locationType == 1)
                {
                    return string.Format(" [regionid] IN (SELECT [regionid] FROM [{0}regions] WHERE [cityid]={1})", DBUniversalHelper.RDBSTablePre, locationId);
                }
                else if (locationType == 2)
                {
                    return string.Format(" [regionid]={0}", locationId);
                }
            }

            return "";
        }

        #endregion

        /// <summary>
        /// 获得部分用户
        /// </summary>
        /// <param name="uid">用户id</param>
        /// <returns></returns>
        public IDataReader GetPartUserById(int uid)
        {
            DbParameter[] parms = {GenerateInParam("@uid",SqlDbType.Int,4,uid)};
            return DBUniversalHelper.ExecuteReader(CommandType.StoredProcedure,string.Format("{0}getpartuserbyid",DBUniversalHelper.RDBSTablePre),parms);
        }

    }
}