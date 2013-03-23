using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class SegmentationArea : IntergerGraph, IDataStruct
    {
        public SegmentationArea() { }
        public SegmentationArea(Abstract.Graph<int> graph)
        {
            this.Width = graph.Width;
            this.Height = graph.Height;
            this.Value = graph.Value;
        }
        public bool IsInArea(int i, int j)
        {
            if (!this.IsInbound(i,j))
            {
                return false;
            }
            return this.Value[i][j] == 1;
        }
        #region IDataStruct 成员

        public new bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormIntergerGraph f = new Controls.FormIntergerGraph();
            f.LoadData(this, originalImg, dataIdent);
            f.AddCheckBox(0, System.Drawing.Color.Blue);
            f.AddCheckBox(1, System.Drawing.Color.Red);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        #endregion
    }
}
