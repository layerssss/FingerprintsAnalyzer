using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_ShrinkPointLocationSet
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_ShrinkPointLocationSetProcess(), args);
        }
    }
    class FRAProcess_ShrinkPointLocationSetProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        Integer ExtW;
        Integer ExtH;
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            ExtH = inputter.GetArg<Integer>("上下边距");
            ExtW = inputter.GetArg<Integer>("左右边距");
            img = inputter.GetArg<GrayLevelImage>("原图像");
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
