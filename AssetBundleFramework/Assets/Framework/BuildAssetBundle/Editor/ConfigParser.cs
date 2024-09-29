/*********************************************
 ** FileName: ConfigParser.cs
 ** Date: Fri, 8 Oct 2021 19:44:18 GMT+0800
 ** Module: Package
 ** Description: 配置文件解析类
 **  
 ********************************************/


using System.Collections.Generic;
using System.Xml;
using UnityEditor;



    public class ConfigParser
    {
        public static string GetItemValue(string region, string key)
        {
            string xmlFilePath = EditorTools.GetProjectPath() + "/Config/config.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNode regionNode = doc.SelectSingleNode("/configs/" + region);
            if (regionNode != null)
            {
                return regionNode.Attributes[key].Value;
            }

            return "";
        }

        public static int GetReviewCode(string region)
        {
            string versionCode = GetItemValue(region, "version");
            string[] stringList = versionCode.Split('.');
            int code = int.Parse(stringList[2]) + 100;
            return code;
        }

        public static List<string> GetIgnoreResList()
        {
            string xmlFilePath = EditorTools.GetProjectPath() + "/Config/ignore.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFilePath);

            XmlNodeList nodeList = doc.SelectNodes("/ignores/item");
            List<string> _ignoreList = new List<string>();
            foreach (XmlNode node in nodeList)
            {
                _ignoreList.Add(node.InnerText);
            }

            return _ignoreList;
        }

        //public static List<string> GetBuildInResList()
        //{
        //    //string xmlFilePath = EditorTools.GetProjectPath() + "/Config/buildIn.xml";
        //    //XmlDocument doc = new XmlDocument();
        //    //doc.Load(xmlFilePath);

        //    //XmlNodeList nodeList = doc.SelectNodes("/buildIns/item");
        //    List<string> _buildinList = new List<string>();
        //    //foreach (XmlNode node in nodeList)
        //    //{
        //    //    _buildinList.Add(node.InnerText);
        //    //}

        //    return _buildinList;
        //}
    }

