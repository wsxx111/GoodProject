using NOSQLStrategy;
using ReadDataBaseStrategy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestStrategy
{
    public partial class Test : System.Web.UI.Page
    {

        private static object _locker = new object();//锁对象
        private static Core.IEmailStrategy _iemailstrategy = null;//邮件策略
        private static Core.EmailConfigInfo _emailconfiginfo = null;//邮件配置信息

        protected void Page_Load(object sender, EventArgs e)
        {
            //RDDBStrategy data = new RDDBStrategy();
            //HashSet<string> ipList = new HashSet<string>();
            //IDataReader reader = data.GetBannedIPList();
            //while (reader.Read())
            //{
            //    ipList.Add(reader["ip"].ToString());
            //}
            //reader.Close();
            //Response.Write(ipList.Count);

            //User u1 = new TestStrategy.User(){Id="001",Name="wk",Age=25};
            //User u2 = new TestStrategy.User() { Id = "002", Name = "bs", Age = 22 };
            //User u3 = new TestStrategy.User() { Id = "003", Name = "hs", Age = 21 };
            //List<User> users = new List<TestStrategy.User>() { u1, u2, u3 };
            //foreach (var item in users)
            //{
            //  HashOperator.AddKeyValueList<User>(item.Id, item);
            //}           
            //List<string> userlist = HashOperator.GetAll<string>(u1.Id); 


            
            _iemailstrategy = Core.SysEmail.Instance;
            _emailconfiginfo = Core.SystemConfigData.EmailConfig;

            _iemailstrategy.Host = _emailconfiginfo.Host;
            _iemailstrategy.Port = _emailconfiginfo.Port;
            _iemailstrategy.UserName = _emailconfiginfo.UserName;
            _iemailstrategy.Password = _emailconfiginfo.Password;
            _iemailstrategy.From = _emailconfiginfo.From;
            _iemailstrategy.FromName = _emailconfiginfo.FromName;

            string subject = "安全中心邮箱验证提醒测试";

            System.Text.StringBuilder body = new System.Text.StringBuilder(_emailconfiginfo.SCVerifyBody);
            body.Replace("{shopname}", "网上商城");
            body.Replace("{siteurl}", "http://www.ddd.com");
            body.Replace("{username}", "d");
            body.Replace("{deadline}", DateTime.Now.AddMinutes(30).ToString("yyyy-MM-dd HH:mm"));
            body.Replace("{url}", "");
            _iemailstrategy.Send("", subject, body.ToString());

        }      
    }
    class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}