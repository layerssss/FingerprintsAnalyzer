using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BSaveOutter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BSaveOutterProcess(), args);
        }
    }
    class FRAProcess_BSaveOutterProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            BG = inputter.GetArg<BinaryGraph>("二值图");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("新二值图", BG);
        }

        public override void Procedure()
        {
            BinaryGraph tmp = BG.Clone();
            int targetValue = tmp.Value[0][0];
            tmp.Fill(0, 0, 0, 0, tmp.Height, tmp.Width, targetValue, targetValue == 0 ? 1 : 0);
            tmp.Fill(0, tmp.Width - 1, 0, 0, tmp.Height, tmp.Width, targetValue, targetValue == 0 ? 1 : 0);
            BG.Accumulate((tov, tnv) => tov == 1 ^ tnv == 1 ? 1 : 0, tmp);
        }
    }

}
