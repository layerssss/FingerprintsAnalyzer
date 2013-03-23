using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_EdgeGraphPadding
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_EdgeGraphPaddingProcess(), args);
        }
    }
    class FRAProcess_EdgeGraphPaddingProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("原突出的边缘");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("突出的边缘", BG);
        }

        public override void Procedure()
        {
            for (int i = 0; i < BG.Height; i++)
            {
                this.BG.Value[i][0] = i % 2;
                this.BG.Value[i][BG.Width - 1] = i % 2;
            }
            for (int j = 0; j < BG.Width; j++)
            {
                this.BG.Value[0][j] = j % 2;
                this.BG.Value[BG.Height - 1][j] = j % 2;
            }
        }
    }

}
