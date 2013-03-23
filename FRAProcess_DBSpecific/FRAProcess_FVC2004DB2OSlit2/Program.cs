using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_FVC2004DB2OSlit2
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_FVC2004DB2OSlit2Process(), args);
        }
    }
    class FRAProcess_FVC2004DB2OSlit2Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("原突出边缘");
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("突出的边缘", BG);
        }

        public override void Procedure()
        {
            for (int i = 0; i < img.Height; i++)
            {
                int j = 0;
                while (img.Value[i][j] == 1)
                {
                    j++;
                }
                for (; j >= 0; j--)
                {
                    BG.Value[i][j] = i % 2;
                }
                j = img.Width - 1;
                while (img.Value[i][j] == 1)
                {
                    j--;
                }
                for (; j < img.Width; j++)
                {
                    BG.Value[i][j] = i % 2;
                }
            }
            for (int j = 0; j < BG.Width; j++)
            {
                BG.Value[0][j] = j % 2;
                BG.Value[BG.Height - 1][j] = j % 2;
            }
        }
    }

}
