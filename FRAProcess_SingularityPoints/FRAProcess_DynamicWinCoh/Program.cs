using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DynamicWinCoh
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DynamicWinCohProcess(), args);
        }
    }
    class FRAProcess_DynamicWinCohProcess : FRAProcess.FRAProcess
    {
        IntergerGraph wGraph;
        GrayLevelImage coh;
        Integer factor;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.coh = inputter.GetArg<GrayLevelImage>("灰度图");
            this.factor = inputter.GetArg<Integer>("乘数");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("窗口大小图", this.wGraph);
        }

        public override void Procedure()
        {
            this.wGraph = new IntergerGraph();
            this.wGraph.Allocate(coh.Width, coh.Height);
            for (int i = 0; i < coh.Height; i++)
            {
                for (int j = 0; j < coh.Width; j++)
                {
                    this.wGraph.Value[i][j] = (int)((1-this.coh.Value[i][j]) * factor.Value);
                    if (this.wGraph.Value[i][j] < 1)
                    {
                        this.wGraph.Value[i][j] = 1;
                    }
                }
            }
        }
    }

}
