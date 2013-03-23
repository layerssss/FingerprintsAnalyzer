using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSGetCurveGraphA
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSGetCurveGraphAProcess(), args);
        }
    }
    class FRAProcess_CSGetCurveGraphAProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        SegmentationArea SA;
        Integer W;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            OG = inputter.GetArg<OrientationGraph>("走向图");
            SA = inputter.GetArg<SegmentationArea>("切割区域");
            W = inputter.GetArg<Integer>("W");
        }
        //PointLocationSet RefPoints;
        GrayLevelImage CurveGraph;
        //PointLocation MAXIMA;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            //outputter.PutArg("参考点集", RefPoints);
            outputter.PutArg("CURVE强度图", CurveGraph);
            //outputter.PutArg("最大CURVE强度点", MAXIMA);
        }

        public override void Procedure()
        {
            CurveGraph = new GrayLevelImage();
            CurveGraph.Allocate(OG, 0);
            CurveGraph.FindAny((ti, tj) =>
            {
                if (!SA.IsInArea(ti, tj))
                {
                    return false;
                }

                if (!SA.IsInArea(ti, tj))
                {
                    return false;
                }
                int maxI = (int)(ti - Math.Sin(OG.Value[ti][tj]) * W.Value + 0.5);
                int maxJ = (int)(tj + Math.Cos(OG.Value[ti][tj]) * W.Value + 0.5);

                int minI = (int)(ti + Math.Sin(OG.Value[ti][tj]) * W.Value + 0.5);
                int minJ = (int)(tj - Math.Cos(OG.Value[ti][tj]) * W.Value + 0.5);

                if ((!SA.IsInArea(minI, minJ)) || (!SA.IsInArea(maxI, maxJ)))
                {
                    return false;
                }
                double maxCurve = Math2.DifOF(OG.Value[maxI][maxJ], OG.Value[ti][tj]);
                double minCurve = Math2.DifOF(OG.Value[minI][minJ], OG.Value[ti][tj]);
                if (maxCurve < minCurve)
                {
                    Math2.Swap(ref maxI, ref minI);
                    Math2.Swap(ref maxJ, ref minJ);
                    Math2.Swap(ref maxCurve, ref minCurve);
                }

                //double sumCurve = 0;
                //int countCurve = 0;
                //Window win = Window.GetSquare(ti, tj, W.Value, OG);
                //win.ForEach((tii, tjj) =>
                //{
                //    if (!SA.IsInArea(tii, tjj))
                //    {
                //        return;
                //    }
                //    double curve = Math2.DifOF(OG.Value[tii][tjj], OG.Value[ti][tj]);

                //    if (curve < minCurve)
                //    {
                //        minCurve = curve;
                //        minI = tii;
                //        minJ = tjj;
                //    }
                //    if (curve > maxCurve)
                //    {
                //        maxCurve = curve;
                //        maxI = tii;
                //        maxJ = tjj;
                //    }

                //    sumCurve += Math.Abs(curve);
                //    countCurve++;
                //});

                //if (maxJ >= minJ)
                //{
                //    return false;
                //}
                CurveGraph.Value[ti][tj] = maxCurve - minCurve;
                return false;
            });
            #region MAXIMATEST
            double maxSumCurve = 0;
            CurveGraph.FindAny((ti, tj, tv) =>
            {
                if (tv > maxSumCurve)
                {
                    maxSumCurve = tv;
                }
                return false;
            });
            CurveGraph.FindAny((ti, tj) =>
            {
                CurveGraph.Value[ti][tj] /= 3;//maxCurve;
                return false;
            });
            //this.MAXIMA = CurveGraph.GetMAXIMA(W.Value);
            Console.WriteLine("maxSumCurve={0};", maxSumCurve);


            #endregion
        }
    }

}
