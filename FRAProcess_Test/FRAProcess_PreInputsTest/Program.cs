using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_PreInputsTest
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new p(), args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            inputter.GetArg<GrayLevelImage>(str.Value);
        }
        StringData str;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            str = inputter.GetArg<StringData>("string");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
        }

        public override void Procedure()
        {
        }
    }
}
