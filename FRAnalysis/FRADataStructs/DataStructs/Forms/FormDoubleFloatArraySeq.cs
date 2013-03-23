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
    public partial class FormDoubleFloatArraySeq : Form
    {
        public FormDoubleFloatArraySeq()
        {
            InitializeComponent();
        }
        public void LoadData(DoubleFloatArraySeq data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "数组序列-" + dataIdent;
            double maxValue;
            double minValue;
            int maxArrLen;
            b = data.Draw(out maxValue, out minValue, out maxArrLen);
            pictureBox1.Image = b;
            checkBox1.Text += string.Format(" 序列信息：seqLen={0},maxArrLen{1},minValue{2},maxValue{3};", this.data.Count, maxArrLen, minValue, maxValue);
        }
        DoubleFloatArraySeq data;
        GrayLevelImage originalImg;
        Bitmap b;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.pictureBox1.SizeMode = this.checkBox1.Checked ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap b2=new Bitmap(b);
            Classes.Drawing2.DrawPoint(Graphics.FromImage(b2), new Pen(Brushes.Yellow),
                int.Parse(textBox1.Text),
                int.Parse(textBox2.Text),
                0, 0, b.Height);
            pictureBox1.Image = b2;
        }

    }
}
