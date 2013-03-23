using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BUnfat
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BUnfatProcess(), args);
        }
    }
    class FRAProcess_BUnfatProcess : FRAProcess.FRAProcess
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
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", BG);
        }

        public override void Procedure()
        {
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
                            if (BG.Value[ii][jj] == 1)
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
                                if (i + li < 0 || i + li >= BG.Height || j + lj < 0 || j + lj >= BG.Width)
                                {
                                    continue;
                                }
                                if (BG.Value[i + li][j + lj] == 0)
                                {
                                    if (BG.Value[ii + li][jj + lj] == 1)
                                    {
                                        canfill = false;
                                    }
                                }
                            }
                            if (canfill)
                            {
                                BG.Value[ii][jj] = 1;
                            }
                        }
                    }
                }
            }
        }
    }

}
