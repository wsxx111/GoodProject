using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webconfig
{
    public class MySection:System.Configuration.ConfigurationSection
    {
        [System.Configuration.ConfigurationProperty("HTML", IsRequired = false)]
        public webconfig.MyTextElement HTML
        {
            get { return (webconfig.MyTextElement)base["HTML"]; }
            set { base["HTML"] = value; }
        }

        [System.Configuration.ConfigurationProperty("SQL", IsRequired = false)]
        public webconfig.MyTextElement SQL
        {
            get { return (webconfig.MyTextElement)base["SQL"]; }
            set { base["SQL"] = value; }
        }
    }

}