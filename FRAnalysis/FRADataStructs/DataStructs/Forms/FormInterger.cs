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
    public partial class FormInterger : Form
    {
        public FormInterger(int i, GrayLevelImage img, string dataIdent)
        {
            InitializeComponent();
            this.I = i;
            try
            {
                textBox1.Text = "原图像高：\r\n" + img.Height.ToString() + "\r\n原图像宽:\r\n" + img.Width.ToString();
            }
            catch {
                textBox1.Text = "原图像未载入";
            }
            this.Text = "整数-" + dataIdent;
        }
        public int I;
        private void button1_Click(object sender, EventArgs e)
        {
            this.I = (int)numericUpDown1.Value;
            DialogResult = DialogResult.OK;
        }

        private void IntegerForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = this.I;
        }
    }
}
