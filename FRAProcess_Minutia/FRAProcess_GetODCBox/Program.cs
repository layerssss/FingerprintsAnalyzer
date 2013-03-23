using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetODCBox
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetODCBoxProcess(), args);
        }
    }
    class FRAProcess_GetODCBoxProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloat deltaTheta;
        Integer W;
        Integer H;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.deltaTheta = inputter.GetArg<DoubleFloat>("Delta增量");
            this.W = inputter.GetArg<Integer>("W");
            this.H = inputter.GetArg<Integer>("H");
        }
        StabSet ODCBoxes;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("ODC盒子", ODCBoxes);
        }

        public override void Procedure()
        {
            this.ODCBoxes = new StabSet();
            List<PointLocationSet> lpls = new List<PointLocationSet>();
            double theta = 0;
            for (; theta < 0.5*Math.PI; theta += deltaTheta.Value)
            {
                PointLocationSet pls = new PointLocationSet();
                double cos = Math.Cos(theta);
                double sin = Math.Sin(theta);
                for (int x = -H.Value; x <= H.Value; x++)
                {
                    for (int y = -H.Value; y <= H.Value; y++)
                    {
                        double a = cos * y - sin * x;
                        double b = sin * y + cos * x;
                        if (a <- W.Value || a > 0)
                        {
                            continue;
                        }
                        if (b < -H.Value || b > 0)
                        {
                            continue;
                        }
                        pls.Add(new PointLocation(-y, x));
                    }
                }
                lpls.Add(pls);
            }
            for (; theta <  Math.PI; theta += deltaTheta.Value)
            {
                PointLocationSet pls = new PointLocationSet();
                double cos = Math.Cos(theta);
                double sin = Math.Sin(theta);
                for (int x = -H.Value; x <= H.Value; x++)
                {
                    for (int y = -H.Value; y <= H.Value; y++)
                    {
                        double a = cos * y - sin * x;
                        double b = sin * y + cos * x;
                        if (a > W.Value || a < 0)
                        {
                            continue;
                        }
                        if (b < -H.Value || b > 0)
                        {
                            continue;
                        }
                        pls.Add(new PointLocation(-y, x));
                    }
                }
                lpls.Add(pls);
            }
            this.ODCBoxes.AddRange(lpls);
        }
    }

}
