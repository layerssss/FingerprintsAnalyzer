using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SFixedThreshold
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SFixedThresholdProcess(), args);
        }
    }
    class FRAProcess_SFixedThresholdProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloat t;
        GrayLevelImage img;
        BoolData biggerBackgrount;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.t = inputter.GetArg<DoubleFloat>("阀值");
            this.img = inputter.GetArg<GrayLevelImage>("灰度图");
            this.biggerBackgrount = inputter.GetArg<BoolData>("背景大于阀值");
        }
        SegmentationArea area;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", area);
        }

        public override void Procedure()
        {
            area = new SegmentationArea();
            area.Allocate(img);
            img.FindAny((ti, tj, tv) =>
            {
                area.Value[ti][tj] = (biggerBackgrount.Value ^ (tv > t.Value)) ? 1 : 0;
                return false;
            });
        }
    }

}
