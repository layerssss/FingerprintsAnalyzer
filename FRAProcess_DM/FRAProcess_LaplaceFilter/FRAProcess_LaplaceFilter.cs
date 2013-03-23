using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_LaplaceFilter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_LaplaceFilterProcess(), args);
        }
    }
    class FRAProcess_LaplaceFilterProcess : FRAProcess.FRAProcess
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
            outputter.PutArg("拉普拉斯滤波器", this.filter);
        }

        public override void Procedure()
        {
            this.filter = new Filter();
            this.filter.Allocate(1);
            this.filter.Value = new[]{
                new []{+0.0,-1.0,+0.0},
                new []{-1.0,+5.0,-1.0},
                new []{+0.0,-1.0,+0.0}
            };
        }
    }

}
