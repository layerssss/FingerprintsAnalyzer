using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SReverse
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SReverseProcess(), args);
        }
    }
    class FRAProcess_SReverseProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("新切割区域", this.SA);
        }

        public override void Procedure()
        {

            this.SA.FindAny((ti, tj, tv) =>
            {
                this.SA.ValueSet(ti, tj, 1 - tv);
                return false;
            });

            var aaaa = new[] { 1, 2, 3, 5, 6, 7, 8 };
            var count = 0;
            aaaa.Any(ti =>
            {
                count += ti;
                return false;
            });
            List<string> aa = new List<string>();
            
        }
    }

}
