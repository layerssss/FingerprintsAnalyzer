using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFSmoothAVG
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFSmoothAVGProcess(), args);
        }
    }
    class FRAProcess_OFSmoothAVGProcess : FRAProcess.FRAProcess
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
            double wDelta=1/((2*w.Value+1)*(2*w.Value+1));
            double w1;
            nog.Allocate(og.Width, og.Height);
            for (int i = w.Value; i < og.Height - w.Value; i++)
            {
                for (int j = w.Value; j < og.Width - w.Value; j++)
                {
                    w1 = 0;
                    double avg = 0;
                    for (int ii = i - w.Value; ii <= i + w.Value; ii++)
                    {
                        for (int jj = j - w.Value; jj <= j + w.Value; jj++)
                        {
                            avg = FRADataStructs.DataStructs.Classes.Math2.MidOF(
                                w1, avg, 1 - w1, og.Value[ii][jj]);
                        }
                    }
                    nog.Value[i][j] = avg;
                }
            }
        }
    }

}
