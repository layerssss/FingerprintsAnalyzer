using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FRADataStructs.DataStructs.Controls
{
    public partial class FormStringData : Form
    {
        public FormStringData()
        {
            InitializeComponent();
        }
        public void LoadData(StringData data,string dataIdent)
        {
            textBox1.Text = data.Value;
            this.data = data;
            this.Text = "字符串-" + dataIdent;
        }
        StringData data;
        private void 选择一个文件路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text += openFileDialog1.FileName;
            }
        }

        private void 选择一个目录路径ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text += folderBrowserDialog1.SelectedPath;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.data.Value = textBox1.Text;
            }
            catch { }
        }

        private void FormStringData_Load(object sender, EventArgs e)
        {

        }
    }
}
