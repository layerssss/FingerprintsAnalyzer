using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MESumSurround
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MESumSurroundProcess(), args);
        }
    }
    class FRAProcess_MESumSurroundProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            BG = inputter.GetArg<BinaryGraph>("骨架");
        }
        IntergerGraph MinutiaSkeleton;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点骨架", MinutiaSkeleton);
        }

        public override void Procedure()
        {
            MinutiaSkeleton = BG.Clone();
            BG.FindAny((ti, tj, tv) =>
                {
                    if (tv == 0)
                    {
                        int sum = 8;
                        try
                        {
                            for (int k = 0; k < Drawing2.EightConnectionAreaI.Length; k++)
                            {
                                sum -= BG.Value[ti + Drawing2.EightConnectionAreaI[k]]
                                    [tj + Drawing2.EightConnectionAreaJ[k]];
                            }
                        }
                        catch {
                            MinutiaSkeleton.Value[ti][tj] = 1;
                            return false;
                        }
                        if (sum == 0)
                        {
                            MinutiaSkeleton.Value[ti][tj] = 1;
                            return false;
                        }

                        if (sum == 1)
                        {
                            MinutiaSkeleton.Value[ti][tj] = 3;
                            return false;
                        }
                        if (sum == 2)
                        {
                            MinutiaSkeleton.Value[ti][tj] = 2;
                            return false;
                        }
                        MinutiaSkeleton.Value[ti][tj] = 4;
                    }
                    return false;
                });
        }
    }

}
