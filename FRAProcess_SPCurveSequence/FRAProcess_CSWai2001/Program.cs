using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSWai2001
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSWai2001Process(), args);
        }
    }
    class FRAProcess_CSWai2001Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            OG = inputter.GetArg<OrientationGraph>("方向场");
        }
        GrayLevelImage CURVE;
        PointLocationSet PLS;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CURVE", CURVE);
            outputter.PutArg("PEAKS", PLS);
        }

        public override void Procedure()
        {
            this.CURVE = new GrayLevelImage(this.OG);
            this.PLS = new PointLocationSet();
            this.OG.FindAny((ti, tj) =>
            {
                double sum = 0;
                if (ti == 0 || tj == 0 || ti == OG.Height - 1 || tj == OG.Width - 1)
                {
                    return false;
                }
                Drawing2.EightConnectionArea((ii, jj, k) =>
                {
                    sum += (1 - Math.Cos(2 * (this.OG.Value[ti + ii][tj + jj] - this.OG.Value[ti][tj])));
                });
                this.CURVE.Value[ti][tj] = sum / 17;
                return false;
            });
            var arr = new List<double>();
            this.CURVE.FindAny((ti, tj, tv) =>
            {
                arr.Add(tv);
                return false;
            });
            {
                var avg = arr.Average();
                var sum = 0.0;
                var n = 0;
                arr.ForEach(tv =>
                {
                    sum += (tv - avg) * (tv - avg);
                    n++;
                });
                var std = Math.Sqrt(sum / n);
                Console.WriteLine("avg:{0};std:{1};", avg,std);
                this.CURVE.FindAny((ti, tj, tv) =>
                {
                    if (ti == 0 || tj == 0 || ti == OG.Height - 1 || tj == OG.Width - 1)
                    {
                        return false;
                    }
                    if (tv < avg + std)
                    {
                        return false;
                    }
                    bool littleThan = false;
                    Drawing2.EightConnectionArea((ii, jj, k) =>
                    {
                        if (tv < this.CURVE.Value[ti + ii][tj + jj])
                        {
                            littleThan = true;
                        }
                    });
                    if (littleThan)
                    {
                        return false;
                    }
                    PLS.Add(new PointLocation(ti, tj));
                    return false;
                });
            }
        }
    }

}
