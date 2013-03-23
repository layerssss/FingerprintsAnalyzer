using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GApplyGaborDynFreq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GApplyGaborDynFreqProcess(), args);
        }
    }
    class FRAProcess_GApplyGaborDynFreqProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        GrayLevelImage IMG;
        FilterSet fs;
        IntergerGraph FG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
            this.IMG = inputter.GetArg<GrayLevelImage>("原图像");
            this.fs = inputter.GetArg<FilterSet>("TGF8-086");
            this.FG = inputter.GetArg<IntergerGraph>("频率图");
        }

        BinaryGraph Result;
        Filter Result2=new Filter();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("滤波结果", this.Result);
            outputter.PutArg("滤波结果", this.Result2);
        }

        public override void Procedure()
        {
            this.Result2.Allocate(this.IMG, 1);
            int w = 10;
            for (int i = w; i < this.Result2.Height - w; i++)
            {
                for (int j = w; j < this.Result2.Width - w; j++)
                {
                    var qtheta = (int)((this.OG.Value[i][j] / Math2.PI(0.125) + 0.5) % 8);
                    double val = this.fs[qtheta+this.FG.Value[i][j]].GetOutput(this.IMG, i, j);
                    this.Result2.Value[i][j] = val;
                }
            }
            this.Result = new BinaryGraph(this.Result2.Clone(tv => tv > -1? 1 : 0));
        }
    }

}
