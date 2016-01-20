using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webconfig
{
    public class Ren:System.Configuration.ConfigurationSection
    {
        [System.Configuration.ConfigurationProperty("yanjing",IsRequired=true)]
        public int YanJing
        {
            get { return (int)base["yanjing"]; }
            set { base["yanjing"] = value; }
        }

        [System.Configuration.ConfigurationProperty("ShuXing", IsDefaultCollection = false)]
        public ChildSection ShuXing
        {
            get { return (ChildSection)base["ShuXing"]; }
            set { base["ShuXing"] = value; }
        }

    }

    public class ChildSection : System.Configuration.ConfigurationElement
    {
        [System.Configuration.ConfigurationProperty("Name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)base["Name"]; }
            set { base["Name"] = value; }
        }

        [System.Configuration.ConfigurationProperty("Age", IsRequired = true)]
        public int Age
        {
            get { return (int)base["Age"]; }
            set { base["Age"] = value; }
        }

        [System.Configuration.ConfigurationProperty("Address", IsRequired = true, IsKey = true)]
        public string Address
        {
            get { return (string)base["Address"]; }
            set { base["Address"] = value; }
        }
    }


}