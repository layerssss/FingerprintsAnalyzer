using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPSumCore
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPSumCoreProcess(), args);
        }
    }
    class FRAProcess_SPSumCoreProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage Core;
        Integer W;
        DoubleFloat T;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.Core = inputter.GetArg<GrayLevelImage>("CORE强度");
            this.W = inputter.GetArg<Integer>("W");
            this.T = inputter.GetArg<DoubleFloat>("样窗内CORE强度阀值");
        }
        BinaryGraph CORE;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CORE位置", CORE);
        }

        public override void Procedure()
        {
            this.CORE = new BinaryGraph();
            this.CORE.Allocate(this.Core, 0);
            this.Core.FindAny((ti, tj) =>
            {
                Window win = Window.GetSquare(ti, tj, W.Value, Core);
                double sumCore = 0;
                win.ForEach(Core, tv =>
                {
                    sumCore += tv;
                });
                if (sumCore > T.Value)
                {
                    this.CORE.Value[ti][tj] = 1;
                }
                return false;
            });
        }
    }

}
