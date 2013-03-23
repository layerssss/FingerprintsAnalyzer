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
    public partial class FormDoubleFloatArray : Form
    {
        public FormDoubleFloatArray()
        {
            InitializeComponent();
        }
        DoubleFloatArray arr;
        public void LoadData(DoubleFloatArray arr, string dataIdent)
        {
            this.Text = "数组-"+dataIdent;
            this.arr = arr;
            this.numericUpDown1.Minimum = this.numericUpDown2.Minimum = 0;
            this.numericUpDown1.Maximum = this.numericUpDown2.Maximum = this.arr.Count - 1;
            FormDoubleFloatArray_SizeChanged(null, null);
        }
        private void FormDoubleFloatArray_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            arr.DrawFigure(pictureBox1.Image, new Pen(Color.Red, 3),checkBox1.Checked);
        }

        private void FormDoubleFloatArray_Load(object sender, EventArgs e)
        {
            FormDoubleFloatArray_SizeChanged(null, null);
        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            for (decimal i = numericUpDown1.Value; i <= numericUpDown2.Value; i++)
            {
                textBox1.Text +=  i + "," + arr[(int)i] + "\r\n";
            }
        }
    }
}
