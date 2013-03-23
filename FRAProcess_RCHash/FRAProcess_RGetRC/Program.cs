using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_RGetRC
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_RGetRCProcess(), args);
        }
    }
    class FRAProcess_RGetRCProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }

        MinutiaeSet MS;
        BinaryGraph tBG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MS = inputter.GetArg<MinutiaeSet>("过滤后的细节点集");
            this.tBG = inputter.GetArg<BinaryGraph>("完全细化结果");
        }
        LSArray sArr = new LSArray();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("RC矩阵", this.sArr);
        }

        public override void Procedure()
        {
            this.sArr.Allocate(this.MS.Count,this.MS.Count);
            this.sArr.ForEach((ti, tj) =>
            {
                var ls = new LocalStructure();
                var df = new DoubleFloat() { Value = 0 };
                ls.Add("RC", df);
                ls.PrimaryData = df;
                var pj = this.MS[tj];
                var pi = this.MS[ti];
                ls.Add("PI", pi);
                ls.Add("PJ", pj);

                bool cancled;
                var rc = pi.Location.RidgeCountDDA(this.tBG, 4, pj.Location, (tti, ttj) => false, out cancled, 4);
                df.Value = rc;
                this.sArr.ValueSet(ti, tj, ls);
            });
        }
    }

}
