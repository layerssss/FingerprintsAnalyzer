using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_TwoCores
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_TwoCoresProcess(), args);
        }
    }
    class FRAProcess_TwoCoresProcess : FRAProcess.FRAProcess
    {
        PointLocation core1;
        OrientationGraph og;
        PortionSize Size;
        WeightFunction w;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {

            core1 = inputter.GetArg<PointLocation>("core1");
            this.Size = inputter.GetArg<PortionSize>("图像尺寸");
            this.w = inputter.GetArg<WeightFunction>("影响权值");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("重建结果-TC", og);
        }

        public override void Procedure()
        {
            og = new OrientationGraph();
            og.Allocate(this.Size.W, this.Size.H);
            for (int i = 0; i < Size.H; i++)
            {
                for (int j = 0; j < Size.W; j++)
                {
                   
                }
            }
        }
    }

}
