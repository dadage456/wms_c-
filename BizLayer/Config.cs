using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace BizLayer
{
    public class Config
    {
        public Config()
        {
            url = GetURL();
        }

        private static Config instance;
        public static Config Instance
        {
            get
            {
                if (instance == null) instance = new Config();
                return instance;
            }
        }



        private string url;
        public string DefaultURL
        {
            get { return this.url; }
        }

        public string GetURL()
        {
            XmlDocument doc = new XmlDocument();
            string path = this.getPath();

            try
            {
                doc.Load(path);
            }
            catch (XmlException xmlEx)
            {
                throw new ApplicationException("配置文件错误。", xmlEx);
            }

            XmlNodeList nodes = doc.DocumentElement.ChildNodes[0].ChildNodes;
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["key"].InnerText == "ServerUrl")
                {
                    this.url = node.Attributes["value"].InnerText;
                    return url;
                }
            }

            throw new ApplicationException("配置文件错误。");
        }

        private string getPath()
        {
            XmlDocument doc = new XmlDocument();
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            appPath = appPath.Substring(0, appPath.LastIndexOf("\\") + 1);
            string cfgPath = Path.Combine(appPath, "App.cfg");
            if (!File.Exists(cfgPath))
            {
                throw new ApplicationException("找不到配置文件。");
            }
            return cfgPath;
        }
    }
}
