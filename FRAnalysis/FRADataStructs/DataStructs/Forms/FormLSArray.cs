using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FRADataStructs.DataStructs.Controls
{
    public partial class FormLSArray : Form
    {
        public FormLSArray()
        {
            InitializeComponent();
        }
        public void LoadData(LSArray data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = dataIdent;
            this.controlDrawable1.Initial(this.data, this.originalImg);
        }
        
        LSArray data;
        GrayLevelImage originalImg;

        private void FormLSArray_Load(object sender, EventArgs e)
        {

        }

        private void controlDrawable1_Clicked(object sender, MouseEventArgs e)
        {
            var i = (int)(e.Y / this.data.DrawInfo.hStep);
            var j = (int)(e.X / this.data.DrawInfo.wStep);
            this.data.Value[i][j].Present(this.originalImg, string.Format("矩阵中I{0}J{1}处元素", i, j));
        }

    }
}