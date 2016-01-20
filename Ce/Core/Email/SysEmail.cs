using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class SysEmail
    {
        private static IEmailStrategy _iemailstrategy = null;  //邮件策略

        static SysEmail()
        {
            try
            {
                string[] fileNameList = Directory.GetFiles(System.Web.HttpRuntime.BinDirectory, "EmailStrategy.dll", SearchOption.TopDirectoryOnly);
                if (fileNameList.Length > 0)
                {
                    _iemailstrategy = (IEmailStrategy)Activator.CreateInstance(Type.GetType(string.Format("EmailStrategy.EmailStrategy,EmailStrategy"), false, true));
                }
                else
                {
                    throw new Exception("创建'邮件策略对象'失败,未能在bin目录中找到'邮件策略程序集'");
                }
            }
            catch
            {
                throw new Exception("创建'邮件策略对象'时发生异常");
            }
        }

        /// <summary>
        /// 邮件策略实例
        /// </summary>
        public static IEmailStrategy Instance
        {
            get { return _iemailstrategy; }
        }
    }
}
