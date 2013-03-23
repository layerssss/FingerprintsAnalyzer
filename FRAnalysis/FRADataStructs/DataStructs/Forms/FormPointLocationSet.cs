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
    public partial class FormPointLocationSet : Form
    {
        public FormPointLocationSet()
        {
            InitializeComponent();
        }
        public void LoadData(PointLocationSet data, string dataIdent, GrayLevelImage img)
        {
            pictureBox1.Image = new Bitmap(img.Width, img.Height);
            img.DrawImage((Bitmap)pictureBox1.Image);
            this.Text = "点坐标集-" + dataIdent;
            foreach (PointLocation p in data)
            {
                p.Draw(pictureBox1.Image, Color.Red);
            }
        }
        private void FormPointLocationSet_Load(object sender, EventArgs e)
        {

        }
    }
}
