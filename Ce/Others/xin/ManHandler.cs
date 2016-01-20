using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webconfig
{
    //注意必须要实现IConfigurationSectionHandler接口
    public class ManHandler:System.Configuration.IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            System.Collections.Hashtable myConfig = new System.Collections.Hashtable();
            // 本节元素，获取的任何属性。
            System.Collections.Hashtable myAttribs = new System.Collections.Hashtable();
            //遍历当前节点的属性
            foreach (System.Xml.XmlAttribute attrib in section.Attributes)
            {
                //如果当前节点是属性节点，则添加进入myAttribs
                if (System.Xml.XmlNodeType.Attribute == attrib.NodeType)
                {
                    myAttribs.Add(attrib.Name, attrib.Value);
                }
            }
            //把当前属性节点集合添加进myConfig
            myConfig.Add(section.Name, myAttribs);
            return myConfig;
        }       
    }
}