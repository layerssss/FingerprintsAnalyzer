using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BGaborFixedFreq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BGaborFixedFreqProcess(), args);
        }
    }
    class FRAProcess_BGaborFixedFreqProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        GrayLevelImage IMG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
            this.IMG = inputter.GetArg<GrayLevelImage>("原图像");
        }
        BinaryGraph BG = new BinaryGraph();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("Gabor二值图", this.BG);
        }

        public override void Procedure()
        {
            this.BG.Allocate(this.IMG, 1);
            int w = 10;
            GaborFilter filter = new GaborFilter(w, 0.1, 25, 25);
            Console.WriteLine("Filter OK");
            for (int i = w; i < this.BG.Height - w; i++)
            {
                for (int j = w; j < this.BG.Width - w; j++)
                {
                    double val=filter.Filter(this.IMG, this.OG, i, j);
                    this.BG.Value[i][j] = val>-1?1:0;
                }
            }
        }
    }
    public class GaborFilter
    {
        public GaborFilter(int w, double f, double deltaXsq, double deltaYsq)
        {
            this.w = w;
            this.val = Array.CreateInstance(typeof(double), 8, w * 2 + 1, w * 2 + 1);
            for (var qtheta = 0; qtheta < 8; qtheta++)
            {
                var theta = Math2.PI(qtheta * 0.125);
                var sin = Math.Sin(theta);
                var cos = Math.Cos(theta);
                for (var j = -w; j <= w; j++)
                {

                    for (var i = -w; i <= w; i++)
                    {
                        var xtheta = j * sin + i * cos;
                        var ytheta = -j * cos + i * sin;
                        var cos2pifx = Math.Cos(Math2.PI(2 * f * xtheta));
                        this.val.SetValue(
                            Math.Exp(-0.5 * (Math2.Sqr(xtheta) / deltaXsq + Math2.Sqr(ytheta) / deltaYsq))
                            * cos2pifx, qtheta, i + w, j + w);
                    }
                }
            }
        }
        public double Filter(GrayLevelImage graph, OrientationGraph og, int i, int j)
        {
            var sum = 0.0;
            for (var ti = -this.w; ti <= this.w; ti++)
            {
                for (var tj = -this.w; tj <= this.w; tj++)
                {
                    var qtheta = (int)((og.Value[i][j] / Math2.PI(0.125) + 0.5) % 8);
                    sum += graph.Value[i + ti][j + tj] * (double)this.val.GetValue(qtheta, ti + w, tj + w);
                }
            }
            return sum;
        }
        Array val;
        int w;
    }
}
