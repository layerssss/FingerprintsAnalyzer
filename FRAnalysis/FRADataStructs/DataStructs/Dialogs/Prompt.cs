using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FRADataStructs.DataStructs.Dialogs
{
    public partial class Prompt : Form
    {
        public static DialogResult Show(string title, string label, string def,out string result)
        {
            var f = new Prompt(title, label, def);
            f.ShowDialog();
            result = f.Result;
            return f.DialogResult;
        }
        public Prompt(string title,string label,string def)
        {
            InitializeComponent();
            this.Text = title;
            this.label1.Text = label;
            this.textBox1.Text = def;
        }
        public string Result
        {
            set
            {
                textBox1.Text = value;
            }
            get
            {
                return textBox1.Text;
            }
        }
        private void FormPrompt_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
