using Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class DA_BannedIPs
    {
        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        public static HashSet<string> GetBannedIPList()
        {
            HashSet<string> ipList = new HashSet<string>();
            IDataReader reader = RDDBData.ReadDatabase.GetBannedIPList();
            while (reader.Read())
            {
                ipList.Add(reader["ip"].ToString());
            }
            reader.Close();
            return ipList;
        }
    }
}
