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
    public class Sv_Login
    {
        /// <summary>
        /// 检查是否含有该用户
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static bool CheckExistThisUser(string username)
        {
            if (!SecureHelper.IsSafeSqlString(username))
            {
                return false;
            }
            else {
                return Login.IsExistUserbyUsername(username);
            }           
        }
    }
}
