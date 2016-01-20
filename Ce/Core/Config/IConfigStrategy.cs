using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial interface IConfigStrategy
    {
        /// <summary>
        /// 获得关系数据库配置
        /// </summary>
        DBConfigInfo GetRDDBConfig();
              

        /// <summary>
        /// 获得系统基本配置
        /// </summary>
        WebConfig GetWebConfig();

        /// <summary>
        /// 获得Redis非关系型数据库配置
        /// </summary>
        RedisNOSQLConfigInfo GetRedisNOSQLConfig();

        /// <summary>
        /// 获得邮件配置
        /// </summary>
        EmailConfigInfo GetEmailConfig();
    }
}
