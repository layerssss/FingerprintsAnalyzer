using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MERemoveMinutiaeNearRef
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MERemoveMinutiaeNearRefProcess(), args);
        }
    }
    class FRAProcess_MERemoveMinutiaeNearRefProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet pls1;
        PointLocation refP;
        Integer distance;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.pls1 = inputter.GetArg<PointLocationSet>("原细节点");
            this.refP = inputter.GetArg<PointLocation>("参考点");
            this.distance = inputter.GetArg<Integer>("最小距离");
        }
        PointLocationSet pls2;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点", pls2);
        }

        public override void Procedure()
        {
            pls2 = new PointLocationSet();
            foreach (PointLocation pl in pls1)
            {
                if ((pl.I - refP.I) * (pl.I - refP.I) + (pl.J - refP.J) * (pl.J - refP.J) >= distance.Value * distance.Value)
                {
                    pls2.Add(pl);
                }
            }
        }
    }

}
