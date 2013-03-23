using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_MEMergeNeighbourBIs
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEMergeNeighbourBIsProcess(), args);
        }
    }
    class FRAProcess_MEMergeNeighbourBIsProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntergerGraph MinutiaSkeleton;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            MinutiaSkeleton = inputter.GetArg<IntergerGraph>("原细节点骨架");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点骨架", MinutiaSkeleton);
        }

        public override void Procedure()
        {
            MinutiaSkeleton.FindAny((ti, tj, tv) =>
            {
                if (tv == 4)
                {
                    double sumi = 0;
                    double sumj = 0;
                    int count = 0;
                    MinutiaSkeleton.FillAdvance(ti, tj, 0, 0, MinutiaSkeleton.Height, MinutiaSkeleton.Width,
                        ttv => ttv == 4,
                        (tti, ttj, ttv) =>
                        {
                            sumi += tti;
                            sumj += ttj;
                            count++;
                            return 2;
                        });
                    MinutiaSkeleton.Value[(int)(sumi / count + 0.5)][(int)(sumj / count + 0.5)] = 4;
                }
                return false;
            });
        }
    }

}
