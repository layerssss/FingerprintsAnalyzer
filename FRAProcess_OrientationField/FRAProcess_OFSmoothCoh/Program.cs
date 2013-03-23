using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFSmoothCoh
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFSmoothCohProcess(), args);
        }
    }
    class FRAProcess_OFSmoothCohProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            throw new NotImplementedException();
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            throw new NotImplementedException();
        }

        public override void Procedure()
        {
            throw new NotImplementedException();
        }
    }

}
