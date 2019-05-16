using System;
using System.IO;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;
using CE.Helper;
using System.Diagnostics;
using System.Reflection;

namespace CE.Forms
{
    public partial class CeXmlConfig : Form
    {
        #region 声明
        XmlConfigHelper config = XmlConfigHelper.GetInstance();
        bool IsRegistAutoStart = false;
        private static CeXmlConfig cxc = new CeXmlConfig(); 
        #endregion

        #region 构造函数
        public static CeXmlConfig GetInstance()
        {
            if (cxc == null || cxc.IsDisposed)
                cxc = new CeXmlConfig();
            return cxc;
        }

        public CeXmlConfig()
        {
            InitializeComponent();
        } 
        #endregion

        #region 初始化控件

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            var ceConfig = config.GetCeConfig();
            cbWhenNotFoundShowTip.Checked = ceConfig.WhenNotFoundShowTip;
            cbCheckUpdate.Checked = ceConfig.CheckUpdate;
            nudFindAppSimilarityPercent.DecimalPlaces = 2;
            nudFindAppSimilarityPercent.Increment = 0.1M;
            nudFindAppSimilarityPercent.Value = ceConfig.FindAppSimilarityPercent;

            txtShortcutkey.Text = ceConfig.Shortcutkey;

            if (!EcanSystem.IsAllowsSystem())
            {
                this.btnSetRunTipAutoRun.Enabled = false;
            }
            else
            {
                BtnTextChange();
            }
        }

        #endregion

        #region 控件回调函数

        protected override void OnLoad(EventArgs e)
        {
            InitControl();
            this.Text += "_" + this.AssemblyFileVersion;
            base.OnLoad(e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var ceConfig = config.GetCeConfig();
            ceConfig.WhenNotFoundShowTip = cbWhenNotFoundShowTip.Checked;
            ceConfig.FindAppSimilarityPercent = nudFindAppSimilarityPercent.Value;
            ceConfig.Shortcutkey = string.IsNullOrEmpty(txtShortcutkey.Text.Trim()) ? "Q" : txtShortcutkey.Text;
            ceConfig.CheckUpdate = cbCheckUpdate.Checked;

            try
            {
                config.SaveCeConfig(ceConfig);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            MessageBox.Show("保存成功!");
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            Repair();
        }

        /// <summary>
        /// 清除无效App
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            var appList = config.GetInvalidAppInfo(out message);

            if (!string.IsNullOrEmpty(message) && appList != null && appList.Count > 0)
            {
                DialogResult dresult = MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);

                if (dresult == DialogResult.Yes)
                {
                    config.ClearInvalidApp(appList.ToArray());
                    MessageBox.Show("清除成功!");
                }
            }
            else
            {
                message = "没有找到失效的App!无需清理！";
                MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            DialogResult dresult = MessageBox.Show("确认清除所有App配置？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
            if (dresult == DialogResult.Yes)
            {
                config.ClearApp();
                MessageBox.Show("清除成功!");
            }
            
        }

        #region 设置提示程序开机启动

        /// <summary>
        /// 设置提示程序开机启动
        /// </summary>
        private void btnSetRunTipAutoRun_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.IsRegistAutoStart)
                    EcanSystem.SetCeRunOnStart(false);
                else
                    EcanSystem.SetCeRunOnStart(true);
                BtnTextChange();
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.Message);
                return;
            }

            MessageBox.Show("设置成功!");
        }
        #endregion

        #endregion

        #region 修复操作

        /// <summary>
        /// 修复操作
        /// </summary>
        private void Repair()
        {
            string message = "点击确认即会执行以下步骤：\r\n1.生成配置文件{0}到{1}目录\r\n2.自动在用户环境变量Path中添加{2}";
            string CeAppIniName = "CeApp.ini";
            string copyTagetPath = Directory.GetParent(System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop)).ToString();
            string copyTagetFullPath = Path.Combine(copyTagetPath, CeAppIniName);
            string CeAppConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CeAppConfig.xml");
            string ApplicationName = EcanSystem.GetCurrentAppName();
            string CeApplicationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ApplicationName);
            string CeDirPath = AppDomain.CurrentDomain.BaseDirectory;

            DirectorySecurity DirHasPower = new DirectorySecurity(copyTagetPath, AccessControlSections.Access);
            //验证
            bool flag = true;
            if (!Directory.Exists(copyTagetPath))
            {
                message = string.Format("目标路径{0}不存在！", copyTagetPath);
                flag = false;
            }
            if (!File.Exists(CeApplicationPath))
            {
                message = string.Format("CE.exe程序不存于{0}！", CeApplicationPath);
                flag = false;
            }

            if (!flag)
            {
                MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //开始修复
            DialogResult result = MessageBox.Show(
                    string.Format(message, CeAppIniName, copyTagetPath, CeDirPath),
                    "提示",
                    MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                try
                {
                    //Setp 1: 配置文件生成
                    string content = CeApplicationPath;
                    File.WriteAllText(copyTagetFullPath, content, Encoding.UTF8);

                    //Setp 2:环境变量修复
                    SetVarPath(CeDirPath);

                    //检查配置文件
                    if (!File.Exists(copyTagetFullPath))
                    {
                        File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CeAppIniName), content, Encoding.UTF8);
                        MessageBox.Show(string.Format("配置文件可能因为权限原因没成功复制到{0}稍候请手动复制程序目录下的{1}到以上目录即可！", copyTagetPath,CeAppIniName)
                            , "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(string.Format("修复失败,原因：{0}", e.Message), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("修复成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        /// <summary>
        /// 环境变量修复
        /// </summary>
        private void SetVarPath(string CeDirPath)
        {
            CeDirPath = CeDirPath.ToLower();

            string strVarPath = EnvironmentVariable.GetPath();
            
            //判断是否有这个变量
            if (string.IsNullOrEmpty(strVarPath))
                strVarPath = string.Empty;

            //判断是否己配置过了？
            if (strVarPath.ToLower().IndexOf(CeDirPath) == -1)//修改
            {
                //新增
                EnvironmentVariable.SetPath(CeDirPath);
            }

        } 
        #endregion

        #region 设置快捷键
        /// <summary>
        /// 设置快捷键
        /// </summary>
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            txtShortcutkey.Text = e.KeyCode.ToString();
        } 
        #endregion

        #region 注册开机启动按钮提示变换

        /// <summary>
        /// 注册开机启动按钮提示变换
        /// </summary>
        public void BtnTextChange()
        {
            this.IsRegistAutoStart = EcanSystem.IsSetRunOnStart();
            if (this.IsRegistAutoStart)
                btnSetRunTipAutoRun.Text = "取消开机提示程序启动";
            else
                btnSetRunTipAutoRun.Text = "设置开机提示程序启动";
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

    }
}
