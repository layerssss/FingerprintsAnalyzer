using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetDistance
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetDistanceProcess(), args);
        }
    }
    class FRAProcess_GetDistanceProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocation pl1;
        PointLocation pl2;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            pl1 = inputter.GetArg<PointLocation>("点1");
            pl2 = inputter.GetArg<PointLocation>("点2");
        }
        DoubleFloat distance;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("距离", distance);
        }

        public override void Procedure()
        {
            distance = new DoubleFloat()
            {
                Value = Math.Sqrt((pl1.I - pl2.I) * (pl1.I - pl2.I) + (pl1.J - pl2.J) * (pl1.J - pl2.J))
            };
        }
    }

}
