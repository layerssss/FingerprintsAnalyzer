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
    public partial class FormDataStuct : Form
    {
        public FormDataStuct()
        {
            InitializeComponent();
        }
        public void LoadData(DataStruct data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "-" + dataIdent;
        }
        DataStruct data;
        GrayLevelImage originalImg;
    }
}
