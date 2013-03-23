﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FRADataStructs.DataStructs.Controls
{
    public partial class FormDoubleFloat : Form
    {
        public FormDoubleFloat(double i, GrayLevelImage img, string dataIdent)
        {
            InitializeComponent();
            this.I = i;
            this.I = i;
            try
            {
                textBox1.Text = "原图像高：\r\n" + img.Height.ToString() + "\r\n原图像宽:\r\n" + img.Width.ToString();
            }
            catch
            {
                textBox1.Text = "原图像未载入";
            }
            this.Text = "浮点数-" + dataIdent;
        }
        public double I;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                this.I = double.Parse(this.textBox2.Text);
                DialogResult = DialogResult.OK;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "无效的值", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void IntegerForm_Load(object sender, EventArgs e)
        {
            textBox2.Text = this.I.ToString();
        }
    }
}
