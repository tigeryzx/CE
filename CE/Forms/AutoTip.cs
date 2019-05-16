using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CE.Helper;
using CE.Model;
using System.Reflection;

namespace CE.Forms
{
    public partial class AutoTip : Form
    {
        #region 声明
        bool RunFirstTime = true;
        XmlConfigHelper Config = XmlConfigHelper.GetInstance();
        List<App> allApp = null;
        Hotkey hk = null;
        #endregion

        #region 构造函数

        private static AutoTip at = new AutoTip();
        public static AutoTip GetInstance()
        {
            return at;
        }

        public AutoTip()
        {
            InitializeComponent();
            //初始化窗体控件
            this.TopMost = true; //为前置窗口
            this.ShowInTaskbar = false;  //不显示在系统任务栏
            notifyIcon.Visible = true;  //托盘图标可见
            this.AcceptButton = button1;
            this.Activate();
            //Alt+Tab 时不显示
            //ToolBarHelper.SetFormToolWindowStyle(this);

            this.Text += "_" + this.ProductVersion;

            //加载APP缓存
            ReloadCacheData();
        } 
        #endregion

        #region 加载APP缓存

        /// <summary>
        /// 加载APP缓存
        /// </summary>
        protected void ReloadCacheData()
        {
            Config.ReloadConfig();
            allApp = Config.GetAllSingleApps();
            string[] sour = (from i in allApp
                             select i.name).ToArray();
            comboBox1.AutoCompleteCustomSource.Clear();
            comboBox1.AutoCompleteCustomSource.AddRange(sour);
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }
        #endregion

        #region 运行程序

        /// <summary>
        /// 运行程序
        /// </summary>
        private void RunApp(bool IsOpenDir)
        {
            string strAppNames = comboBox1.Text;

            string[] args = strAppNames.Split(' ');

            if (args.Length <= 0 || (args.Length == 1 && string.IsNullOrEmpty(args[0].Trim())))
                return;

            Config.TransferToLower(args);
            App[] apps = Config.GetApp(args);

            //应用程序对象不为空，并且找到的程序要跟输入的对等否则全不启动
            if (apps != null && apps.Length > 0 && apps.Length == args.Length)
            {
                ExecuteHelper.Execute(IsOpenDir, apps);
            }
            else//以下为找不到程序时执行
            {
                string[] names = Config.ComparisonAndGetNotFoundNameList(apps, args);
                CeConfig cec = Config.GetCeConfig();
                FormsHelper.ShowConfigForm(cec, names);
            }
            this.Hide();
        } 
        #endregion

        #region 控件事件

        private void button1_Click(object sender, EventArgs e)
        {
            RunApp(false);
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Opacity = 1;
            this.Show();
        }

        private void AutoTip_FormClosing(object sender, FormClosingEventArgs e)
        {
            //用户关闭时
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true; //取消关闭事件
                this.Hide();
            }
        } 
        #endregion

        #region 注册全局热键

        private void AutoTip_Load(object sender, EventArgs e)
        {
            CeConfig cec = Config.GetCeConfig();
            Keys k = Keys.Q;

            if (!string.IsNullOrEmpty(cec.Shortcutkey))
                k = (Keys)Enum.Parse(typeof(Keys), cec.Shortcutkey,true);

            hk = new Hotkey(Handle);
            hk.RegisterHotkey(k, Hotkey.KeyFlags.MOD_WIN);
            hk.OnHotkey += hk_OnHotkey;

            if (this.RunFirstTime)
            {
                this.Opacity = 0;
                this.RunFirstTime = false;
            }
        }

        void hk_OnHotkey(int HotKeyID)
        {
            if (this.Visible)
                this.Hide();
            else
            {
                this.Opacity = 1;
                this.Show();
                this.Activate();
            }
        }

        private void AutoTip_FormClosed(object sender, FormClosedEventArgs e)
        {
            hk.UnregisterHotkeys();
        }

        private void AutoTip_VisibleChanged(object sender, EventArgs e)
        {
            //显示时刷新一下APP缓存
            if (this.Visible)
                ReloadCacheData();
        }
        #endregion

        #region 托盘菜单项

        /// <summary>
        /// 完全退出
        /// </summary>
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 打开设置界面
        /// </summary>
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CeXmlConfig.GetInstance().Show();
        }

        #endregion

        #region ESC键和回车时事件

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Hide();

            if (e.Modifiers.CompareTo(Keys.Alt) == 0 && e.KeyCode == Keys.Enter)
                RunApp(true);
            else if (e.KeyCode == Keys.Enter)
                RunApp(false);

        }

        #endregion

        #region 第一次显示时最小化
        /// <summary>
        /// 第一次显示时最小化
        /// </summary>
        private void AutoTip_Shown(object sender, EventArgs e)
        {
            this.Hide();
        } 
        #endregion

        #region 属性
        public string AssemblyFileVersion
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false);
                if (attributes.Length == 0)
                    return "";
                return ((AssemblyFileVersionAttribute)attributes[0]).Version;
            }
        }

        #endregion

        #region 显示更新日志

        /// <summary>
        /// 显示更新日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLogPlan oUpdateLogPlan = new UpdateLogPlan();
            oUpdateLogPlan.ShowDialog();
        } 

        #endregion

        #region 检查更新

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdateHelper.GetInstance().CheckUpdate();
        } 
        #endregion


    }
}
