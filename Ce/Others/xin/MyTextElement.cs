using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webconfig
{
    public class MyTextElement:System.Configuration.ConfigurationElement
    {
        protected override void DeserializeElement(System.Xml.XmlReader reader, bool serializeCollectionKey)
        {
            CommandText = reader.ReadElementContentAs(typeof(string), null) as string;
        }

        protected override bool SerializeElement(System.Xml.XmlWriter writer, bool serializeCollectionKey)
        {
            if (writer != null)
            {
                writer.WriteCData(CommandText);
            }
            return true;
        }


        [System.Configuration.ConfigurationProperty("data", IsRequired = false)]
        public string CommandText
        {
            get { return this["data"].ToString(); }
            set { this["data"] = value; }
        }

    }
}