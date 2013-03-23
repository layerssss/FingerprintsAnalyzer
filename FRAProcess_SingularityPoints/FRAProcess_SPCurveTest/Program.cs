using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPCurveTest
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPCurveTestProcess(), args);
        }
    }
    class FRAProcess_SPCurveTestProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        //Window win;
        GrayLevelImage img;
        Integer W;
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("走向图");
            //this.win = inputter.GetArg<Window>("样窗");
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            this.W = inputter.GetArg<Integer>("W");
        }
        //PointLocation CORE;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            //outputter.PutArg<PointLocation>("CORE",CORE);
        }

        public override void Procedure()
        {
            PointLocation PL = new PointLocation();
            PL = (PointLocation)PL.BuildInstance();
            while (PL.Present(img, "源计算点"))
            {
                int ti = PL.I;
                int tj = PL.J;
                if (!SA.IsInArea(ti, tj))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format(" not in area ti={0};tj={1};", ti, tj));
                    continue;
                }
                Window win = Window.GetSquare(ti, tj, W.Value);
                int maxI = (int)(ti - Math.Sin(OG.Value[ti][tj]) * W.Value + 0.5);
                int maxJ = (int)(tj + Math.Cos(OG.Value[ti][tj]) * W.Value + 0.5);

                int minI = (int)(ti + Math.Sin(OG.Value[ti][tj]) * W.Value + 0.5);
                int minJ = (int)(tj - Math.Cos(OG.Value[ti][tj]) * W.Value + 0.5);

                if ((!SA.IsInArea(minI, minJ)) || (!SA.IsInArea(maxI, maxJ)))
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("not in area min={0},{1};max={2},{3};",minI,minJ,maxI,maxJ));
                    continue;
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
                    System.Windows.Forms.MessageBox.Show(string.Format("w=0;"));
                    continue;
                }
                double centerI = ((double)maxI + minI) / 2;
                double centerJ = ((double)maxJ + minJ) / 2;
                double gamma = (0.5 * maxCurve - minCurve);
                double l = w / Math.Tan(gamma);

                double theta = Math2.MidOF(0.5, this.OG.Value[ti][tj] + maxCurve, 0.5, this.OG.Value[ti][tj] + minCurve);
                if (Math.Sin(theta) * (minI - maxI) + Math.Cos(theta) * (maxJ - minJ) <= 0)
                {
                    theta += Math.PI;
                }
                int targetI = (int)(centerI - Math.Sin(theta + 0.5 * Math.PI) * l + 0.5);
                int targetJ = (int)(centerJ + Math.Cos(theta + 0.5 * Math.PI) * l + 0.5);
                if (OG.IsInbound(targetI, targetJ))
                {
                    PointLocation target = new PointLocation(targetI, targetJ);
                    target.Present(img, "CORE位置");
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(string.Format("i={0};j={1};", targetI, targetJ));
                }
                continue;
            }
        }
    }

}
