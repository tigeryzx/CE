using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace CE.Forms
{
    public partial class UpdateLogPlan : Form
    {
        public UpdateLogPlan()
        {
            InitializeComponent();

            Assembly asm = Assembly.GetExecutingAssembly();
            Stream sm = asm.GetManifestResourceStream("CE.Resource.UpdateLog.txt");

            StreamReader reader = new StreamReader(sm);
            string text = reader.ReadToEnd();

            this.textBox1.Text = text;
            this.textBox1.Select(0, 0); 
        }
    }
}
