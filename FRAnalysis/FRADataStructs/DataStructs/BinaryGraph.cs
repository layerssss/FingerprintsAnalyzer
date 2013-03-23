using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class BinaryGraph:IntergerGraph
    {
        public BinaryGraph() { }
        public BinaryGraph(Abstract.Graph<int> ig)
        {
            this.Width = ig.Width;
            this.Height = ig.Height;
            this.Value = ig.Value;
        }
        public new BinaryGraph Clone()
        {
            return new BinaryGraph(base.Clone());
        }
        public override bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormIntergerGraph f = new Controls.FormIntergerGraph();
            f.LoadData(this, originalImg, dataIdent);
            f.Text = "二值图-" + dataIdent;
            f.AddCheckBox(0, System.Drawing.Color.Blue);
            f.AddCheckBox(1, System.Drawing.Color.Yellow);
            f.ShowDialog();
            
            return false;
        }
        
    }
}
