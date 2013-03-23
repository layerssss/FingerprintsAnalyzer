using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPManualy
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPManualyProcess(), args);
        }
    }
    class FRAProcess_SPManualyProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            img = inputter.GetArg<GrayLevelImage>("原图像");
        }
        PointLocation refP;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("手动标出的参考点", refP);
        }

        public override void Procedure()
        {
            refP = new PointLocation();
            if (!refP.Present(img, "手动标出的参考点"))
            {
                throw (new FRAProcess.AlgorithmException("用户未手动标注参考点"));
            }
        }
    }

}
