using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPCurve
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPCurveProcess(), args);
        }
    }
    public class FRAProcess_SPCurveProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        Integer W;
        SegmentationArea SA;
        //GrayLevelImage coherence;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("走向图");
            this.W = inputter.GetArg <Integer>("W");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            //this.coherence = inputter.GetArg<GrayLevelImage>("连贯性");
        }
        GrayLevelImage CORE;
        PointLocation MAXIMA;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CORE强度", CORE);
            outputter.PutArg("最大CORE强度点", MAXIMA);
        }

        public override void Procedure()
        {
            CORE = new GrayLevelImage();
            CORE.Allocate(OG,0);
            Window win = Window.GetSquare(1, 1, Math.Max(CORE.Width, CORE.Height), CORE);
            this.MAXIMA = new PointLocation(0, 0);
            for (int i = 0; i < 1; i++)
            {
                double maxCore = 0;
                double bestWeightI = 1;
                double bestWeightJ = 1;
                #region Optimizing
                //for (double weightI = 0.1; weightI <= 2.5; weightI += 0.2)
                //{
                //    for (double weightJ = 0.1; weightJ <= 2.5; weightJ += 0.2)
                //    {
                //        double curCore;
                //        RefineCoreCenter(this.OG, this.W, this.CORE, this.MAXIMA, this.SA, win, out curCore, weightI, weightJ);
                //        if (curCore > maxCore)
                //        {
                //            maxCore = curCore;
                //            bestWeightI = weightI;
                //            bestWeightJ = weightJ;
                //        }
                //    }
                //} 
                #endregion
                RefineCoreCenter(this.OG, this.W, this.CORE, this.MAXIMA, this.SA, win, out maxCore, bestWeightI, bestWeightJ);
                win = Window.GetSquare(MAXIMA.I, MAXIMA.J, 80,OG);
                Console.WriteLine(string.Format("i:{0};j:{1};max={2}", bestWeightI, bestWeightJ,maxCore));
            }

        }
        public static void RefineCoreCenter(OrientationGraph OG,Integer W,GrayLevelImage CORE,PointLocation MAXIMA,SegmentationArea SA,Window win,out double maxCore,double weightI,double weightJ)
        {
            CORE.Allocate(OG, 0);
            win.ForEach((ti, tj) =>
            {
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
                double w = 0.5 * Math.Sqrt((maxI - minI) * (maxI - minI) + (maxJ - minJ) * (maxJ - minJ));
                if (w == 0)
                {
                    return false;
                }
                double centerI = ((double)maxI + minI) / 2;
                double centerJ = ((double)maxJ + minJ) / 2;
                double gamma = (0.5 * (maxCurve - minCurve));
                if (gamma == 0)
                {
                    return false;
                }
                double l = w / Math.Tan(gamma);

                double theta = Math2.MidOF(0.5, OG.Value[ti][tj] + maxCurve, 0.5, OG.Value[ti][tj] + minCurve);
                if (Math.Sin(theta) * (minI - maxI) + Math.Cos(theta) * (maxJ - minJ) < 0)
                {
                    theta += Math.PI;
                }
                int targetI = (int)(centerI - Math.Sin(theta + 0.5 * Math.PI) * l * weightI + 0.5);
                int targetJ = (int)(centerJ + Math.Cos(theta + 0.5 * Math.PI) * l * weightJ + 0.5);
                if (OG.IsInbound(targetI, targetJ))
                {
                    CORE.Value[targetI][targetJ] += 1 / (maxCurve - minCurve);//1;/// coherence.Value[(int)(centerI+0.5)][(int)(centerJ+0.5)];
                }
                return false;
            });
            double cmaxCore = 0;
            CORE.FindAny((ti, tj) =>
            {
                if (CORE.Value[ti][tj] > cmaxCore)
                {
                    cmaxCore = CORE.Value[ti][tj];
                    MAXIMA.I = ti;
                    MAXIMA.J = tj;
                }
                return false;
            });
            Console.WriteLine("cmaxCore={0}", cmaxCore);
            CORE.FindAny((ti, tj) =>
            {
                CORE.Value[ti][tj] /= 150;
                return false;
            });

            double maxSum = 0;
            CORE.FindAny((ti, tj) =>
            {
                Window w = Window.GetSquare(ti, tj, 3, CORE);
                double sumCore = 0;
                w.ForEach(CORE, tv =>
                {
                    sumCore += tv;
                });
                if (sumCore > maxSum)
                {
                    maxSum = sumCore;
                    MAXIMA.I = ti;
                    MAXIMA.J = tj;
                }
                return false;
            });
            maxCore = maxSum;
            Console.WriteLine("maxCore={0}", maxCore);
        }
    }

}
