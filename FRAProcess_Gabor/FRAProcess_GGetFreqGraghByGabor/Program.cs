using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GGetFreqGraghByGabor
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GGetFreqGraghByGaborProcess(), args);
        }
    }
    class FRAProcess_GGetFreqGraghByGaborProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
        }
        GrayLevelImage img;
        FilterSet fs;
        OrientationGraph OG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.fs = inputter.GetArg<FilterSet>("TGF8-086");
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
        }
        IntergerGraph FG = new IntergerGraph();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("频率图", this.FG);
        }

        public override void Procedure()
        {
            this.FG.Allocate(this.img, 0);
            this.FG.ForEach((tti, ttj) =>
            {
                if (tti % 40 != 0 || ttj % 40 != 0)
                {
                    return;
                }
                if (PointLocation.CloseToBoundary(this.FG,10,tti,ttj))
                {
                    return;
                }
                var win = Window.GetSquare(tti, ttj, 20);
                var maxResult = 0.0;
                var maxqf = 0;
                win.ForEach((ti, tj) =>
                {
                    if (tti % 5 != 0 || ttj % 5 != 0)
                    {
                        return;
                    }
                    if (PointLocation.CloseToBoundary(this.FG, 20, ti, tj))
                    {
                        return;
                    }
                    var qtheta = (int)((this.OG.Value[ti][tj] / Math2.PI(0.125) + 0.5) % 8);
                    for (int qf = 0; qf < this.fs.Count; qf += 8)
                    {
                        var result = this.fs[qtheta + qf].GetOutput(this.img, ti, tj);
                        if (result > maxResult)
                        {
                            maxResult = result;
                            maxqf = qf;
                        }
                    }
                });
                win.ForEach((ti, tj) =>
                {
                    if (PointLocation.CloseToBoundary(this.FG, 20, ti, tj))
                    {
                        return;
                    }
                    this.FG.ValueSet(ti, tj, maxqf);
                });
            });
        }
    }

}
