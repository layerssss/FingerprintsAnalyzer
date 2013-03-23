using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSApplyReferenceSeq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSApplyReferenceSeqProcess(), args);
        }
    }
    class FRAProcess_CSApplyReferenceSeqProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloatArray CurveSample;
        GrayLevelImage CurveGraph;
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            SA = inputter.GetArg<SegmentationArea>("切割区域");
            CurveGraph = inputter.GetArg<GrayLevelImage>("CURVE强度图");
            CurveSample = inputter.GetArg<DoubleFloatArray>("CURVE强度样本");
        }
        PointLocation refPoint;
        DoubleFloat curveSampleRotate;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("参考点", refPoint);
        }

        public override void Procedure()
        {
            double maxSum = double.MaxValue;
            double maxTheta = 0;
            int maxI = 0;
            int maxJ = 0;
            int debugLastI = 0;
            CurveGraph.FindAny((ti, tj, tv) =>
            {
                //if (tv <0.5)
                //{
                //    return false;
                //}
                for (double theta = 0.166 * Math.PI; theta < 0.833 * Math.PI; theta += 0.2)
                {
                    double sum = 0;
                    int count = 0;
                    for (int l = 0; l < this.CurveSample.Count; l++)
                    {
                        int ii = ti - (int)Math.Round(Math.Sin(theta) * l);
                        int jj = tj + (int)Math.Round(Math.Cos(theta) * l);
                        if (!SA.IsInArea(ii, jj))
                        {
                            continue;
                        }
                        sum += Math.Abs(CurveGraph.Value[ii][jj] - this.CurveSample[l]);
                        count++;
                    }
                    if (count == 0)
                    {
                        continue;
                    }
                    sum /= count;
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxTheta = theta;
                        maxI = ti;
                        maxJ = tj;
                    }
                }
                if (ti != debugLastI)
                {
                    debugLastI = ti;
                    this.Progress(ti, CurveGraph.Height);
                }
                return false;
            });

            this.curveSampleRotate = new DoubleFloat();
            this.refPoint = new PointLocation();

            this.curveSampleRotate.Value = maxTheta;
            this.refPoint.I = maxI;
            this.refPoint.J = maxJ;
            Console.WriteLine("theta={0};sum={1};", maxTheta,maxSum);
        }
    }

}
