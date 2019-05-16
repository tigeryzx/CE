using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CE.Forms;
using CE.Model;

namespace CE.Helper
{
    public class FormsHelper
    {
     
        public static void ShowConfigForm(CeConfig config, string[] appNames)
        {
            if (config != null && config.WhenNotFoundShowTip)
            {
                foreach (var item in appNames)
                {
                    bool IsOpen = false;
                    //只是单项启动时才显示推荐框
                    if (appNames.Length == 1)
                    {
                        IsOpen = ShowAppTipBox(item, config.FindAppSimilarityPercent);
                    }

                    //判断推荐窗口是否打开过，如果没打开过则打开添加窗口
                    if (IsOpen)
                        return;
                    
                    DialogResult result = ShowAddAppMessage(item);
                    if (DialogResult.Cancel == result)
                        break;
                    if (DialogResult.No == result)
                        continue;
                    if (DialogResult.Yes == result)
                    {
                        DialogResult result2 = ShowAddModelSelect();
                        if (result2 == DialogResult.Yes)
                        {
                            var addSingleForm = new AddSingleApp();
                            addSingleForm.AppName = item;
                            //用模态窗口阻塞
                            addSingleForm.ShowDialog();
                        }
                        else if (result2 == DialogResult.No)
                        {
                            var addMultiApp = new AddMultiApp();
                            addMultiApp.AppName = item;
                            //用模态窗口阻塞
                            addMultiApp.ShowDialog();
                        } 
                    }
                    
                }
            }

            //结束应用程序
            //Application.Exit();

        }

        private static DialogResult ShowAddModelSelect()
        {
            return MessageBox.Show("是否以单项模式添加？\r\n[确认]单项模式添加\r\n[否]现在程序关联添加\r\n[取消]关闭窗口取消添加",
                "提示",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
        }

        private static DialogResult ShowAddAppMessage(string appName)
        {
            return MessageBox.Show(string.Format(
                "你要运行的程序{0}没有查找到，是否需要添加配置!\r\n[确认]进行添加\r\n[否]跳过一项\r\n[取消]全不添加",appName), 
                "添加提示", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
        }

        /// <summary>
        /// 提示相似程序
        /// </summary>
        /// <param name="AppName"></param>
        /// <param name="similarity"></param>
        /// <returns></returns>
        public static bool ShowAppTipBox(string AppName, decimal similarity)
        {
            bool flag = false;
            List<App> applist = XmlConfigHelper.GetInstance().GetSimilarityApp(AppName, similarity);
            if (applist != null && applist.Count > 0)
            {
                ShowAppTip showAppTip = new ShowAppTip();
                showAppTip.Similarity = similarity;
                showAppTip.AppName = AppName;
                showAppTip.AppList = applist;

                flag = true;

                //模态窗口
                showAppTip.ShowDialog();
            }
            return flag;
        }
    }
}
