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
    public partial class FormOrientationGraph : Form
    {
        public FormOrientationGraph(OrientationGraph og, GrayLevelImage img, string dataIdent)
        {
            InitializeComponent();
            this.og = og;
            this.img = img;
            this.Text += "-" + dataIdent;
        }
        OrientationGraph og;
        GrayLevelImage img;
        private void OrientationGraphForm_Load(object sender, EventArgs e)
        {
            bool isDg;
            try
            {
                DirectionGraph dg = (DirectionGraph)og;
                isDg=true;
            }
            catch
            {
                isDg=false;
            }
            this.pictureBox1.Image = new Bitmap(img.Width, img.Height);
            img.DrawImage((Bitmap)pictureBox1.Image);
            this.pictureBox4.Image = new Bitmap(og.Width, og.Height);
            GrayLevelImage ogg = new GrayLevelImage();
            ogg.Allocate(og.Width, og.Height);
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    if(isDg)
                    {
                        double thetapi = og.Value[i][j] / Math.PI / 2;
                        //if (thetapi > 1)
                        //{
                        //    thetapi = 1;
                        //}
                        ogg.Value[i][j] = thetapi;
                    }
                    else
                    {
                        double thetapi = og.Value[i][j] / Math.PI;
                        //if (thetapi > 1)
                        //{
                        //    thetapi = 1;
                        //}
                        ogg.Value[i][j] = thetapi;
                    }
                    
                }
            }
            ogg.DrawImage((Bitmap)this.pictureBox4.Image);
            trackBar1_Scroll(null, null);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Bitmap oldimg = (Bitmap)pictureBox1.Image;
            pictureBox2.Image = new Bitmap(oldimg, new Size(oldimg.Width * trackBar1.Value, oldimg.Height * trackBar1.Value));
            pictureBox3.Image = new Bitmap(oldimg.Width, oldimg.Height);
            Graphics g1 = Graphics.FromImage(pictureBox2.Image);
            Graphics g2 = Graphics.FromImage(pictureBox3.Image);
            Pen blackPen = new Pen(Color.Black);
            int w = trackBar1.Value;
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    Classes.Drawing2.DrawOrientation(g1, blackPen, new Point(j * w, i * w), w / 2, og.Value[i][j], null);
                }
            }



            for (int i = 0; i < og.Height; i+=w)
            {
                for (int j = 0; j < og.Width; j+=w)
                {
                    Classes.Drawing2.DrawOrientation(g2, blackPen, new Point(j, i), w / 2, og.Value[i][j], null);
                }
            }
        }
    }
}
