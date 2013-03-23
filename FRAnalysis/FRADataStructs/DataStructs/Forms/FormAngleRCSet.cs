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
    public partial class FormAngleRCSet : Form
    {
        public FormAngleRCSet()
        {
            InitializeComponent();
        }
        public void LoadData(AngleRCSet data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "" + dataIdent;
            pictureBox1_SizeChanged(null, null);
        }
        AngleRCSet data;
        GrayLevelImage originalImg;

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.data.Draw(img);
            this.pictureBox1.Image = img;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            this.toolStripStatusLabel1.Text = this.data.DrawingInfo.Reverse(e.Location);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "";
        }
    }
}
