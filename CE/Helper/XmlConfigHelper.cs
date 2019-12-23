using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using CE.Model;
using System.Reflection;
using System.IO;

namespace CE.Helper
{
    /// <summary>
    /// XML配置文件辅助类
    /// </summary>
    public class XmlConfigHelper
    {
        #region 声明
        public XDocument xmlDoc { get; set; }
        public string ConfigPath = string.Empty;
        private static XmlConfigHelper xmlConfigHelper = null;
        #endregion

        #region 配置文档操作

        /// <summary>
        /// 检查配置文件是否存在
        /// </summary>
        /// <returns></returns>
        public static bool CheckConfigExists()
        {
            return !string.IsNullOrEmpty(GetXmlPath());
        }

        /// <summary>
        /// 获取xml配置文件的地址
        /// </summary>
        /// <returns></returns>
        private static string GetXmlPath()
        {
            string result = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CeAppConfig.xml");
            if (File.Exists(path))
                result = path;
            else
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CeApp.ini");
                if(File.Exists(path))
                    result = File.ReadAllText(path, Encoding.UTF8);
            }
            return result;
        }

        /// <summary>
        /// 实例化指定路径的配置文件
        /// </summary>
        /// <param name="ConfigPath"></param>
        private XmlConfigHelper(string ConfigPath)
        {
            InitConfigFile(ConfigPath);
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static XmlConfigHelper GetInstance ()
        {
            //检查配置文件是否存在
            if (!CheckConfigExists())
            {
                throw new Exception("程序运行失败，找不到配置文件！");
            }

            //默认配置文件的名称
            if (xmlConfigHelper == null)
                xmlConfigHelper = new XmlConfigHelper(GetXmlPath());
            return xmlConfigHelper;
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private void InitConfigFile(string filePath)
        {
            this.ConfigPath = filePath;
            if (xmlDoc == null)
            {
                xmlDoc = XDocument.Load(ConfigPath);
            }
        }

        /// <summary>
        /// 重新加载配置文件
        /// </summary>
        public void ReloadConfig()
        {
            //检查配置文件是否存在
            if (string.IsNullOrEmpty(ConfigPath) && !File.Exists(ConfigPath))
                throw new Exception("配置加载失败，找不到配置文件！");
            else
                xmlDoc = XDocument.Load(ConfigPath);
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        private void SaveConfig()
        {
            if (xmlDoc != null)
                xmlDoc.Save(ConfigPath);
        }
        #endregion

        #region 获取App对象

        /// <summary>
        /// 自动筛选获程序
        /// </summary>
        /// <param name="AppName"></param>
        /// <param name="similarity"></param>
        /// <returns></returns>
        public List<App> GetContainsApp(string AppName)
        {
            List<App> applist = null;
            var SingleAppNameList = from i in xmlDoc.Descendants("App")
                                    where
                                    i.Attribute("name").Value.ToLower().Contains(AppName.ToLower())
                                    orderby Convert.ToInt32(i.Attribute("runTime").Value) descending
                                    select i.Attribute("name").Value;

            if (SingleAppNameList != null)
            {
                applist = GetApp(SingleAppNameList.ToArray()).ToList();
            }
            return applist;
        }


        /// <summary>
        /// 按相似度获程序
        /// </summary>
        /// <param name="AppName"></param>
        /// <param name="similarity"></param>
        /// <returns></returns>
        public List<App> GetSimilarityApp(string AppName, decimal similarity)
        {
            List<App> applist = null;
            var SingleAppNameList = from i in xmlDoc.Descendants("App")
                                    where LevenshteinDistance.Instance.LevenshteinDistancePercent(
                                    i.Attribute("name").Value.ToLower(),AppName.ToLower())>=similarity
                                    orderby Convert.ToInt32(i.Attribute("runTime").Value) descending
                                    select i.Attribute("name").Value;
            
            if (SingleAppNameList != null)
            {
                applist = GetApp(SingleAppNameList.ToArray()).ToList();
            }
            return applist;
        }

        /// <summary>
        /// 获取所有单项程序
        /// </summary>
        /// <returns></returns>
        public List<App> GetAllSingleApps()
        {
            List<App> applist = null;
            var SingleAppNameList = from i in xmlDoc.Descendants("App")
                       where !Convert.ToBoolean(i.Attribute("multi").Value)
                                    select i.Attribute("name").Value.ToString();

            if (SingleAppNameList != null)
            {
                applist = GetApp(SingleAppNameList.ToArray()).ToList();
            }
            return applist;
        }

        /// <summary>
        /// 获取App对象
        /// </summary>
        /// <param name="AppName">对象名</param>
        public App GetApp(string AppName)
        {
            App app = null;
            app = GetApp(app, AppName);
            return app;
        }

        /// <summary>
        /// 获取App对象
        /// </summary>
        /// <param name="AppName">对象名</param>
        public App[] GetApp(params string[] AppNames)
        {
            List<App> list = new List<App>();
            foreach (string AppName in AppNames)
            {
                var app = GetApp(AppName);
                if(app!=null)
                    list.Add(app);
            }
            return list.ToArray();
        }

        private App GetApp(App AppParent, string AppName)
        {
            App app = null;
            if (string.IsNullOrEmpty(AppName))
                return app;


            app = (from i in xmlDoc.Descendants("App")
                   where i.Attribute("name").Value == AppName
                   select new App
                   {
                       name = AppName,
                       multi = Convert.ToBoolean(i.Attribute("multi").Value ?? "false"),
                       runTime = Convert.ToInt32(i.Attribute("runTime").Value ?? "0"),
                       path = i.Value,
                       isNetWorkPath = i.Attribute("isNetWorkPath") == null ? false : Convert.ToBoolean(i.Attribute("isNetWorkPath").Value),
                   }).SingleOrDefault();

            //如果有子节点
            if (app != null && app.multi)
            {
                //找出当前APP节点下的子节点并且忽略引用自己的
                var result = (from e in
                                  (from i in xmlDoc.Descendants("App")
                                   where i.Attribute("name").Value == AppName
                                   select i).Nodes()
                              select (XElement)e).Where(i => i.Attribute("name").Value != AppName);
                foreach (var item in result)
                {
                    //递归
                    GetApp(app, item.Attribute("name").Value);
                }
            }
            else if (AppParent != null && !app.multi)//如果有父节点添加到其中(则该节点是单节点)
            {
                AppParent.sub.Add(app);
                return AppParent;
            }

            return app;
        }
        #endregion

        #region 对比应用对象跟输入名称得出找不到对象的名称列表
        /// <summary>
        /// 对比应用对象跟输入名称得出找不到对象的名称列表
        /// </summary>
        public string[] ComparisonAndGetNotFoundNameList(App[] apps, string[] names)
        {
            List<string> list = new List<string>();

            foreach (string name in names)
            {
                bool IsExist = false;
                foreach (App app in apps)
                {
                    if (app.Equals(name))
                        IsExist = true;
                }
                if (!IsExist)
                    list.Add(name);
            }

            return list.ToArray();
        } 
        #endregion

        #region 保存APP对象

        /// <summary>
        /// 保存APP对象
        /// </summary>
        /// <param name="app"></param>
        public void SaveApp(App app)
        {
            var result = xmlDoc.Descendants("Apps").SingleOrDefault();

            if (app != null && result != null)
            {
                //检查当前不存在同名节点[新增节点]
                if (!IsExistsApp(app.name))
                {
                    var x = new XElement("App",
                        new XAttribute("name", app.name),
                        new XAttribute("multi", app.multi),
                        new XAttribute("runTime", app.runTime),
                        new XAttribute("isNetWorkPath", app.isNetWorkPath)
                        );

                    //是否是多项启动
                    if (!app.multi)
                    {
                        x.Value = app.path;
                    }
                    else
                    {
                        foreach (var sub in app.sub)
                        {
                            x.Add(new XElement("sub",
                                new XAttribute("name", sub.name)
                            ));
                        }
                    }
                    result.Add(x);
                }
                else //修改节点
                {
                    XElement x = GetAppXmlObject(app.name);
                    x.Value = app.path;
                }

                SaveConfig();
            }
        }
        #endregion

        #region 记录运行次数
        /// <summary>
        /// 记录运行次数
        /// </summary>
        /// <param name="apps"></param>
        public void RecordRunTime(params App[] apps)
        {
            if (apps == null)
                return;
            foreach (App app in apps)
            {
                var x = (from i in xmlDoc.Descendants("Apps").Nodes()
                         where ((XElement)i).Attribute("name").Value.ToLower() == app.name.ToLower()
                         select (XElement)i).SingleOrDefault();

                if (x != null)
                {
                    x.SetAttributeValue("runTime", Convert.ToInt32(x.Attribute("runTime").Value) + 1);

                }
            }

            SaveConfig();
        } 
        #endregion

        #region 检查指定的APP名称是否存在
        /// <summary>
        /// 检查指定的APP名称是否存在
        /// </summary>
        /// <param name="AppName"></param>
        /// <returns></returns>
        public bool IsExistsApp(string AppName)
        {
            var currentNode = xmlDoc.Descendants("App").Where(i => i.Attribute("name").Value == AppName).SingleOrDefault();

            if (currentNode != null)
                return true;
            return false;
        }
        #endregion

        #region 获取应用配置的XML信息

        /// <summary>
        /// 获取应用配置的XML信息
        /// </summary>
        /// <param name="AppName"></param>
        /// <returns></returns>
        public XElement GetAppXmlObject(string AppName)
        {
            return xmlDoc.Descendants("App").Where(i => i.Attribute("name").Value == AppName).SingleOrDefault();
        } 
        #endregion

        #region 获取程序运行的基本配置

        /// <summary>
        /// 获取程序运行的基本配置
        /// </summary>
        /// <returns></returns>
        public CeConfig GetCeConfig()
        {
            CeConfig ce = new CeConfig();

            foreach (var ceProp in typeof(CeConfig).GetProperties())
            {
                string name = ceProp.Name;
                var x = (from i in xmlDoc.Descendants("AppConfig").Nodes()
                         where i is XElement && ((XElement)i).Name.ToString().ToLower() == name.ToLower()
                         select (XElement)i).SingleOrDefault();
                if (x != null)
                    SetValue(ce, ceProp, x.Value);
            }

            return ce;
        }

        /// <summary>
        /// 保存配置到文件
        /// </summary>
        /// <param name="config"></param>
        public void SaveCeConfig(CeConfig config)
        {
            if (config == null)
                return;
            foreach (var ceProp in typeof(CeConfig).GetProperties())
            {
                string name = ceProp.Name;
                object value = ceProp.GetValue(config, null);
                var x = (from i in xmlDoc.Descendants("AppConfig").Nodes()
                         where i is XElement && ((XElement)i).Name.ToString().ToLower() == name.ToLower()
                         select (XElement)i).SingleOrDefault();
                if (x != null)
                    x.Value = value.ToString();
                else
                {
                    //如果不存在节点就创建一个
                    var AppConfigNode = xmlDoc.Descendants("AppConfig").SingleOrDefault();
                    var NewSetting = new XElement(name);
                    NewSetting.Value = value.ToString();
                    AppConfigNode.Add(NewSetting);
                }
            }

            SaveConfig();
        }

        /// <summary>
        /// 设置值
        /// </summary>
        private void SetValue(object obj,PropertyInfo Prop, object value)
        {
            if (Prop.PropertyType.Equals(typeof(bool)))
            {
                Prop.SetValue(obj, Convert.ToBoolean(value), null);
            }
            else if (Prop.PropertyType.Equals(typeof(int)))
            {
                Prop.SetValue(obj, Convert.ToInt32(value), null);
            }
            else if (Prop.PropertyType.Equals(typeof(string)))
            {
                Prop.SetValue(obj, Convert.ToString(value), null);
            }
            else if (Prop.PropertyType.Equals(typeof(double)))
            {
                Prop.SetValue(obj, Convert.ToDouble(value), null);
            }
            else if (Prop.PropertyType.Equals(typeof(decimal)))
            {
                Prop.SetValue(obj, Convert.ToDecimal(value), null);
            }
        }
        #endregion

        #region 字符串组转为小写
        /// <summary>
        /// 字符串组转为小写
        /// </summary>
        /// <param name="args"></param>
        public void TransferToLower(string[] args)
        {
            for (int i = 0; null != args && i < args.Length; i++)
            {
                args[i] = args[i].ToLower();
            }
        } 
        #endregion

        #region 清除无效的App

        /// <summary>
        /// 清除所有App
        /// </summary>
        public void ClearApp()
        {
            xmlDoc.Descendants("App").Remove();
            SaveConfig();
        }

        /// <summary>
        /// 获取无效的APP信息
        /// </summary>
        /// <param name="DeleteLog"></param>
        /// <param name="RecordCount"></param>
        public List<App> GetInvalidAppInfo(out string DeleteLog)
        {
            DeleteLog = string.Empty;
            int RecordCount = 0;
            StringBuilder sb = new StringBuilder();
            List<App> deleAppList = new  List<App>();
            var SingleAppList = GetAllSingleApps();
            if (SingleAppList == null || SingleAppList.Count <= 0)
                return null;

            foreach (var app in SingleAppList)
            {
                string path = app.path;
                if (app.isNetWorkPath)
                    continue;

                if (!File.Exists(path) && !Directory.Exists(path))
                {
                    deleAppList.Add(app);
                    sb.AppendFormat("程序:{0} 路径:{1}\r\n", app.name, app.path);
                    RecordCount++;
                }
            }

            DeleteLog = string.Format("无效APP共{0}个,以下是将要清除的明细,点击[确认]进行删除:\r\n{1}", RecordCount, sb.ToString());
            return deleAppList;
        }

        /// <summary>
        /// 清除无效的App
        /// </summary>
        public void ClearInvalidApp()
        {
            var SingleAppList = GetAllSingleApps();
            if (SingleAppList == null || SingleAppList.Count <= 0)
                return;

            foreach (var app in SingleAppList)
            {
                string path = app.path;
                if (!File.Exists(path) && !Directory.Exists(path))
                    DeleteAppAndSub(app.name);
            }
        }

        /// <summary>
        /// 清除指定无效的App
        /// </summary>
        public void ClearInvalidApp(params App[] apps)
        {
            if (apps == null)
                return;
            foreach (var app in apps)
            {
                DeleteAppAndSub(app.name);
            }
        }

        /// <summary>
        /// 删除一个App和引用它的Sub
        /// </summary>
        public void DeleteAppAndSub(App app)
        {
            if (app != null && !string.IsNullOrEmpty(app.name))
                DeleteAppAndSub(app.name);
        }

        /// <summary>
        /// 删除一个App和引用它的Sub
        /// </summary>
        public void DeleteAppAndSub(string AppName)
        {
            if (string.IsNullOrEmpty(AppName))
                return;

            //先删除对他引用的子程序
            var sub = from i in xmlDoc.Descendants("sub")
                      where i.Attribute("name").Value.ToLower()==AppName.ToLower()
                      select i;

            if (sub != null)
                sub.Remove();

            //删除程序连接自己
            var app = from i in xmlDoc.Descendants("App")
                      where i.Attribute("name").Value.ToLower() == AppName
                      select i;

            if (app != null)
                app.Remove();

            SaveConfig();
        }

        #endregion
    }
}
