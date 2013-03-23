using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BUnfatBalanced4C
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BUnfatBalanced4CProcess(), args);
        }
    }
    class FRAProcess_BUnfatBalanced4CProcess : FRAProcess.FRAProcess
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
            outputter.PutArg("四连通二值图", newBG);
        }

        public override void Procedure()
        {
            bool changed;
            do
            {
                changed = false;
                newBG = new BinaryGraph(BG.Clone());
                for (int i = 0; i < BG.Height; i++)
                {
                    for (int j = 0; j < BG.Width; j++)
                    {
                        if (BG.Value[i][j] == 1)
                        {
                            for (int k = 0; k < Drawing2.FourConnectionAreaI.Length; k++)
                            {
                                int ii = i + Drawing2.FourConnectionAreaI[k];
                                int jj = j + Drawing2.FourConnectionAreaJ[k];
                                if (ii < 0 || ii >= BG.Height || jj < 0 || jj >= BG.Width)
                                {
                                    continue;
                                }
                                if (BG.Value[ii][jj] != 0)
                                {
                                    continue;
                                }
                                bool canfill = true;
                                for (int l = 0; l < Drawing2.EightConnectionAreaI.Length; l++)
                                {
                                    int li = Drawing2.EightConnectionAreaI[l];
                                    int lj = Drawing2.EightConnectionAreaJ[l];
                                    if (ii + li < 0 || ii + li >= BG.Height || jj + lj < 0 || jj + lj >= BG.Width)
                                    {
                                        continue;
                                    }
                                    if (i + li < 0 || i + li >= BG.Height || j + lj < 0 || j + lj >= BG.Width)
                                    {
                                        continue;
                                    }
                                    if (BG.Value[i + li][j + lj] == 0)//本来是黑的
                                    {
                                        if (BG.Value[ii + li][jj + lj] != 0)//填完后不黑了
                                        {
                                            canfill = false;//则不能填
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
                    }
                }
                BG = new BinaryGraph(newBG.Clone());
            }
            while (changed);
        }
    }

}
