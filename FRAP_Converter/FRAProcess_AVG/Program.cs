using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_AVG
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
        GrayLevelImage img;
        DoubleFloat avg = new DoubleFloat();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("当前图像");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<DoubleFloat>("AVG", avg);
        }

        public override void Procedure()
        {
            this.avg.Value = img.AVG();
        }
    }
}
