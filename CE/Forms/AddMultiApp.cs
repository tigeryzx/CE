using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CE.Helper;
using CE.Model;

namespace CE.Forms
{
    public partial class AddMultiApp : Form
    {
        #region 声明
        static XmlConfigHelper Config = XmlConfigHelper.GetInstance();
        #endregion

        public AddMultiApp()
        {
            InitializeComponent();
        }

        #region 控件回调函数

        protected override void OnLoad(EventArgs e)
        {
            InitControl();
            base.OnLoad(e);
        }

        #endregion

        #region 初始化控件
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            clbApp.DataSource = Config.GetAllSingleApps();
        } 
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            IEnumerable<App> sublist = clbApp.CheckedItems.Cast<App>();
            if (validation() && sublist != null)
            {
                App app = new App();
                app.name = name;
                app.sub.AddRange(sublist);
                Config.SaveApp(app);

                if (cbRun.Checked)
                    ExecuteHelper.Execute(app);
                this.DialogResult = DialogResult.OK;
                //关闭自己
                this.Close();
            }
        } 
        #endregion

        #region 验证信息

        /// <summary>
        /// 验证信息
        /// </summary>
        /// <returns></returns>
        private bool validation()
        {
            string name = txtName.Text;
            IEnumerable<App> sublist = clbApp.CheckedItems.Cast<App>();
            bool Result = true;
            string MessageText = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                MessageText = "名称不能为空!";
                Result = false;

            }

            if (sublist == null || sublist.Count() <= 0)
            {
                MessageText = string.Format("至少要选择一个子项!");
                Result = false;
            }

            if (Config.IsExistsApp(name))
            {
                MessageText = string.Format("你指定的名称[{0}]己存在配置中，请重新指定!", name);
                Result = false;
            }


            if (!Result)
                MessageBox.Show(MessageText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return Result;
        }
        #endregion

        #region 属性

        public string AppName
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value;
            }
        } 
        #endregion
    }
}
