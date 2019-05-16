namespace CE.Forms
{
    partial class AddMultiApp
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
            this.Label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.clbApp = new System.Windows.Forms.CheckedListBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbRun = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(41, 12);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "名称：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(59, 6);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 21);
            this.txtName.TabIndex = 1;
            // 
            // clbApp
            // 
            this.clbApp.FormattingEnabled = true;
            this.clbApp.Location = new System.Drawing.Point(59, 33);
            this.clbApp.Name = "clbApp";
            this.clbApp.Size = new System.Drawing.Size(281, 100);
            this.clbApp.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(265, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbRun
            // 
            this.cbRun.AutoSize = true;
            this.cbRun.Location = new System.Drawing.Point(59, 139);
            this.cbRun.Name = "cbRun";
            this.cbRun.Size = new System.Drawing.Size(84, 16);
            this.cbRun.TabIndex = 4;
            this.cbRun.Text = "保存后运行";
            this.cbRun.UseVisualStyleBackColor = true;
            // 
            // AddMultiApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(352, 158);
            this.Controls.Add(this.cbRun);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.clbApp);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.Label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AddMultiApp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "添加含子项的应用";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckedListBox clbApp;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbRun;
    }
}