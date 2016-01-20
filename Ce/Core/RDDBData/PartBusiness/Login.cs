using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class Login
    {
        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        public static bool IsExistUserbyUsername(string username)
        {
            IDataReader reader = RDDBData.ReadDatabase.GetUserByUserName(username);
            while (reader.Read())
            {              
                return true;
            }
            reader.Close();
            return false;
        }
    }
}
