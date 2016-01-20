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
        #region  辅助方法

        /// <summary>
        /// 生成输入参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private DbParameter GenerateInParam(string paramName, SqlDbType sqlDbType, int size, object value)
        {
            return GenerateParam(paramName, sqlDbType, size, ParameterDirection.Input, value);
        }

        /// <summary>
        /// 生成输出参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <returns></returns>
        private DbParameter GenerateOutParam(string paramName, SqlDbType sqlDbType, int size)
        {
            return GenerateParam(paramName, sqlDbType, size, ParameterDirection.Output, null);
        }

        /// <summary>
        /// 生成返回参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <returns></returns>
        private DbParameter GenerateReturnParam(string paramName, SqlDbType sqlDbType, int size)
        {
            return GenerateParam(paramName, sqlDbType, size, ParameterDirection.ReturnValue, null);
        }

        /// <summary>
        /// 生成参数
        /// </summary>
        /// <param name="paramName">参数名</param>
        /// <param name="sqlDbType">参数类型</param>
        /// <param name="size">类型大小</param>
        /// <param name="direction">方向</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        private DbParameter GenerateParam(string paramName, SqlDbType sqlDbType, int size, ParameterDirection direction, object value)
        {
            SqlParameter param = new SqlParameter(paramName, sqlDbType, size);
            param.Direction = direction;
            if (direction == ParameterDirection.Input && value != null)
                param.Value = value;
            return param;
        }

        #endregion                    

    }
}
