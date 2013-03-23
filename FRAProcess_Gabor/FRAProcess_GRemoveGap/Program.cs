using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GRemoveGap
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GRemoveGapProcess(), args);
        }
    }
    class FRAProcess_GRemoveGapProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        MinutiaeSet MS;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MS = inputter.GetArg<MinutiaeSet>("过滤后的细节点集");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("干净的细节点集", this.MS);
        }

        public override void Procedure()
        {
            Func<bool> d = (() =>
            {
                foreach (var m in this.MS)
                {
                    foreach (var m2 in this.MS)
                    {
                        if (m2 == m)
                        {
                            continue;
                        }
                        if (m2.Location.Distance(m.Location) < 8&&Math.Abs(Math2.DifDf(m2.Angle,m.Angle))>Math2.PI(0.5))
                        {
                            this.MS.Remove(m2);
                            this.MS.Remove(m);
                            return true;
                        }
                    }
                }
                return false;
            });
            while (d()) { }
        }
    }

}
