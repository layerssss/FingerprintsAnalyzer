using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPAVGFromBG
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPAVGFromBGProcess(), args);
        }
    }
    class FRAProcess_SPAVGFromBGProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph Core;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            Core = inputter.GetArg<BinaryGraph>("CORE位置");
        }
        PointLocation refPoint;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("参考点位置", refPoint);
        }

        public override void Procedure()
        {
            List<int> allI = new List<int>();
            List<int> allJ = new List<int>();
            this.Core.FindAny((ti, tj, tv) =>
            {
                if (tv == 1)
                {
                    allI.Add(ti);
                    allJ.Add(tj);
                }
                return false;
            });
            try
            {
                refPoint = new PointLocation((int)(allI.Average((tv => (double)tv)) + 0.5), (int)(allJ.Average(tv => (double)tv) + 0.5));
            }
            catch
            {
                refPoint = new PointLocation(0, 0);
            }
        }
    }

}
