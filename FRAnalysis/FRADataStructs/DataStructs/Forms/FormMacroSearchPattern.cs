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
    public partial class FormMacroSearchPattern : Form
    {
        public FormMacroSearchPattern()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        public string SearchPattern;
        private void button1_Click(object sender, EventArgs e)
        {
            this.SearchPattern = textBox1.Text;
            this.DialogResult = DialogResult.OK;
        }
    }
}
