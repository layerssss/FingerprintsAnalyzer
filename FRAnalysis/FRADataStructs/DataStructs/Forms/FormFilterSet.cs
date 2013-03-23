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
    public partial class FormFilterSet : Form
    {
        public FormFilterSet()
        {
            InitializeComponent();
        }
        public void LoadData(FilterSet data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "-" + dataIdent;
        }
        FilterSet data;
        GrayLevelImage originalImg;
    }
}
