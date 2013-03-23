using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace FRADataStructs.DataStructs
{
    public class PoincareIndexes:IntergerGraph
    {
        public override bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormIntergerGraph f = new Controls.FormIntergerGraph();
            f.LoadData(this, originalImg, dataIdent);

            f.AddCheckBox(-2, Color.Blue);
            f.AddCheckBox(-1, Color.Green);
            f.AddCheckBox(0, Color.Yellow);
            f.AddCheckBox(1, Color.Violet);
            f.AddCheckBox(2, Color.Red);
            f.AddCheckBox(-3, Color.Gray);
            f.Text = "庞加莱指数-" + dataIdent;
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }
    }
}
