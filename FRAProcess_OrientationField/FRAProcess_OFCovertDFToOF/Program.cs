using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFCovertDFToOF
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFCovertDFToOFProcess(), args);
        }
    }
    class FRAProcess_OFCovertDFToOFProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DirectionGraph dg;
        DoubleFloat nPI;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.dg = inputter.GetArg<DirectionGraph>("方向图");
            this.nPI = inputter.GetArg<DoubleFloat>("所增加的PI的倍数");
        }
        OrientationGraph og;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("走向图", og);
        }

        public override void Procedure()
        {
            og = new DirectionGraph();
            og.Allocate(dg.Width, dg.Height);
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    og.Value[i][j] = (dg.Value[i][j] + nPI.Value * Math.PI) % (Math.PI);
                }
            }
        }
    }

}
