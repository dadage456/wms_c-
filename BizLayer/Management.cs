using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace BizLayer
{
    public class Management
    {
        Management()
        {
            GetConfig();
        }

        /// <summary>
        /// 程序默认的WebService的虚拟路径地址
        /// </summary>
        public string DefaultBaseUrl
        {
            get { return this.url; }
        }

        string url = "";

        string station = string.Empty;
        public string DefaultStation
        {
            get { return station; }
        }

        /// <summary>
        /// 获取唯一的管理实例
        /// </summary>
        /// <returns></returns>
        public static Management GetSingleton()
        {
            object lockobj = new object();
            if (management == null)
            {
                lock (lockobj)
                {
                    if (management == null)
                    {
                        management = new Management();
                    }
                }
            }
            return management;
        }

        private static Management management;

        /// <summary>
        /// 获取默认设置的各种配置信息
        /// </summary>
        public void GetConfig()
        {
            XmlDocument doc = new XmlDocument();
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase; 
            appPath = appPath.Substring(0, appPath.LastIndexOf("\\") + 1);
            if (!File.Exists(Path.Combine(appPath, "App.cfg")))
                throw new ApplicationException("找不到配置文件！");
            
            try
            {
                doc.Load(Path.Combine(appPath, "App.cfg"));
            }
            catch (XmlException)
            {
                throw new ApplicationException("配置文件错误！");
            }

            XmlNodeList nodes = doc.DocumentElement.ChildNodes[0].ChildNodes;
            for (int i = 0; i < nodes.Count; i++)
            {
                bool isValid = nodes[i] is XmlElement;
                if (!isValid)
                    continue;
                XmlNode node = nodes[i];
                if (node.Attributes["key"].InnerText == "ServerUrl")
                {
                    this.url = node.Attributes["value"].InnerText; 
                }
                else if (node.Attributes["key"].InnerText == "Station")     //站点
                {
                    station = node.Attributes["value"].InnerText; 
                } 
            }
        }

        /// <summary>
        /// 保存配置文件中指定的元素
        /// </summary>
        public void SaveConfig(object content, EnumSaveContent contentType)
        {
            XmlDocument doc = new XmlDocument();
            string appPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase;
            appPath = appPath.Substring(0, appPath.LastIndexOf("\\") + 1);
            if (!File.Exists(Path.Combine(appPath, "App.cfg"))) 
                throw new ApplicationException("找不到配置文件！");
            
            try
            {
                doc.Load(Path.Combine(appPath, "App.cfg"));
            }
            catch (XmlException)
            {
                throw new ApplicationException("配置文件错误！");
            }

            XmlNodeList nodes = doc.DocumentElement.ChildNodes[0].ChildNodes;
            foreach (XmlNode node in nodes)
            {
                bool isValid = node is XmlElement;
                if (!isValid)
                    continue;
                if (contentType == EnumSaveContent.Url)
                {
                    if (node.Attributes["key"].InnerText == "ServerUrl")
                    {
                        node.Attributes["value"].InnerText = content.ToString();
                        this.url = content.ToString();
                        break;
                    } 
                }
                else if (contentType == EnumSaveContent.StationCode)
                {
                    if (node.Attributes["key"].InnerText == "Station")
                    {
                        node.Attributes["value"].InnerText = content.ToString();
                        this.station = content.ToString();
                        break;
                    }
                }
            }
            doc.Save(Path.Combine(appPath, "App.cfg"));
        }


        public bool CheckQuantity(string count)
        {
            //Regex re = new Regex("^\\+?[1-9][0-9]*$");
            //Regex re = new Regex("^\\d+(\\.\\d{1,3})?$");
            Regex re = new Regex("^\\d+(\\.\\d{1,7})?$");           //TODO:修改成小数点后7位，秦瑞20131219
            if (!re.IsMatch(count))
                return false;
            try
            {
                Decimal.Parse(count);
                if (Decimal.Parse(count) > 999999)
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string RemoveExcessZero(string input)
        {
            int dotIndex = input.IndexOf('.');//取小数点的索引，不存在则返回输入内容
            if (dotIndex < 0) return input;

            string decimalPlace = input.Substring(dotIndex + 1);//截取小数位

            Regex regularExpression = new Regex(@"0*$");
            int matchLength = regularExpression.Match(decimalPlace).Length;
            string result = input.Substring(0, input.Length - matchLength);

            if (decimalPlace.Length == matchLength) result = result.Substring(0, result.Length - 1);
            return result;
        }


        /// <summary>
        /// 配置管理中配置元素枚举
        /// </summary>
        public enum EnumSaveContent
        {
            Url,StationCode
        }

       
    }
}
