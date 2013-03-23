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
    public partial class FormPointLocation : Form
    {
        public FormPointLocation()
        {
            InitializeComponent();
        }
        public void LoadData(PointLocation data, string dataIdent, GrayLevelImage oimg)
        {
            this.Text = "点坐标-"+dataIdent;
            this.data = data;
            this.img = oimg;
            this.pictureBox1.Image = new Bitmap(oimg.Width, oimg.Height);
            this.textBox1.Text = data.I.ToString();
            this.textBox2.Text = data.J.ToString() ;
            
        }
        GrayLevelImage img;
        PointLocation data;
        private void button1_Click(object sender, EventArgs e)
        {
            data.I = int.Parse(textBox1.Text);
            data.J = int.Parse(textBox2.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.img.DrawImage((Bitmap)pictureBox1.Image);
                PointLocation pl = new PointLocation() { I = int.Parse(textBox1.Text), J = int.Parse(textBox2.Text) };
                Image tmp = this.pictureBox1.Image;
                this.pictureBox1.Image = null;
                this.pictureBox1.Image = tmp;
                pl.Draw(this.pictureBox1.Image,Color.Red);
            }
            catch { }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.Y.ToString();
            textBox2.Text = e.X.ToString();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }
    }
}

