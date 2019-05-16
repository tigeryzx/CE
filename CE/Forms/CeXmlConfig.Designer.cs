namespace CE.Forms
{
    partial class CeXmlConfig
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
            this.btnRepair = new System.Windows.Forms.Button();
            this.cbWhenNotFoundShowTip = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudFindAppSimilarityPercent = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetRunTipAutoRun = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtShortcutkey = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbCheckUpdate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudFindAppSimilarityPercent)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRepair
            // 
            this.btnRepair.Location = new System.Drawing.Point(6, 20);
            this.btnRepair.Name = "btnRepair";
            this.btnRepair.Size = new System.Drawing.Size(174, 23);
            this.btnRepair.TabIndex = 0;
            this.btnRepair.Text = "程序不能运行时点击我修复";
            this.btnRepair.UseVisualStyleBackColor = true;
            this.btnRepair.Click += new System.EventHandler(this.btnRepair_Click);
            // 
            // cbWhenNotFoundShowTip
            // 
            this.cbWhenNotFoundShowTip.AutoSize = true;
            this.cbWhenNotFoundShowTip.Location = new System.Drawing.Point(8, 49);
            this.cbWhenNotFoundShowTip.Name = "cbWhenNotFoundShowTip";
            this.cbWhenNotFoundShowTip.Size = new System.Drawing.Size(204, 16);
            this.cbWhenNotFoundShowTip.TabIndex = 1;
            this.cbWhenNotFoundShowTip.Text = "当找不指定的程序时弹出相似提示";
            this.cbWhenNotFoundShowTip.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "找不到程序时推荐的相似程度(最大1)";
            // 
            // nudFindAppSimilarityPercent
            // 
            this.nudFindAppSimilarityPercent.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFindAppSimilarityPercent.Location = new System.Drawing.Point(207, 70);
            this.nudFindAppSimilarityPercent.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudFindAppSimilarityPercent.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFindAppSimilarityPercent.Name = "nudFindAppSimilarityPercent";
            this.nudFindAppSimilarityPercent.Size = new System.Drawing.Size(52, 21);
            this.nudFindAppSimilarityPercent.TabIndex = 3;
            this.nudFindAppSimilarityPercent.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbCheckUpdate);
            this.groupBox1.Controls.Add(this.btnSetRunTipAutoRun);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtShortcutkey);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnRepair);
            this.groupBox1.Controls.Add(this.nudFindAppSimilarityPercent);
            this.groupBox1.Controls.Add(this.cbWhenNotFoundShowTip);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 131);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "程序运行配置";
            // 
            // btnSetRunTipAutoRun
            // 
            this.btnSetRunTipAutoRun.Location = new System.Drawing.Point(186, 20);
            this.btnSetRunTipAutoRun.Name = "btnSetRunTipAutoRun";
            this.btnSetRunTipAutoRun.Size = new System.Drawing.Size(137, 23);
            this.btnSetRunTipAutoRun.TabIndex = 8;
            this.btnSetRunTipAutoRun.Text = "设置开机提示程序启动";
            this.btnSetRunTipAutoRun.UseVisualStyleBackColor = true;
            this.btnSetRunTipAutoRun.Click += new System.EventHandler(this.btnSetRunTipAutoRun_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(201, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "(修改后需重启程序)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "快捷键：Lwin+";
            // 
            // txtShortcutkey
            // 
            this.txtShortcutkey.Location = new System.Drawing.Point(98, 98);
            this.txtShortcutkey.Name = "txtShortcutkey";
            this.txtShortcutkey.ReadOnly = true;
            this.txtShortcutkey.Size = new System.Drawing.Size(100, 21);
            this.txtShortcutkey.TabIndex = 2;
            this.txtShortcutkey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(353, 98);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存配置";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClearAll);
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Location = new System.Drawing.Point(12, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(428, 113);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "App设置";
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(6, 49);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(191, 23);
            this.btnClearAll.TabIndex = 1;
            this.btnClearAll.Text = "清除当前所有App的连接配置";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 20);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(191, 23);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "清除己失效的App快速启动连接";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbCheckUpdate
            // 
            this.cbCheckUpdate.AutoSize = true;
            this.cbCheckUpdate.Location = new System.Drawing.Point(218, 48);
            this.cbCheckUpdate.Name = "cbCheckUpdate";
            this.cbCheckUpdate.Size = new System.Drawing.Size(96, 16);
            this.cbCheckUpdate.TabIndex = 9;
            this.cbCheckUpdate.Text = "自动检查更新";
            this.cbCheckUpdate.UseVisualStyleBackColor = true;
            // 
            // CeXmlConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 274);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CeXmlConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ce程序设置";
            ((System.ComponentModel.ISupportInitialize)(this.nudFindAppSimilarityPercent)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRepair;
        private System.Windows.Forms.CheckBox cbWhenNotFoundShowTip;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudFindAppSimilarityPercent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.TextBox txtShortcutkey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSetRunTipAutoRun;
        private System.Windows.Forms.CheckBox cbCheckUpdate;
    }
}