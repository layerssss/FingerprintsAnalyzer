using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FRADataStructs.DataStructs.Classes;
namespace Tool_Math2Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Bitmap b;
            b = new Bitmap(100, 100);
            int i = 60;
            int j = 70;
            Graphics g = Graphics.FromImage(b);
            g.DrawLine(new Pen(Color.Red), 50, 50, j, i);
            Math2.Rotate(ref i, ref j, Math.PI * 0.1, 50, 50);
            g.DrawLine(new Pen(Color.Black), 50, 50, j, i);
            pictureBox1.Image = b;
        }
    }
}
