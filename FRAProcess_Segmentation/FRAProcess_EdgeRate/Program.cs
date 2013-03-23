using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_EdgeRate
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_EdgeRateProcess(), args);
        }
    }
    class FRAProcess_EdgeRateProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        Integer edgeConnectivityT;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("二值图");
            this.edgeConnectivityT = inputter.GetArg<Integer>("边缘连通度阀值");
        }
        BinaryGraph Edge;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("突出的边缘", Edge);
        }

        public override void Procedure()
        {
            Edge = new BinaryGraph();
            Edge.Allocate(BG, 0);
            this.BG.FindAny((ti, tj, tv) =>
            {
                int sumConnectivity = 8;
                if (tv == 1)
                {
                    return false;
                }
                if (ti == 0 || ti == BG.Height - 1 || tj == 0 || tj == BG.Width - 1)
                {
                    return false;
                }
                for (int k = 0; k < 8; k++)
                {
                    sumConnectivity -= this.BG.Value[ti + Drawing2.EightConnectionAreaI[k]][tj + Drawing2.EightConnectionAreaJ[k]];
                }
                if (sumConnectivity < this.edgeConnectivityT.Value)
                {
                    this.Edge.Value[ti][tj] = 1;
                }
                return false;
            });
        }
    }

}
