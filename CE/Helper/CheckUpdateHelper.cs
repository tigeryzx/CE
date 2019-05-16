using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CE.Helper
{
    /// <summary>
    /// 检查更新辅助类
    /// </summary>
    public class CheckUpdateHelper
    {
        private static CheckUpdateHelper _CheckUpdateHelper;

        private CheckUpdateHelper()
        {

        }

        public static CheckUpdateHelper GetInstance()
        {
            if (_CheckUpdateHelper == null)
                _CheckUpdateHelper = new CheckUpdateHelper();
            return _CheckUpdateHelper;
        }

        /// <summary>
        /// 检查更新
        /// </summary>
        public void CheckUpdate()
        {
            var CurrentVersion = Environment.Version.ToString();
            string UpdateUrl = "http://tigeryzx.xicp.net:8081/Ce_Net40.xml";
            if (CurrentVersion.StartsWith("2."))
                UpdateUrl = "http://localhost:59774/UpdateChecker/GetN35UpdateInfo";
            
            //设置下载目录为当前应用目录
            AutoUpdater.UpdateDownloadPath = AppDomain.CurrentDomain.BaseDirectory;
            AutoUpdater.Start(UpdateUrl);

            //如果需要自定义更新则使用以下方式
            //AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
           if(args==null)
               MessageBox.Show(
               @"访问更新服务器出错，请稍候再试.",
               @"检查更新失败", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if(!args.IsUpdateAvailable)
                MessageBox.Show(@"当前版本己经是最新版本", @"没检查到新版本",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
