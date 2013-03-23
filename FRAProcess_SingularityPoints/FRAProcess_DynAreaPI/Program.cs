using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DynAreaPI
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DynAreaPIProcess(), args);
        }
    }
    class FRAProcess_DynAreaPIProcess :FRAProcess_PoincareIndexesCal1.PI1{
        IntergerGraph wGraph;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            base.Input(inputter);
            this.wGraph = inputter.GetArg<IntergerGraph>("窗口宽度图");
        }
        public FRAProcess_DynAreaPIProcess():base()
        {
            
        }
        /// <summary>
        /// Processes the around.
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        public override void ProcessAround(int i, int j)
        {
            base.ProcessAround(i, j);
            this.AroundI.Clear();
            this.AroundJ.Clear();
            int w=this.wGraph.Value[i][j];
            for (int k = -w; k < w; k++)
            {
                this.AroundI.Add(-w);
                this.AroundJ.Add(k);
            }
            for (int k = -w; k < w; k++)
            {
                this.AroundI.Add(k);
                this.AroundJ.Add(w);
            }
            for (int k = -w; k < w; k++)
            {
                this.AroundI.Add(w);
                this.AroundJ.Add(-k);
            }

            for (int k = -w; k < w; k++)
            {
                this.AroundI.Add(-k);
                this.AroundJ.Add(-w);
            }
        }
    }

}
