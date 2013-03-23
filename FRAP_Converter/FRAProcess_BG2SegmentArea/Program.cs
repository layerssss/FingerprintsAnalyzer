using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BG2SegmentArea
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BG2SegmentAreaProcess(), args);
        }
    }
    class FRAProcess_BG2SegmentAreaProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph bg;

        BoolData reverse;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            bg = inputter.GetArg<BinaryGraph>("切割区域");
            reverse = inputter.GetArg<BoolData>("是否有效区域为前景");
        }

        SegmentationArea sa;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", sa);
        }

        public override void Procedure()
        {
            sa = new SegmentationArea(bg.Clone(ti => ti == 1 ^ reverse.Value ? 1 : 0));
        }
    }

}
