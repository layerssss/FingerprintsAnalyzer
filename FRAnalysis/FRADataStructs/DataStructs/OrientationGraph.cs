using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class OrientationGraph:Abstract.DoubleFloatGraph
    {

        public override bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormOrientationGraph ogf = new Controls.FormOrientationGraph(this, originalImg, dataIdent);
            ogf.ShowDialog();
            return false;
        }
    }
}
