using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_OneCore
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OneCoreProcess(), args);
        }
    }
    class FRAProcess_OneCoreProcess : FRAProcess.FRAProcess
    {
        PointLocation core1;
        OrientationGraph og;
        PortionSize Size;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            core1 = inputter.GetArg<PointLocation>("core1");
            this.Size = inputter.GetArg<PortionSize>("图像尺寸");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("重建结果-C", og);
        }

        public override void Procedure()
        {
            this.og = new OrientationGraph();
            this.og.Allocate(Size.W, Size.H);
            this.og.FindAny((ti,tj)=>{
                this.og.Value[ti][tj] = (Math.Atan2((this.core1.I - ti) , (tj-this.core1.J)) + 1.5 * Math.PI)%(Math.PI);
                return false;
            });
        }
    }

}
