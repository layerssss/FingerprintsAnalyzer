using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_ApplySegmentation
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_ApplySegmentationProcess(), args);
        }
    }
    class FRAProcess_ApplySegmentationProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        SegmentationArea sa;
        BinaryGraph bg;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.sa = inputter.GetArg<SegmentationArea>("切割区域");
            this.bg = inputter.GetArg<BinaryGraph>("二值图");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("有效二值图", bg);
        }

        public override void Procedure()
        {
            bg.Accumulate((ti, wi) => wi == 1 ? ti : 1, sa);
        }
    }

}
