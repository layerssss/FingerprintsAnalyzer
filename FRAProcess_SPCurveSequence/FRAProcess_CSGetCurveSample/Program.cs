using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSGetCurveSample
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSGetCurveSampleProcess(), args);
        }
    }
    class FRAProcess_CSGetCurveSampleProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage CurveGraph;
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.CurveGraph = inputter.GetArg<GrayLevelImage>("CURVE强度图");
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            
        }
        DoubleFloatArray CurveSample;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CURVE样本", CurveSample);
        }

        public override void Procedure()
        {
            PointLocation pl = new PointLocation();
            Integer length = new Integer();
            while (pl.Present(CurveGraph, "计算点")&&length.Present(img,"样本长度"))
            {
                double maxSumCurve = 0;
                double bestTheta = 0;
                for (double theta = 0; theta < Math.PI*2; theta += 0.01)
                {
                    double sumCurve = 0;
                    for (int l = 0; l < length.Value; l++)
                    {
                        try
                        {
                            sumCurve += CurveGraph.Value[(int)Math.Round(pl.I - Math.Sin(theta) * l)][(int)Math.Round(pl.J + Math.Cos(theta) * l)];
                        }
                        catch
                        {
                        }
                    }
                    if (sumCurve > maxSumCurve)
                    {
                        maxSumCurve = sumCurve;
                        bestTheta = theta;
                    }
                }
                this.CurveSample = new DoubleFloatArray();
                for (int l = 0; l < length.Value; l++)
                {try
                        {
                            this.CurveSample.Add(CurveGraph.Value[(int)Math.Round(pl.I - Math.Sin(bestTheta) * l)][(int)Math.Round(pl.J + Math.Cos(bestTheta) * l)]);
                        }
                catch
                {
                }
                }
                Console.WriteLine("Theta={0};SumCurve={1}", bestTheta, maxSumCurve);
                this.CurveSample.Present(img, "CURVE样本");
            }
        }
    }

}
