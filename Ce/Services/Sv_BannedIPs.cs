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
    public class Sv_BannedIPs
    {
        /// <summary>
        /// 检查IP是否禁止
        /// </summary>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static bool CheckIP(string ip)
        {
            HashSet<string> ipList = GetBannedIPList();
            if (ipList.Count > 0 && ip.Length > 0)
            {
                if (ipList.Contains(ip))
                    return true;
                if (ipList.Contains(StringHelper.SubString(ip, ip.LastIndexOf('.'))))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 获得禁止的ip列表
        /// </summary>
        /// <returns></returns>
        public static HashSet<string> GetBannedIPList()
        {
            HashSet<string> ipList = SysCacheData.Get(CacheKeys.Sys_BANNEDIP_HASHSET) as HashSet<string>;             
            if (ipList == null)
            {
                ipList = DA_BannedIPs.GetBannedIPList();
                SysCacheData.Insert(CacheKeys.Sys_BANNEDIP_HASHSET, ipList);
            }
            return ipList;
        }

    }
}
