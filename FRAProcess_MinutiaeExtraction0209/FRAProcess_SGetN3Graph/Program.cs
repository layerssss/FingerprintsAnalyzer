using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SGetN3Graph
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SGetN3GraphProcess(), args);
        }
    }
    class FRAProcess_SGetN3GraphProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("细化结果");
        }
        BinaryGraph N3;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("三邻点图", this.N3);
        }

        public override void Procedure()
        {
            this.N3 = new BinaryGraph();
            this.N3.Allocate(this.BG, 1);
            this.BG.FindAny((ti, tj, tv) =>
            {
                if (tv == 0)
                {
                    return false;
                }
                var count = 0;
                Drawing2.FourConnectionArea((deltai, deltaj, k) =>
                {
                    var i = ti + deltai;
                    var j = tj + deltaj;
                    if (this.BG.IsInbound(i, j))
                    {
                        count += 1 - this.BG.Value[i][j];
                    }
                });
                if (count >= 3)
                {
                    this.N3.Value[ti][tj] = 0;
                }
                return false;
            });
        }
    }

}
