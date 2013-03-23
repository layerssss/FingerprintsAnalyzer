using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FRADataStructs.DataStructs.Forms
{
    public partial class FormComplexFilter : Form
    {
        public FormComplexFilter()
        {
            InitializeComponent();
        }
        public void LoadData(ComplexFilter data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = dataIdent;
        }
        ComplexFilter data;
        GrayLevelImage originalImg;
    }
}
