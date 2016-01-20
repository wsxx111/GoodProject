using Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;

namespace ReadDataBaseStrategy
{
    public partial class RDDBStrategy : IReadDBStrategy
    {   
        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetBannedIPList()
        {
            DbParameter[] parms = {
									GenerateInParam("@nowtime",SqlDbType.DateTime,8, DateTime.Now)
			                       };
            string commandText = string.Format("SELECT [ip] FROM [{0}bannedips] WHERE [liftbantime]>@nowtime", DBUniversalHelper.RDBSTablePre);
            return DBUniversalHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }
        

        /// <summary>
        /// 判断是否存在某用户列表
        /// </summary>
        /// <returns></returns>
        public IDataReader GetUserByUserName(string username)
        {
            DbParameter[] parms = {
									GenerateInParam("@userName",SqlDbType.NChar,20,username)
			                       };
            string commandText = string.Format("SELECT [uid] FROM [{0}users] WHERE [username]=@userName", DBUniversalHelper.RDBSTablePre);
            return DBUniversalHelper.ExecuteReader(CommandType.Text, commandText, parms);
        }       

    }
}
