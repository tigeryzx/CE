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
    public partial class ShowAppTip : Form
    {
        public ShowAppTip()
        {
            InitializeComponent();
        }

        #region 初始化控件
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            lbTip.Text = this.TipText;
            //推荐框获取焦点
            lvApps.Focus();
            foreach (var app in this.AppList)
            {
                var item = new ListViewItem(string.Format("{0}", app.name));
                item.Tag = app;
                lvApps.Items.Add(item);
            }
            //默认选中第一项
            if (lvApps.Items.Count > 0)
                lvApps.Items[0].Selected = true;

        }

        protected override void OnLoad(EventArgs e)
        {
            InitControl();
            base.OnLoad(e);
        }

        #endregion

        #region 控件回调函数

        private void lvApps_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var list = lvApps.SelectedItems;
                if (list != null && list.Count > 0)
                {
                    App app = list[0].Tag as App;
                    if (app != null)
                    {
                        ExecuteHelper.Execute(app);
                        Application.Exit();
                    }
                }
            }
        }

        private void lbAddSingle_Click(object sender, EventArgs e)
        {
            AddSingleApp f = new AddSingleApp();
            f.AppName = AppName;
            DialogResult result = f.ShowDialog();
            if(result==DialogResult.OK)
                this.Close();
        }

        private void lbAddMulit_Click(object sender, EventArgs e)
        {
            AddMultiApp f = new AddMultiApp();
            f.AppName = AppName;
            DialogResult result =  f.ShowDialog();
            if (result == DialogResult.OK)
                this.Close();
        }

        #endregion

        #region 属性

        private string TipFormat = "找不到程序[{0}]，但相似度为{1}%的有：";
        /// <summary>
        /// 提示信息
        /// </summary>
        public string TipText
        {
            get
            {
                return string.Format(TipFormat, AppName, Similarity*100);
            }
            set
            {
                lbTip.Text = value;
            }
        }

        /// <summary>
        /// 相似度
        /// </summary>
        public decimal Similarity
        {
            get;
            set;
        }

        /// <summary>
        /// 应用用名称
        /// </summary>
        public string AppName
        {
            get;
            set;
        }

        /// <summary>
        /// 找到的相似应用程序
        /// </summary>
        public List<App> AppList
        {
            get;
            set;
        }
        #endregion

    }
}
