using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEGetPLsFromMinutiaSkeleton
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEGetPLsFromMinutiaSkeletonProcess(), args);
        }
    }
    class FRAProcess_MEGetPLsFromMinutiaSkeletonProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntergerGraph MinutiaSkeleton;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MinutiaSkeleton = inputter.GetArg<IntergerGraph>("细节点骨架");
        }
        PointLocationSet REs;
        PointLocationSet BIs;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("RE", REs);
            outputter.PutArg("BI", BIs);
        }

        public override void Procedure()
        {
            this.REs = new PointLocationSet();
            this.BIs = new PointLocationSet();
            this.MinutiaSkeleton.FindAny((ti, tj, tv) =>
            {
                if (tv == 3)
                {
                    REs.Add(new PointLocation(ti, tj));
                }
                if (tv == 4)
                {
                    BIs.Add(new PointLocation(ti, tj));
                }
                return false;
            });
        }
    }

}
