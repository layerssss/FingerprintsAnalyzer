using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_GetPI
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetPIProcess(), args);
        }
    }
    class FRAProcess_GetPIProcess : FRAProcess.FRAProcess
    {
        DoubleFloat x;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.x = inputter.GetArg<DoubleFloat>("PI的倍数");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("xPI", this.x);
        }

        public override void Procedure()
        {
            this.x.Value *= Math.PI;
        }
    }

}
