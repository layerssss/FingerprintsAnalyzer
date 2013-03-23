using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BUnfatBalanced
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BUnfatBalancedProcess(), args);
        }
    }
    class FRAProcess_BUnfatBalancedProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("原二值图");
        }
        BinaryGraph newBG;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", newBG);
        }

        public override void Procedure()
        {
            bool changed;
            do
            {
                changed = false;
                newBG = BG.Clone();
                BG.FindAny((ti, tj) =>
                {
                    if (BG.Value[ti][tj] == 1)
                    {
                        for (int k = 0; k < Drawing2.FourConnectionAreaI.Length; k++)
                        {
                            int ii = ti + Drawing2.FourConnectionAreaI[k];
                            int jj = tj + Drawing2.FourConnectionAreaJ[k];
                            if (ii < 0 || ii >= BG.Height || jj < 0 || jj >= BG.Width)
                            {
                                continue;
                            }
                            if (BG.Value[ii][jj] != 0)
                            {
                                continue;
                            }
                            bool canfill = true;
                            for (int l = 0; l < Drawing2.FourConnectionAreaI.Length; l++)
                            {
                                int li = Drawing2.FourConnectionAreaI[l];
                                int lj = Drawing2.FourConnectionAreaJ[l];
                                if (ii + li < 0 || ii + li >= BG.Height || jj + lj < 0 || jj + lj >= BG.Width)
                                {
                                    continue;
                                }
                                if (ti + li < 0 || ti + li >= BG.Height || tj + lj < 0 || tj + lj >= BG.Width)
                                {
                                    continue;
                                }
                                if (BG.Value[ti + li][tj + lj] == 0)
                                {
                                    if (BG.Value[ii + li][jj + lj] != 0)
                                    {
                                        canfill = false;
                                    }
                                }
                            }
                            if (canfill)
                            {
                                BG.Value[ii][jj] = 2;
                                newBG.Value[ii][jj] = 1;
                                changed = true;
                            }
                        }
                    }
                    return false;
                });
                BG = new BinaryGraph(newBG.Clone());
            }
            while (changed);
        }
    }

}
