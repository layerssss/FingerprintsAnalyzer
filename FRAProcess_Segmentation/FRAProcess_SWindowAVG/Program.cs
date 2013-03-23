using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SWindowAVG
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SWindowAVGProcess(), args);
        }
    }
    class FRAProcess_SWindowAVGProcess : FRAProcess.FRAProcess
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
            this.Threshold = inputter.GetArg<DoubleFloat>("前景AVG阀值");
            this.IMG = inputter.GetArg<GrayLevelImage>("原图像");
            this.W = inputter.GetArg<Integer>("W");
        }
        SegmentationArea SA;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", SA);
        }

        public override void Procedure()
        {
            this.SA = new SegmentationArea();
            this.SA.Allocate(this.IMG);
            this.SA.FindAny((ti, tj) =>
            {
                var win = Window.GetSquare(ti, tj, this.W.Value, this.IMG);
                var sum = 0.0;
                var count = 0;
                win.ForEach(this.IMG, tv =>
                {
                    sum += tv;
                    count++;
                });
                this.SA.Value[ti][tj] = sum / count > this.Threshold.Value ? 0 : 1;
                return false;
            });
        }
    }

}
