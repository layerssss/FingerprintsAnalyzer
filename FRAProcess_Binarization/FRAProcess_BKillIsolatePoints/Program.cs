using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BKillIsolatePoints
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BKillIsolatePointsProcess(), args);
        }
    }
    class FRAProcess_BKillIsolatePointsProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        Integer IPSize;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            BG = inputter.GetArg<BinaryGraph>("原二值图");
            IPSize = inputter.GetArg<Integer>("孤立点大小");
        }
        
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", BG);
        }

        public override void Procedure()
        {
            BinaryGraph tmp = new BinaryGraph(BG.Clone());
            tmp.FindAny((ti, tj, tv) =>
            {
                if (tv == 1)
                {
                    int count = tmp.Fill(ti, tj, 0, 0, tmp.Height, tmp.Width, 1, 0);
                    if (count < this.IPSize.Value)
                    {
                        BG.Fill(ti, tj, 0, 0, tmp.Height, tmp.Width, 1, 0);
                    }
                }
                return false;
            });
            BinaryGraph tmp2 = new BinaryGraph(BG.Clone());
            tmp2.FindAny((ti, tj, tv) =>
            {
                if (tv == 0)
                {
                    int count = tmp2.Fill(ti, tj, 0, 0, tmp.Height, tmp.Width, 0, 1);
                    if (count < this.IPSize.Value)
                    {
                        BG.Fill(ti, tj, 0, 0, tmp.Height, tmp.Width, 0, 1);
                    }
                }
                return false;
            });
        }
    }

}
