using Microsoft.Win32;
using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;

namespace CE.Helper
{
    
    #region 获取&设置环境变量（调用系统API）

    /// <summary>
    /// 获取&设置环境变量（调用系统API）
    /// </summary>
    public class EnvironmentVariable
    {
        public static void SetPath(string pathValue)
        {
            string pathlist;
            pathlist = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);

            if (string.IsNullOrEmpty(pathlist))
                pathlist = string.Empty;

            string[] list = pathlist.Split(';');
            bool isPathExist = false;

            foreach (string item in list)
            {
                if (item == pathValue)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                if (string.IsNullOrEmpty(pathlist) || pathlist.Trim()==string.Empty)
                    Environment.SetEnvironmentVariable("Path", pathValue, EnvironmentVariableTarget.User);
                else
                    Environment.SetEnvironmentVariable("Path", pathlist + ";" + pathValue, EnvironmentVariableTarget.User);
            }

        }

        public static void SetAllPath(string pathValue)
        {
            Environment.SetEnvironmentVariable("Path", pathValue, EnvironmentVariableTarget.User);
        }

        public static string GetPath()
        {
            string pathlist = string.Empty;
            pathlist = Environment.GetEnvironmentVariable("Path", EnvironmentVariableTarget.User);
            return pathlist;
        }
    } 
    #endregion
}
