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
    public partial class FormWeightFunction : Form
    {
        public FormWeightFunction()
        {
            InitializeComponent();
        }
        public void LoadData(WeightFunction data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "-" + dataIdent;
            this.textBox1.Text = data.a.ToString();
            this.textBox2.Text = data.b.ToString();
        }
        WeightFunction data;
        GrayLevelImage originalImg;

        private void button1_Click(object sender, EventArgs e)
        {
            this.data.a = double.Parse(textBox1.Text);
            this.data.b = double.Parse(textBox2.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            WeightFunction w = new WeightFunction();
            w.a = double.Parse(textBox1.Text);
            w.b = double.Parse(textBox2.Text);
            DoubleFloatArray arr = new DoubleFloatArray();
            for (double i = 0; i < 50; i += 0.5)
            {
                arr.Add(w.GetW(i));
            }
            Bitmap b=new Bitmap(pictureBox1.Width,pictureBox1.Height);
            arr.DrawFigure(b, new Pen(Color.Red, 1), 0, arr.Max(), checkBox1.Checked);
            this.button3.Text = "预览(d=1~50,wMax=" + arr.Max() + ")";
            pictureBox1.Image = b;
        }
    }
}
