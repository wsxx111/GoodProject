using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webconfig
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //  appSettings与connectionStrings都是由ConfigurationManager提供的两个属性来读取的

            //对于客户端应用程序，请使用 ConfigurationManager。
            this.AppSetValue.Text = System.Configuration.ConfigurationManager.AppSettings["Version"];
            this.ConstrValue.Text = System.Configuration.ConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

            //对于web应用程序而言，建议优先使用WebConfigurationManager 
            //使用 WebConfigurationManager 是处理与 Web 应用程序相关的配置文件的首选方法
            this.AppSetValue.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["Version"];
            this.ConstrValue.Text = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["connstr"].ConnectionString;

            //section节点  Handler  自带的
            //只允许一个 <configSections> 元素。它必须是根 <configuration> 元素的第一个子元素 

            System.Collections.Specialized.NameValueCollection getinfo = (System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationManager.GetSection("People");
            string st = "";
            foreach (string key in getinfo.AllKeys)
            {
                st += key + ":" + getinfo[key] + ";  ";
            }
            this.SectionPeopleValue.Text = st;

            System.Collections.IDictionary dic = (System.Collections.IDictionary)System.Configuration.ConfigurationManager.GetSection("Human");
            string st2 = "";
            foreach (string key in dic.Keys)
            {
                st2 += key + ":" + dic[key] + ";  ";
            }
            this.SectionHumanValue.Text = st2;

            System.Collections.IDictionary sing = (System.Collections.IDictionary)System.Configuration.ConfigurationManager.GetSection("Person");
            string st3 = "";
            foreach (string key in sing.Keys)
            {
                st3 += key + ":" + sing[key] + ";  ";
            }
            this.SectionPersonValue.Text = st3;


            //自定义Handler
            //System.Collections.Hashtable conf = System.Configuration.ConfigurationManager.GetSection("Man") as System.Collections.Hashtable;
            string st4 = "";
            //st4 += "节点数量：" + conf.Count + ";   ";
            //foreach (System.Collections.DictionaryEntry elem in conf)
            //{
            //    System.Collections.Hashtable att = (System.Collections.Hashtable)elem.Value;
            //    foreach (System.Collections.DictionaryEntry a in att)
            //    {
            //        st4 += a.Key.ToString() + ":" + a.Value.ToString() + ";  ";
            //    }

            //}
            this.SectionManValue.Text = st4;


            //对于稍微在复杂一点的结构，子元素的Model类要继承自ConfigurationElement。
            Ren reninfo = System.Configuration.ConfigurationManager.GetSection("Ren") as Ren;
            this.SectionRenValue.Text = "有" + reninfo.YanJing + "个眼睛，名字：" + reninfo.ShuXing.Name + "，年龄：" + reninfo.ShuXing.Age.ToString() + "，地址：" + reninfo.ShuXing.Address;


            //配置文件中的CDATA
            MySection section = System.Configuration.ConfigurationManager.GetSection("MySection") as MySection;
            string st5 = "";
            st5 += section.HTML.CommandText + section.SQL.CommandText;
            this.SectionCurtureValue.Text = st5;

            //配置节点写入
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            LivingSection Section = config.GetSection("Living") as LivingSection;
            Section.Name = "哈哈哈";
            Section.Age = 18;
            Section.Address = "ShangHai";
            config.Save();  //好像没法修改
            //System.Configuration.ConfigurationManager.RefreshSection("Living");
            //var dd = System.Web.Configuration.WebConfigurationManager.GetSection("Living") as LivingSection;          


            //读取.Net Framework中已经定义的节点
            System.Web.Configuration.HttpModulesSection sect = System.Configuration.ConfigurationManager.GetSection("system.web/httpModules") as System.Web.Configuration.HttpModulesSection;
            string st6 = "";
            foreach (System.Web.Configuration.HttpModuleAction action in sect.Modules)
            {
                st6+=action.Name+";  ";
            }


        }
    }
  
}