using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SystemConfigData
    {
        private static object _locker = new object();//锁对象       

        private static IConfigStrategy _iconfigstrategy = null;//配置策略

        private static DBConfigInfo _rdbsconfiginfo = null;//关系数据库配置信息

        private static RedisNOSQLConfigInfo _redisnosqlconfiginfo = null;//redis非关系数据库配置信息

        private static WebConfig _sysconfiginfo = null;//网站基本配置信息

        private static EmailConfigInfo _emailconfiginfo = null;//邮件配置信息


        static SystemConfigData()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "ConfigStrategy.dll", SearchOption.TopDirectoryOnly);
                if (fileNameList.Length > 0)
                {
                    _iconfigstrategy = (IConfigStrategy)Activator.CreateInstance(Type.GetType("ConfigStrategy.ConfigStrategy,ConfigStrategy"));
                }
                else
                {
                    throw new Exception("创建'配置策略对象'失败，未能在bin目录中找到'配置策略程序集'ConfigStrategy.dll'");
                }
            }
            catch
            {
                throw new Exception("创建'配置策略对象'创建实例时发生异常");
            }
        }


        /// <summary>
        /// 网站基本配置信息
        /// </summary>
        public static WebConfig WebConfig
        {
            get { return _sysconfiginfo = _iconfigstrategy.GetWebConfig(); }
        }

        /// <summary>
        /// 关系数据库配置信息
        /// </summary>
        public static DBConfigInfo DBConInfo
        {
            get { return _rdbsconfiginfo = _iconfigstrategy.GetRDDBConfig(); }
        }

        public static DbProviderFactory DBProviderFactory
        {
            get { return SqlClientFactory.Instance; }
        }

        /// <summary>
        /// Redis非关系型数据库配置信息
        /// </summary>
        public static RedisNOSQLConfigInfo RedisNOSQLConfig
        {
            get
            {
                if (_redisnosqlconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_redisnosqlconfiginfo == null)
                        {
                            _redisnosqlconfiginfo = _iconfigstrategy.GetRedisNOSQLConfig();
                        }
                    }
                }
                return _redisnosqlconfiginfo;
            }
        }

        /// <summary>
        /// 邮件配置信息
        /// </summary>
        public static EmailConfigInfo EmailConfig
        {
            get
            {
                if (_emailconfiginfo == null)
                {
                    lock (_locker)
                    {
                        if (_emailconfiginfo == null)
                        {
                            _emailconfiginfo = _iconfigstrategy.GetEmailConfig();
                        }
                    }
                }
                return _emailconfiginfo;
            }
        }

    }
}
