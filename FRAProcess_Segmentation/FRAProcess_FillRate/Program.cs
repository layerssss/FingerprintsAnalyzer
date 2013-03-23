using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_FillRate
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_FillRateProcess(), args);
        }
    }
    class FRAProcess_FillRateProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntergerGraph img;
        Integer valueToFill;
        Integer w;
        Integer step;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<IntergerGraph>("整数图");
            this.valueToFill = inputter.GetArg<Integer>("要填充的整数");
            this.w = inputter.GetArg<Integer>("W");
            this.step = inputter.GetArg<Integer>("坐标步进");
        }
        GrayLevelImage fRate;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<GrayLevelImage>("填充率", this.fRate);
        }

        public override void Procedure()
        {
            fRate = new GrayLevelImage();
            fRate.Allocate(img);
            BinaryGraph tmp = new BinaryGraph();
            tmp.Allocate(img, 0);
            img.FindAny((ti, tj, tv) =>
            {
                int ri = ti % this.step.Value;
                int rj = tj % this.step.Value;
                if ( ri!= 0 || rj  != 0) {
                    this.fRate.Value[ti][tj] = this.fRate.Value[ti - ri][tj - rj];
                    return false;
                }
                Window win = Window.GetSquare(ti, tj, this.w.Value, img);
                win.ForEach((tii, tjj) =>
                {
                    tmp.Value[tii][tjj] = img.Value[tii][tjj] == this.valueToFill.Value ? 1 : 0;
                });
                int fCount = tmp.Fill(ti, tj, win.IMin, win.JMin, win.IMax, win.JMax, 1, 0);
                this.fRate.Value[ti][tj] = (double)fCount / win.GetSize();
                return false;
            });
        }
    }

}
