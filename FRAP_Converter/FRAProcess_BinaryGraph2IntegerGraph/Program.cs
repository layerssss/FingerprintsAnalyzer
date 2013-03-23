using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BinaryGraph2IntegerGraph
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BinaryGraph2IntegerGraphProcess(), args);
        }
    }
    class FRAProcess_BinaryGraph2IntegerGraphProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph bg;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.bg = inputter.GetArg<BinaryGraph>("二值图");
        }
        IntergerGraph ig;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", ig);
        }

        public override void Procedure()
        {
            ig = bg.Clone();
        }
    }

}
