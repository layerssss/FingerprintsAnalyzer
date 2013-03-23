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
    public partial class FormStabSet : Form
    {
        public FormStabSet()
        {
            InitializeComponent();
        }
        public void LoadData(StabSet data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "针刺集-" + dataIdent;
            trackBar1.Minimum = 0;
            trackBar1.Maximum = this.data.Count - 1;
            trackBar1_Scroll(null, null);
        }
        StabSet data;
        GrayLevelImage originalImg;

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int maxlen = data.Max<PointLocationSet>(tpls => tpls.Max<PointLocation>(tpl => Math.Max(Math.Abs(tpl.I), Math.Abs(tpl.J))));
            Bitmap b = new Bitmap(maxlen * 2 + 1, maxlen * 2 + 1);
            foreach (PointLocation pl in this.data[trackBar1.Value])
            {
                b.SetPixel(maxlen + pl.J, maxlen + pl.I, Color.Red);
            }
            pictureBox1.Image = b;
        }
    }
}
