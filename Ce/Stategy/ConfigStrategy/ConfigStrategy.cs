using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Helper;

namespace ConfigStrategy
{    
    public class ConfigStrategy:IConfigStrategy       
    {
        //设置配置文件路径
        private static readonly string _webconfigfilepath = "/App_Data/websys.config";

        private readonly string _dbconfilepath = "/App_Data/database.config";

        private readonly string _redisnosqlconfigfilepath = "/App_Data/redisnosql.config";//redis非关系型数据库配置信息文件路径

        private readonly string _emailconfigfilepath = "/App_Data/email.config";//邮件配置信息文件路径

        /// <summary>
        /// 获得关系数据库配置
        /// </summary>
        public DBConfigInfo GetRDDBConfig()
        {            
            return (DBConfigInfo)IOHelper.DeserializeFromXML(typeof(DBConfigInfo), IOHelper.GetMapPath(_dbconfilepath));
        }        

        /// <summary>
        /// 获得关系数据库配置
        /// </summary>
        public WebConfig GetWebConfig()
        {         
            return (WebConfig)IOHelper.DeserializeFromXML(typeof(WebConfig),IOHelper.GetMapPath(_webconfigfilepath));
        }

        /// <summary>
        /// 获得Redis非关系型数据库配置
        /// </summary>
        public RedisNOSQLConfigInfo GetRedisNOSQLConfig()
        {
            return (RedisNOSQLConfigInfo)IOHelper.DeserializeFromXML(typeof(RedisNOSQLConfigInfo),IOHelper.GetMapPath(_redisnosqlconfigfilepath));
        }
       

        /// <summary>
        /// 获得邮件配置
        /// </summary>
        public EmailConfigInfo GetEmailConfig()
        {
            return (EmailConfigInfo)IOHelper.DeserializeFromXML(typeof(EmailConfigInfo), IOHelper.GetMapPath(_emailconfigfilepath));
        }
      

    }
}
