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
    public partial class FormWindow : Form
    {
        public FormWindow()
        {
            InitializeComponent();
        }
        int iMin;
        int iMax;
        int jMin;
        int jMax;
        bool moving = false;
        void showstatus()
        {
            toolStripStatusLabel1.Text = string.Format("i:{0} j:{1} {2}*{3}",iMin,jMin, iMax - iMin, jMax - jMin);
            Bitmap b = (Bitmap)pictureBox1.Image;
            pictureBox1.Image = null;
            img.DrawImage(b);
            g.DrawRectangle(new Pen(Color.Black), jMin, iMin,jMax-jMin, iMax-iMin);
            pictureBox1.Image = b;
        }
        public void LoadData(Window w,GrayLevelImage img,string dataIdent)
        {
            this.window = w;
            this.iMin = w.IMin;
            this.iMax = w.IMax;
            this.jMin = w.JMin;
            this.jMax = w.JMax;
            this.img = img;
            this.Text = "窗口-" + dataIdent;
            pictureBox1.Image = new Bitmap(img.Width, img.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            showstatus();
        }
        Graphics g;
        Window window;
        GrayLevelImage img;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            iMin = e.Y;
            jMin = e.X;
            showstatus();
        }
        
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                iMax = e.Y;
                jMax = e.X;
                showstatus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.window.IMin = this.iMin;
            this.window.IMax = this.iMax;
            this.window.JMin = this.jMin;
            this.window.JMax = this.jMax;
            
            this.DialogResult = DialogResult.OK;
        }

    }
}
