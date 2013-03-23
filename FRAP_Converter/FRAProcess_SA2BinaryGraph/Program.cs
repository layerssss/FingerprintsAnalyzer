using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SA2BinaryGraph
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SA2BinaryGraphProcess(), args);
        }
    }
    class FRAProcess_SA2BinaryGraphProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        SegmentationArea sa;
        BoolData reverse;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            sa = inputter.GetArg<SegmentationArea>("切割区域");
            reverse = inputter.GetArg<BoolData>("是否有效区域为前景");
        }
        BinaryGraph bg;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", bg);
        }

        public override void Procedure()
        {
            this.bg = new BinaryGraph(sa.Clone((ti) => ti == 1 ^ reverse.Value ? 1 : 0));
        }
    }

}
