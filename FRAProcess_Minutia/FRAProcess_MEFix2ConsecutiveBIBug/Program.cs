using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEFix2ConsecutiveBIBug
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEFix2ConsecutiveBIBugProcess(), args);
        }
    }
    class FRAProcess_MEFix2ConsecutiveBIBugProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntergerGraph MinutiaSkeleton;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MinutiaSkeleton = inputter.GetArg<IntergerGraph>("原细节点骨架");

        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点骨架", MinutiaSkeleton);
        }

        public override void Procedure()
        {
            MinutiaSkeleton.FindAny((ti, tj, tv) =>
            {
                if (tv == 2)
                {
                    int k = 0;
                    int k1;
                    try
                    {
                        while (this.MinutiaSkeleton.Value[ti + Drawing2.EightConnectionAreaI[k]][tj + Drawing2.EightConnectionAreaJ[k]] == 1)
                        {
                            k++;
                        }
                        k1 = k;
                        k++;
                        while (this.MinutiaSkeleton.Value[ti + Drawing2.EightConnectionAreaI[k]][tj + Drawing2.EightConnectionAreaJ[k]] == 1)
                        {
                            k++;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                    if (k - k1 == 1 || k - k1 == 7)//Consecutive
                    {
                        if (this.MinutiaSkeleton.Value
                            [ti + Drawing2.EightConnectionAreaI[k1]]
                            [tj + Drawing2.EightConnectionAreaJ[k1]] + 
                            this.MinutiaSkeleton.Value
                            [ti + Drawing2.EightConnectionAreaI[k]]
                            [tj + Drawing2.EightConnectionAreaJ[k]] == 8)
                        {
                            this.MinutiaSkeleton.Value[ti][tj] = 3;
                        }
                    }
                }
                return false;
            });
        }
    }

}
