using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPAVGCore
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPAVGCoreProcess(), args);
        }
    }
    class FRAProcess_SPAVGCoreProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage Core;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.Core = inputter.GetArg<GrayLevelImage>("CORE强度");
        }
        PointLocation PL;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CORE强度重心",PL);
        }

        public override void Procedure()
        {
            double sumI = 0;
            double sumJ = 0;
            double sumWeight = 0;
            this.Core.FindAny((ti, tj, tv) =>
            {
                sumI += ((double)ti) * tv;
                sumJ += ((double)tj) * tv;
                sumWeight += tv;
                return false;
            });
            PL = new PointLocation((int)(sumI / sumWeight + 0.5), (int)(sumJ / sumWeight + 0.5));
        }
    }

}
