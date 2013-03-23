using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_AVGFilter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_AVGFilterProcess(), args);
        }
    }
    class FRAProcess_AVGFilterProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
        }
        Filter filter;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("均值滤波器", this.filter);
        }

        public override void Procedure()
        {
            this.filter = Filter.GetFilter(2, (ti, tj) =>
            {
                return 0.04;
            });
            
        }
    }

}
