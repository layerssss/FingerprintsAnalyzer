using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CoarseZP
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CoarseZPProcess(), args);
        }
    }
    class FRAProcess_CoarseZPProcess : FRAProcess.FRAProcess
    {
        PointLocationSet cores;
        PointLocationSet deltas;
        DoubleFloat thetaS;
        OrientationGraph og;
        Integer w;
        Integer h;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.cores = inputter.GetArg<PointLocationSet>("CORES");
            this.deltas = inputter.GetArg<PointLocationSet>("DELTAS");
            this.thetaS = inputter.GetArg<DoubleFloat>("ThetaS");
            this.w = inputter.GetArg<Integer>("W");
            this.h = inputter.GetArg<Integer>("H");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("重建结果", this.og);
        }

        public override void Procedure(){
            this.og = new OrientationGraph();
            this.og.Allocate(w.Value, h.Value);
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    double sumarg = 0;;
                    foreach (PointLocation core in this.cores)
                    {
                        sumarg += Math2.Arg(j - core.J, core.I - i);
                    }
                    foreach (PointLocation delta in this.deltas)
                    {
                        sumarg -= Math2.Arg(j - delta.J, delta.I - i);
                    }
                    this.og.Value[i][j] = this.thetaS.Value + 0.5 * sumarg;
                }
            }
        }
    }

}
