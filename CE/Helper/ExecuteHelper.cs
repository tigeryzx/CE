using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using CE.Model;
using System.Windows.Forms;
using CE.Forms;
using System.Text.RegularExpressions;

namespace CE.Helper
{
    public class ExecuteHelper
    {

        /// <summary>
        /// 启动多个程序
        /// </summary>
        /// <param name="Name"></param>
        public static void Execute(params App[] apps)
        {
            Execute(false, apps);
        }

        /// <summary>
        /// 启动多个程序
        /// </summary>
        /// <param name="Name"></param>
        public static void Execute(bool IsOpenDir,params App[] apps)
        {
            foreach (App app in apps)
            {
                if (app.multi)
                {
                    foreach (App a in app.sub)
                    {
                        Execute(a,IsOpenDir);
                    }
                }
                else
                {
                    Execute(app,IsOpenDir);
                }
            }

            //更新运行次数
            XmlConfigHelper.GetInstance().RecordRunTime(apps);
        }

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="name"></param>
        /// <param name="IsOpenDir">是否只是打开目录</param>
        static void Execute(App app,bool IsOpenDir)
        {
            if (app != null && !string.IsNullOrEmpty(app.path))
            {
                string ProgramPath = app.path;

                var Match = Regex.Match(ProgramPath, "%(.+)%");
                //验证是否有系统变量的路径，有则进行替换
                if (Match != null && Match.Groups.Count>1)
                {
                    string varName = Match.Groups[1].Value;
                    string varPath = System.Environment.GetEnvironmentVariable(varName);
                    ProgramPath = ProgramPath.Replace(Match.Groups[0].Value, varPath); ;
                }

                if (app.isNetWorkPath)
                {
                    Process.Start(app.path);
                }
                else if (File.Exists(ProgramPath) || Directory.Exists(ProgramPath))
                {
                    if (IsOpenDir && File.Exists(ProgramPath))
                        ProgramPath = "/select," + ProgramPath;
                    Process.Start("Explorer.exe", ProgramPath);
                }
                else
                {
                    DialogResult dr = MessageBox.Show(string.Format("你要执行的程序[{0}]:{1}路径己更改或删除!\r\n[是]修改应用路径\r\n[否]删除应用信息",app.name,app.path), "修改提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        AddSingleApp asa = new AddSingleApp();
                        asa.AppName = app.name;
                        asa.IsEdit = true;
                        asa.ShowDialog();
                    }
                    else if (dr == DialogResult.No)
                    {
                        XmlConfigHelper.GetInstance().DeleteAppAndSub(app.name);
                    }
                }
            }

        }
    }
}
