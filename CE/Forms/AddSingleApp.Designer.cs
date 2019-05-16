namespace CE.Forms
{
    partial class AddSingleApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnFile = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.cbRun = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(59, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(263, 21);
            this.txtName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "路径：";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(59, 35);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(159, 21);
            this.txtPath.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(224, 35);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(46, 23);
            this.btnFile.TabIndex = 4;
            this.btnFile.Text = "文件";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(104, 62);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(218, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDir
            // 
            this.btnDir.Location = new System.Drawing.Point(276, 35);
            this.btnDir.Name = "btnDir";
            this.btnDir.Size = new System.Drawing.Size(46, 23);
            this.btnDir.TabIndex = 6;
            this.btnDir.Text = "目录";
            this.btnDir.UseVisualStyleBackColor = true;
            this.btnDir.Click += new System.EventHandler(this.btnDir_Click);
            // 
            // cbRun
            // 
            this.cbRun.AutoSize = true;
            this.cbRun.Checked = true;
            this.cbRun.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRun.Location = new System.Drawing.Point(14, 66);
            this.cbRun.Name = "cbRun";
            this.cbRun.Size = new System.Drawing.Size(84, 16);
            this.cbRun.TabIndex = 7;
            this.cbRun.Text = "保存后运行";
            this.cbRun.UseVisualStyleBackColor = true;
            // 
            // AddSingleApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 101);
            this.Controls.Add(this.cbRun);
            this.Controls.Add(this.btnDir);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnFile);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddSingleApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加单项应用";
            this.VisibleChanged += new System.EventHandler(this.AddSingleApp_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.CheckBox cbRun;
    }
}