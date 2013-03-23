using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BUnconstrainedMend
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BUnconstrainedMendProcess(), args);
        }
    }
    class FRAProcess_BUnconstrainedMendProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("不完全细化结果");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细化结果", this.BG);
        }

        public override void Procedure()
        {

            this.BG.FindAny((ti, tj, tv) =>
            {
                if (tv == 0)
                {
                    var count = 0;
                    if (ti == 0 || tj == 0 || ti == this.BG.Height - 1 || tj == this.BG.Width - 1)
                    {
                        return false;
                    }
                    Drawing2.FourConnectionArea((deltai, deltaj, k) =>
                    {
                        if (this.BG.Value[ti + deltai][tj + deltaj] == 0)
                        {
                            count++;
                        }
                    });
                    if (count == 3)
                    {
                        this.BG.Value[ti][tj] = 1;
                        return false;
                    }
                    if (count == 2)
                    {
                        var opsiteDeltai = 0;
                        var opsiteDeltaj = 0;
                        Drawing2.FourConnectionArea((deltai, deltaj, k) =>
                        {
                            if (this.BG.Value[ti + deltai][tj + deltaj] == 0)
                            {
                                if (deltai == 0)
                                {
                                    opsiteDeltaj = -deltaj;
                                }
                                else
                                {
                                    opsiteDeltai = -deltai;
                                }
                            }
                        });
                        if (this.BG.Value[ti + opsiteDeltai][tj + opsiteDeltaj] == 1)
                        {
                            this.BG.Value[ti][tj] = 1;
                            return false;
                        }
                    }
                }
                return false;
            });
        }
    }

}
