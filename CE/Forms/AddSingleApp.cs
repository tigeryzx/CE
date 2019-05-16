using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CE.Helper;
using CE.Model;

namespace CE.Forms
{
    public partial class AddSingleApp : Form
    {
        #region 声明
        static XmlConfigHelper Config = XmlConfigHelper.GetInstance();
        #endregion

        public AddSingleApp()
        {
            InitializeComponent();
        }


        #region 初始化控件
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            if (IsEdit)
            {
                this.txtName.ReadOnly = true;
                App oapp = Config.GetApp(this.AppName);
                if (oapp != null)
                {
                    this.txtPath.Text = oapp.path;
                }
            }
        } 
        #endregion

        #region 属性

        /// <summary>
        /// 应用名称
        /// </summary>
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

        /// <summary>
        /// 是否编辑模式
        /// </summary>
        public bool IsEdit
        {
            get;
            set;
        }

        #endregion

        #region 控件回调函数

        private void btnFile_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnDir_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                txtPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (validation())
            {
                App app = new App();
                app.name = txtName.Text;
                app.path = txtPath.Text;
                Config.SaveApp(app);

                //是否保存后运行
                if (cbRun.Checked)
                {
                    ExecuteHelper.Execute(app);
                }
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
            string path = txtPath.Text;
            bool Result = true;
            string MessageText = string.Empty;
            if (string.IsNullOrEmpty(name))
            {
                MessageText = "名称不能为空!";
                Result = false;
            }
            if (string.IsNullOrEmpty(path))
            {
                MessageText = "路径不能为空!";
                Result = false;
            }
            if (!File.Exists(path) && !Directory.Exists(path))
            {
                MessageText = "路径指定的文件或目录不存在!";
                Result = false;
            }
            //if (Config.IsExistsApp(name))
            //{
            //    MessageText = string.Format("你指定的名称[{0}]己存在配置中，请重新指定!", name);
            //    Result = false;
            //}

            if (!Result)
                MessageBox.Show(MessageText, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            return Result;
        } 
        #endregion

        #region 显示状态改变事件
        /// <summary>
        /// 显示状态改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSingleApp_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                InitControl();
            }
        } 
        #endregion
    }
}
