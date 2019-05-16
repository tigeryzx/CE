namespace CE.Forms
{
    partial class ShowAppTip
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbTip = new System.Windows.Forms.Label();
            this.lvApps = new System.Windows.Forms.ListView();
            this.lbAddSingle = new System.Windows.Forms.LinkLabel();
            this.lbAddMulit = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lbTip
            // 
            this.lbTip.AutoSize = true;
            this.lbTip.Location = new System.Drawing.Point(12, 9);
            this.lbTip.Name = "lbTip";
            this.lbTip.Size = new System.Drawing.Size(35, 12);
            this.lbTip.TabIndex = 0;
            this.lbTip.Text = "lbTip";
            // 
            // lvApps
            // 
            this.lvApps.Location = new System.Drawing.Point(12, 24);
            this.lvApps.Name = "lvApps";
            this.lvApps.Size = new System.Drawing.Size(301, 97);
            this.lvApps.TabIndex = 1;
            this.lvApps.UseCompatibleStateImageBehavior = false;
            this.lvApps.View = System.Windows.Forms.View.List;
            this.lvApps.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvApps_KeyDown);
            // 
            // lbAddSingle
            // 
            this.lbAddSingle.AutoSize = true;
            this.lbAddSingle.Location = new System.Drawing.Point(12, 124);
            this.lbAddSingle.Name = "lbAddSingle";
            this.lbAddSingle.Size = new System.Drawing.Size(77, 12);
            this.lbAddSingle.TabIndex = 2;
            this.lbAddSingle.TabStop = true;
            this.lbAddSingle.Text = "添加单启动项";
            this.lbAddSingle.Click += new System.EventHandler(this.lbAddSingle_Click);
            // 
            // lbAddMulit
            // 
            this.lbAddMulit.AutoSize = true;
            this.lbAddMulit.Location = new System.Drawing.Point(95, 124);
            this.lbAddMulit.Name = "lbAddMulit";
            this.lbAddMulit.Size = new System.Drawing.Size(89, 12);
            this.lbAddMulit.TabIndex = 3;
            this.lbAddMulit.TabStop = true;
            this.lbAddMulit.Text = "添加现在关联项";
            this.lbAddMulit.Click += new System.EventHandler(this.lbAddMulit_Click);
            // 
            // ShowAppTip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 144);
            this.Controls.Add(this.lbAddMulit);
            this.Controls.Add(this.lbAddSingle);
            this.Controls.Add(this.lvApps);
            this.Controls.Add(this.lbTip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ShowAppTip";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "显示相似程序";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTip;
        private System.Windows.Forms.ListView lvApps;
        private System.Windows.Forms.LinkLabel lbAddSingle;
        private System.Windows.Forms.LinkLabel lbAddMulit;
    }
}