using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class RDDBData
    {
        private static IReadDBStrategy _irdbsstrategy = null;//关系型数据库策略   

        private static object _locker = new object();//锁对象       
        private static IUserNOSQLStrategy _iusernosqlstrategy = null;//用户的非关系型数据库策略
        private static bool _enablednosql = false;//是否启用非关系型数据库

        static RDDBData()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "ReadDataBaseStrategy.dll", SearchOption.TopDirectoryOnly);
                if (fileNameList.Length > 0)
                {
                    _irdbsstrategy = (IReadDBStrategy)Activator.CreateInstance(Type.GetType("ReadDataBaseStrategy.RDDBStrategy,ReadDataBaseStrategy"));
                }
                else
                {
                    throw new Exception("创建'关系数据库策略对象'失败,未将'ReadDataBaseStrategy.dll'关系数据库策略程序集'在bin目录中未找到;");
                }
            }
            catch
            {
                throw new Exception("创建'关系数据库策略对象'失败,'ReadDataBaseStrategy.dll'关系数据库策略程序集'创建实例时发生异常");
            }
            //如果有非关系数据库策略程序集，则开启
            _enablednosql = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "NOSQLStrategy.dll", SearchOption.TopDirectoryOnly).Length > 0;
        }

        /// <summary>
        /// 关系型数据库
        /// </summary>
        public static IReadDBStrategy ReadDatabase
        {
            get { return _irdbsstrategy; }
        }


        /// <summary>
        /// 用户的非关系型数据库
        /// </summary>
        public static IUserNOSQLStrategy UserNOSQL
        {
            get
            {
                if (_enablednosql && SystemConfigData.RedisNOSQLConfig.User.Enabled == 1)
                {
                    if (_iusernosqlstrategy == null)
                    {
                        lock (_locker)
                        {
                            if (_iusernosqlstrategy == null)
                            {
                                try
                                {
                                    _iusernosqlstrategy = (IUserNOSQLStrategy)Activator.CreateInstance(Type.GetType("NOSQLStrategy.UserNOSQLStrategy,NOSQLStrategy", false, true));
                                }
                                catch
                                {
                                    throw new Exception("创建'用户非关系数据库策略对象'失败,可能存在的原因:未将'用户非关系数据库策略程序集'添加到bin目录中;'用户非关系数据库策略程序集'文件名不符合'NOSQLStrategy.dll'格式");
                                }
                            }
                        }
                    }
                }
                return _iusernosqlstrategy;
            }
        }


    }
}
