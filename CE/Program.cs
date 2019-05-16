using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using CE.Forms;
using CE.Helper;
using CE.Model;
using AutoUpdaterDotNET;
using System.Globalization;

namespace CE
{
    
    class Program
    {
        private static System.Threading.Mutex mutex;
        static XmlConfigHelper Config = null;

        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Config = XmlConfigHelper.GetInstance();
            }
            catch (Exception)
            {
                MessageBox.Show("程序运行失败，找不到配置文件!");
                return;
            }

            if (args == null || args.Length == 0)
            {

                mutex = new System.Threading.Mutex(true, "Ce");
                if (mutex.WaitOne(0, false))
                {
                    //检查更新
                    if(Config.GetCeConfig().CheckUpdate)
                        CheckUpdateHelper.GetInstance().CheckUpdate();

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(AutoTip.GetInstance());
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {

                Config.TransferToLower(args);
                App[] apps = Config.GetApp(args);

                //应用程序对象不为空，并且找到的程序要跟输入的对等否则全不启动
                if (apps != null && apps.Length > 0 && apps.Length == args.Length)
                {
                    ExecuteHelper.Execute(apps);
                }
                else//以下为找不到程序时执行
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    string[] names = Config.ComparisonAndGetNotFoundNameList(apps, args);
                    CeConfig cec = Config.GetCeConfig();
                    FormsHelper.ShowConfigForm(cec, names);

                    Application.Exit();
                }
            }


        }
    }
}
