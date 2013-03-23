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
    public partial class FormGrayScaleImage :Form
    {
        public FormGrayScaleImage(GrayLevelImage orgimg, GrayLevelImage img, string dataIdent)
        {
            InitializeComponent();
            try
            {
                this.pictureBox1.Image = new Bitmap(orgimg.Width, orgimg.Height);
                orgimg.DrawImage((Bitmap)pictureBox1.Image);
            }
            catch { }

            this.pictureBox2.Image = new Bitmap(img.Width, img.Height);
            img.DrawImage((Bitmap)pictureBox2.Image);
            this.Text += "-" + dataIdent;
        }

        private void GrayScaleImageForm_Load(object sender, EventArgs e)
        {

        }

        private void bMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.pictureBox2.Image.Save("temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
            System.Diagnostics.Process.Start("temp.bmp");
        }
    }
}
