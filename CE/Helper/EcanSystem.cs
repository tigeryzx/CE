using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Reflection;

namespace CE.Helper
{
    /// <summary>
    /// 系统设置
    /// </summary>
    public class EcanSystem
    {
        private static string subKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run\";
        private static string cePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetCurrentAppName());
        private static string registKey = "ce";

        /// <summary>
        /// 设置CE开机运行
        /// </summary>
        public static void SetCeRunOnStart(bool IsRunOnStart)
        {
            if (IsRunOnStart)
                RunOnStart(registKey, cePath);
            else
                RemoveRunStart(registKey);
        }

        /// <summary>
        /// 检查是否己设置开机启动
        /// </summary>
        /// <returns></returns>
        public static bool IsSetRunOnStart()
        {
            RegistryKey loca = Registry.LocalMachine;
            RegistryKey run = loca.CreateSubKey(subKey);
            string value = Convert.ToString(run.GetValue(registKey));
            loca.Close();

            if (!string.IsNullOrEmpty(value) && value.ToLower() == cePath.ToLower())
                return true;
            else
                return false;
        }

        /// <summary>
        /// 设置开机启动
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="strProgramPath">程序路径</param>
        /// <returns></returns>
        public static void RunOnStart(string key, string strProgramPath)
        {
            RegistryKey loca = Registry.LocalMachine;
            RegistryKey run = loca.CreateSubKey(subKey);

            run.SetValue(key, strProgramPath);

            loca.Close();
        }

        /// <summary>
        /// 去除开机运行
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveRunStart(string key)
        {
            RegistryKey loca = Registry.LocalMachine.OpenSubKey(subKey,true);
            loca.DeleteValue(registKey,false);
            loca.Close();
        }

        /// <summary>
        /// 判断是否是允许开启全功能的系统
        /// </summary>
        /// <returns></returns>
        public static bool IsAllowsSystem()
        {
            OperatingSystem osinfo = Environment.OSVersion;
            int sysMinor = osinfo.Version.Minor;
            //2000、xp、2003
            if (osinfo.Platform == System.PlatformID.Win32NT && osinfo.Version.Major == 5 && 
                (sysMinor == 0 || sysMinor == 1 || sysMinor==2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取当前程序集的名称
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentAppName()
        {
            return Path.GetFileName(Assembly.GetExecutingAssembly().GetName().CodeBase);
        }
    }
}
