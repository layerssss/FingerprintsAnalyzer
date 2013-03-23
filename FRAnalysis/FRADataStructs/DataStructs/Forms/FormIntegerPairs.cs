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
    public partial class FormIntegerPairs : Form
    {
        public FormIntegerPairs()
        {
            InitializeComponent();
        }
        public void LoadData(IntegerPairs data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "" + dataIdent;
            this.pictureBox1_SizeChanged(null, null);
        }
        IntegerPairs data;
        GrayLevelImage originalImg;
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.data.Draw(img);
            pictureBox1.Image = img;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int a,b;
            this.data.GetData(e.X,e.Y,out a,out b,this.pictureBox1.Image);
            this.toolStripStatusLabel1.Text = string.Format("预估该点数据：A={0},B={1}",
                a,
                b);
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "";
        }
    }
}
