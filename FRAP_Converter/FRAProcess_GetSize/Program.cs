using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_GetSize
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetSizeProcess(), args);
        }
    }
    class FRAProcess_GetSizeProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.W = inputter.GetArg<Integer>("W");
            this.H = inputter.GetArg<Integer>("H");
        }
        Integer W;
        Integer H;
        PortionSize Size;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("图像尺寸", this.Size);
        }

        public override void Procedure()
        {
            this.Size = new PortionSize() { W = this.W.Value, H = this.H.Value };
        }
    }

}
