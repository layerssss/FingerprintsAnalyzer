using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_OFCovertToDF
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFCovertToDFProcess(), args);
        }
    }
    class FRAProcess_OFCovertToDFProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph og;
        DoubleFloat nPI;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.og = inputter.GetArg<OrientationGraph>("走向图");
            this.nPI = inputter.GetArg<DoubleFloat>("所增加的PI的倍数");
        }
        DirectionGraph dg;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("方向图", dg);
        }

        public override void Procedure()
        {
            dg = new DirectionGraph();
            dg.Allocate(og.Width, og.Height);
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    dg.Value[i][j] = (og.Value[i][j] + nPI.Value * Math.PI) % (2 * Math.PI);
                }
            }
        }
    }

}
