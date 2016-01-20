using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class WebSessionData
    {
        private static ISessionStrategy _isessionstrategy = null;//会话状态策略

        static WebSessionData()
        {
            //try
            //{
            //    string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "SessionStrategy.dll", SearchOption.TopDirectoryOnly);
            //    _isessionstrategy = (ISessionStrategy)Activator.CreateInstance(Type.GetType("SessionStrategy.SessionStrategy,SessionStrategy",false,true));
            //}
            //catch
            //{
            //    throw new Exception("创建'会话状态策略对象'失败,可能存在的原因:未将'会话状态策略程序集'添加到bin目录中;'会话状态策略程序集'文件名不符合'SessionStrategy.dll'格式");
            //}
        }

        /// <summary>
        /// 会话状态策略实例
        /// </summary>
        public static ISessionStrategy Instance
        {
            get { return _isessionstrategy; }
        }

    }
}
