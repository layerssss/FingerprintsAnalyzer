using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GApplyFilter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GApplyFilterProcess(), args);
        }
    }
    class FRAProcess_GApplyFilterProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }

        OrientationGraph OG;
        GrayLevelImage IMG;
        FilterSet fs;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {

            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
            this.IMG = inputter.GetArg<GrayLevelImage>("原图像");
            this.fs = inputter.GetArg<FilterSet>("AGF8-7");
        }
        Filter Result = new Filter();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("滤波结果", this.Result);
        }

        public override void Procedure()
        {
            this.Result.Allocate(this.IMG, 0);
            int w = 10;
            for (int i = w; i < this.Result.Height - w; i++)
            {
                for (int j = w; j < this.Result.Width - w; j++)
                {
                    var qtheta = (int)((this.OG.Value[i][j] / Math2.PI(0.125) + 0.5) % 8);
                    double val = this.fs[qtheta].GetOutput(this.IMG, i, j);
                    this.Result.Value[i][j] = val;//>0?1:0;
                }
            }
        }
    }

}
