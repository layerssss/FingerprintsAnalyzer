using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_Count
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CountProcess(), args);
        }
    }
    class FRAProcess_CountProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        Integer W;
        BinaryGraph BG;
        Integer T;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("二值图");
            this.W = inputter.GetArg<Integer>("W");
            this.T = inputter.GetArg<Integer>("样窗内阀值");
        }
        SegmentationArea SA;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", SA);
        }

        public override void Procedure()
        {
            SA = new SegmentationArea();
            SA.Allocate(BG,1);
            BG.FindAny((ti, tj) =>
            {
                Window win = Window.GetSquare(ti, tj, W.Value, BG);
                int sum = 0;
                win.ForEach(BG, tv =>
                {
                    sum += 1 - tv;
                    if (sum > T.Value)
                    {
                        return true;
                    }
                    return false;
                });
                if (sum > T.Value)
                {
                    SA.Value[ti][tj] = 0;
                }
                return false;
            });
        }
    }

}
