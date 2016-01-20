using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Redis
{
    public partial class Test2 : System.Web.UI.Page
    {
        public RedisClient client = new RedisClient("127.0.0.1", 6379);
        protected void Page_Load(object sender, EventArgs e)
        {
            client.Set<string>("wk", "vvv");
            TextBox1.Text = client.LastSave.ToString();

            //TextBox1.Text = client.Get<string>("ppp");
            //client.AddItemToList("dd","ccc");
            //client.AddItemToList("dd", "eee");
            //client.AddItemToList("dd", "rrr");
            //HashSet<string> ds = client.Sets["dd"].GetAll();           
            //foreach (string str in ds)
            //{
            //    TextBox1.Text += str;
            //}
            Person p1 = new Person() { Id = 1, Name = "张飞" };
            Person p2 = new Person() { Id = 2, Name = "关羽" };
            Person p3 = new Person() { Id = 3, Name = "曹操" };
            Person p4 = new Person() { Id = 4, Name = "刘备" };
            Person p5 = new Person() { Id = 5, Name = "典韦" };
            List<Person> ListPerson = new List<Person>() { p1, p2, p3, p4, p5 };
            IRedisTypedClient<Person> IPerson = client.As<Person>();
            IPerson.DeleteAll();
            IPerson.Store(p1);
            IPerson.StoreAll(ListPerson);
            Response.Write(IPerson.GetAll().Count());
            Response.Write(IPerson.GetAll().Where(m => m.Id == 5).First().Name);
            IPerson.DeleteById(2); //删除 关羽
            Response.Write(IPerson.GetAll().Count());      //4
            IPerson.DeleteByIds(new List<int> { 3, 4 });    //删除张飞 曹操
            Response.Write(IPerson.GetAll().Count());      //2
            IPerson.DeleteAll();   //全部删除
            Response.Write(IPerson.GetAll().Count());      //
            //执行事务          
            client.Add("key", 1);
            using (IRedisTransaction IRT = client.CreateTransaction())
            {
                IRT.QueueCommand(r => r.Set("key", 20));
                IRT.QueueCommand(r => r.Increment("key", 1));
                IRT.Commit(); // 提交事务
            }
            Response.Write(client.Get<string>("key"));


            //并发锁

            client.Add("mykey", 1);
            // 支持IRedisTypedClient和IRedisClient
            using (client.AcquireLock("testlock"))
            {
                Response.Write("申请并发锁<br/>");
                var counter = client.Get<int>("mykey");
                Thread.Sleep(100);
                client.Set("mykey", counter + 1);
                Response.Write(client.Get<int>("mykey"));
            }
            client.Set("dd", "k");
           Response.Write(client.Get("dd")[0]);
        }
    }
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}