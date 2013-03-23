using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_RGetSupportArray
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_RGetSupportArrayProcess(), args);
        }
    }
    class FRAProcess_RGetSupportArrayProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        
        LSArray RC;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.RC = inputter.GetArg<LSArray>("RC矩阵");
        }
        LSArray sArr = new LSArray();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("RC支持矩阵", this.sArr);
        }

        public override void Procedure()
        {
            this.sArr.Allocate(this.RC);
            this.RC.ForEach((ti, tj,tls) =>
            {
                if (ti == tj)
                {
                    return;
                }
                var ls = tls;
                var df = new DoubleFloat() { Value = 0 };
                ls.Add("SUPPORT",df);
                ls.PrimaryData=df;
                for (int tk = 0; tk < this.RC.Width; tk++)
                {
                    if (tk == ti || tk == tj)
                    {
                        continue;
                    }
                    var ij = (this.RC.Value[ti][tj].PrimaryData as DoubleFloat).Value;
                    var kj = (this.RC.Value[tk][tj].PrimaryData as DoubleFloat).Value;
                    var ki = (this.RC.Value[ti][tk].PrimaryData as DoubleFloat).Value;
                    if (Math.Abs(ki - kj) == ij)
                    {
                        df.Value++;
                    }
                }

                this.sArr.ValueSet(ti, tj, ls);
            });
        }
    }

}
