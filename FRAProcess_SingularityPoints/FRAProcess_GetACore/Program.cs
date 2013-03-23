using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetACore
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetACoreProcess(), args);
        }
    }
    class FRAProcess_GetACoreProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        SegmentationArea SA;
        PoincareIndexes PI;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            SA = inputter.GetArg<SegmentationArea>("切割区域");
            PI = inputter.GetArg<PoincareIndexes>("PI");
        }
        PointLocation core;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("参考点", core);
        }

        public override void Procedure()
        {
            this.core = new PointLocation();
            PI.FindAny((ti, tj, tv) =>
            {
                if (!SA.IsInArea(ti, tj))
                {
                    return false;
                }
                if (tv == -1)
                {
                    this.core.I = ti;
                    this.core.J = tj;
                    return true;
                }
                return false;
            });
        }
    }

}
