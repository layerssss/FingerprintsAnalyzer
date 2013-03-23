using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSGetReferenceSeq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSGetReferenceSeqProcess(), args);
        }
    }
    class FRAProcess_CSGetReferenceSeqProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage CurveGraph;
        Integer SampleLength;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            CurveGraph = inputter.GetArg<GrayLevelImage>("CURVE强度图");
            SampleLength = inputter.GetArg<Integer>("样本长度");
        }
        PointLocation refPoint;
        DoubleFloatArray curveSample;
        DoubleFloat curveSampleRotate;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CURVE强度样本基准点", refPoint);
            outputter.PutArg("CURVE强度样本", curveSample);
            outputter.PutArg("CURVE强度样本旋转度", curveSampleRotate);
        }

        public override void Procedure()
        {
            double maxSum = 0;
            double maxTheta = 0;
            int maxI = 0;
            int maxJ = 0;
            CurveGraph.FindAny((ti, tj,tv) =>
            {
                if (tv < 0.3)
                {
                    return false;
                }
                for (double theta = 0.166 * Math.PI; theta < 0.833 * Math.PI; theta += 0.05)
                {
                    double sum = 0;
                    for (int l = 0; l < SampleLength.Value; l++)
                    {
                        int ii = ti - (int)Math.Round(Math.Sin(theta) * l);
                        int jj = tj + (int)Math.Round(Math.Cos(theta) * l);
                        if (!CurveGraph.IsInbound(ii, jj))
                        {
                            continue;
                        }
                        sum += CurveGraph.Value[ii][jj];//+ l * 0.007;
                    }
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxTheta = theta;
                        maxI = ti;
                        maxJ = tj;
                    }
                }
                return false;
            });
            this.curveSample = new DoubleFloatArray();
            this.curveSampleRotate = new DoubleFloat();
            this.refPoint = new PointLocation();

            for (int l = 0; l < SampleLength.Value; l++)
            {
                int ii = maxI - (int)Math.Round(Math.Sin(maxTheta) * l);
                int jj = maxJ + (int)Math.Round(Math.Cos(maxTheta) * l);
                if (!CurveGraph.IsInbound(ii, jj))
                {
                    continue;
                }
                this.curveSample.Add(CurveGraph.Value[ii][jj]);// + l * 0.007);
                //sum += CurveGraph.Value[ii][jj];
            }
            this.curveSampleRotate.Value = maxTheta;
            this.refPoint.I = maxI;
            this.refPoint.J = maxJ;
            Console.WriteLine("theta={0};", maxTheta);
        }
    }

}
