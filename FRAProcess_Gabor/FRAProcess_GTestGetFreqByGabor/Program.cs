using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GTestGetFreqByGabor
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GTestGetFreqByGaborProcess(), args);
        }
    }
    class FRAProcess_GTestGetFreqByGaborProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        FilterSet fs;
        OrientationGraph OG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.fs = inputter.GetArg<FilterSet>("TGF8-680");
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            
        }

        public override void Procedure()
        {
            Window win = new Window() { };
            win.Present(this.img, "");
            var max6 = 0.0;
            var max8 = 0.0;
            var max0 = 0.0;
            var sum6 = 0.0;
            var sum8 = 0.0;
            var sum0 = 0.0;
            win.ForEach((ti, tj) =>
            {
                var qtheta = (int)((this.OG.Value[ti][tj] / Math2.PI(0.125) + 0.5) % 8);
                var result6 = this.fs[qtheta + 0].GetOutput(this.img, ti, tj);
                var result8 = this.fs[qtheta + 8].GetOutput(this.img, ti, tj);
                var result0 = this.fs[qtheta + 16].GetOutput(this.img, ti, tj);
                if (result6 > max6) { max6 = result6; }
                if (result8 > max8) { max8 = result8; }
                if (result0 > max0) { max0 = result0; }
                sum0 += Math.Abs(result0);
                sum6 +=  Math.Abs(result6);
                sum8 += Math.Abs(result8);
            });
            Console.WriteLine("{0}       {1}          {2}", max6, max8, max0);
            Console.WriteLine("{0}       {1}          {2}", sum6, sum8, sum0);
        }
    }

}
