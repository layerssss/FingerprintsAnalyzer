using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFUnsmoothAlg1
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFUnsmoothAlg1Process(), args);
        }
    }
    class FRAProcess_OFUnsmoothAlg1Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph og;
        Integer w;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.og = inputter.GetArg<OrientationGraph>("原方向图");
            this.w = inputter.GetArg<Integer>("抚平窗口宽度的一半");
        }
        OrientationGraph nog;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("抚平后走向图", nog);
        }

        public override void Procedure()
        {
            nog = new OrientationGraph();
            nog.Allocate(og.Width, og.Height);
            int influenceWeight = (w.Value * 2 + 1) * (w.Value * 2 + 1) - 1;
            for (int i = w.Value; i < og.Height - w.Value; i++)
            {
                for (int j = w.Value; j < og.Width - w.Value; j++)
                {
                    double influence = 0;
                    for (int ii = i - w.Value; ii <= i + w.Value; ii++)
                    {
                        for (int jj = j - w.Value; jj <= j + w.Value; jj++)
                        {
                            influence += (og.Value[ii][jj] - og.Value[i][j] + Math.PI * 1.5) % Math.PI - 0.5 * Math.PI;
                        }
                    }
                    nog.Value[i][j] = (og.Value[i][j] + Math.PI - influence / influenceWeight) % Math.PI;
                }
            }
        }
    }

}
