using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace FRADataStructs.DataStructs.Controls
{
    [DefaultEvent("Clicked")]
    public partial class ControlDrawable : UserControl
    {
        public ControlDrawable()
        {
            InitializeComponent();
        }
        bool initialed = false;
        GrayLevelImage img;
        IDrawable drawable;
        [Browsable(true)]
        [Category("行为")]
        [Description("当用户单击控件时发生")]
        public event DrawableControlClickHandler Clicked;
        public void Initial(IDrawable drawable, GrayLevelImage originalImg)
        {
            this.drawable = drawable;
            this.img = originalImg;
            this.initialed = true;
            this.ControlDrawable_Resize(null, null);
        }
        private void ControlDrawable_Resize(object sender, EventArgs e)
        {
            if (this.initialed)
            {
                var bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
                this.drawable.Draw(bmp,this.Font);
                this.pictureBox1.Image = bmp;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.initialed)
            {
                this.toolStripStatusLabel1.Text = this.drawable.Reverse(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Clicked(this, e);
        }

    }
    public delegate void DrawableControlClickHandler(object sender,MouseEventArgs e);
}
