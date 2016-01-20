using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webconfig
{
    public class LivingSection:System.Configuration.ConfigurationSection
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Address { get; set; }   
    }
}