using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_EErosion
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_EErosionProcess(), args);
        }
    }
    class FRAProcess_EErosionProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }

        BinaryGraph BG;
        Integer iiiiiii;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("原二值图");
            this.iiiiiii = inputter.GetArg<Integer>("腐蚀次数");
        }

        BinaryGraph newBG;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", newBG);
        }

        public override void Procedure()
        {
            for(int iiii=0;iiii<iiiiiii.Value;iiii++)
            {
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
                                {
                                    BG.Value[ii][jj] = 2;
                                    newBG.Value[ii][jj] = 1;
                                }
                            }
                        }
                    }
                }
                BG = new BinaryGraph(newBG.Clone());
            }
        }
    }

}
