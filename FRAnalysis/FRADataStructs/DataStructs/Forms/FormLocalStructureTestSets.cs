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
    public partial class FormLocalStructureTestSets : Form
    {
        public FormLocalStructureTestSets()
        {
            InitializeComponent();
        }
        public void LoadData(LocalStructureTestSets data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "" + dataIdent;
            this.refresh();
        }
        LocalStructureTestSets data;
        GrayLevelImage originalImg;
        void refresh()
        {
            this.listBox1.Items.Clear();
            this.listBox1.Items.AddRange(this.data.ToArray());
        }
        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.data.Add(new LocalStructureTest());
            this.refresh();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.data.RemoveAll(tlst => this.listBox1.SelectedItems.Contains(tlst));
            this.refresh();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedItems.Count == 1)
            {
                FormLocalStructureTest.Show("局部结构测试对", this.listBox1.SelectedItems[0] as LocalStructureTest);
            }
        }

        private void 随机匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormLocalStructureTest.Show("随机匹配对", this.data.GetRandomMatched());
            }
            catch { }
        }

        private void 随机不匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FormLocalStructureTest.Show("随机不匹配对", this.data.GetRandomUnmatched());
            }
            catch { }
        }

    }
}
