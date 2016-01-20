using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CacheKeys
    {
        /// <summary>
        /// 被禁止的IPHashSet缓存键
        /// </summary>
        public const string Sys_BANNEDIP_HASHSET = "/Sys/BannedIPHashSet";

        /// <summary>
        /// 在线游客数量缓存键
        /// </summary>
        public const string Sys_ONLINEUSER_GUESTCOUNT = "/Sys/OnlineGuestCount";
        /// <summary>
        /// 全部在线人数缓存键
        /// </summary>
        public const string Sys_ONLINEUSER_ALLUSERCOUNT = "/Sys/OnlineAllUserCount";
    }
}
