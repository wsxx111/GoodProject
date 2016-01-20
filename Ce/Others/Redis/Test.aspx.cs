using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServiceStack.Redis;

namespace Redis
{
    // 首先 C# 不要调用的ServiceStack.Redis驱动的4.0版本，因为已经商业化，且有6000条/小时的限制

    //测试   对于大量数据的存储，client.Set比client.Store快一半，且Store更容易掉数据
    public partial class Test : System.Web.UI.Page
    {
        public RedisClient client = new RedisClient("127.0.0.1", 6379);  
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Result.Text = "";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Random random = new Random();
            for (int i = 0; i < 100000; i++)
            {
                client.Set<UserInfo>(Guid.NewGuid().ToString(), new UserInfo
                {
                    Name = "wk",
                    Age = random.Next(10, 40),
                    Address = "BeiJing"
                });               
            }
            sw.Stop();
            this.ResumeTime.Text += (sw.ElapsedMilliseconds/1000.000).ToString() + "秒, ";
            this.Result.Text = "本次请求存储10万条数据，结果已存储" + client.DbSize.ToString() + "条数据";
            client.FlushDb();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            Random random = new Random();
            for (int i = 0; i < 100000; i++)
            {
                client.Store<UserInfo>(new UserInfo
                {
                    Name = "wk2",
                    Age = random.Next(10, 40),
                    Address = "HeNan"
                });
            }
            sw.Stop();
            this.ResumeTime2.Text += (sw.ElapsedMilliseconds/1000.000).ToString() + "秒, ";
            this.Result.Text = "本次请求存储10万条数据，结果已存储" + client.DbSize.ToString() + "条数据";
            client.FlushDb();
        }
    }

    class UserInfo
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }
    }
}