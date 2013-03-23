using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SWindowAVGAdjust
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SWindowAVGAdjustProcess(), args);
        }
    }
    class FRAProcess_SWindowAVGAdjustProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }

        DoubleFloat Threshold;
        GrayLevelImage IMG;
        Integer W;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.Threshold = inputter.GetArg<DoubleFloat>("初始前景AVG阀值");
            this.IMG = inputter.GetArg<GrayLevelImage>("原图像");
            this.W = inputter.GetArg<Integer>("初始W");
        }
        
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("前景AVG阀值", this.Threshold);
            outputter.PutArg("W", this.W);
        }

        public override void Procedure()
        {
            while (this.Threshold.Present(this.IMG, "请调节：前景AVG阀值")&&
                this.W.Present(this.IMG, "请调节：W"))
            {
                this.SaveAsInput(this.Threshold, "前景AVG阀值");
                this.SaveAsInput(this.IMG, "原图像");
                this.SaveAsInput(this.W, "W");
                this.CallProcess("SWindowAVG");
                var SA = this.LoadFromOutput<SegmentationArea>("切割区域");
                SA.Present(this.IMG, "切割区域");
            }
        }
    }

}
