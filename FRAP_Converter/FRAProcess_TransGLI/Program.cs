using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_TransGLI
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_TransGLIProcess(), args);
        }
    }
    class FRAProcess_TransGLIProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage GLI;
        DoubleFloat factor;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.GLI = inputter.GetArg<GrayLevelImage>("灰度图");
            this.factor = inputter.GetArg<DoubleFloat>("淡化因子");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("处理后灰度图", this.GLI);
        }

        public override void Procedure()
        {
            this.GLI.ForEach((ti, tj, tv) =>
            {
                this.GLI.ValueSet(ti, tj, 1-(1-tv) * this.factor.Value);
            });
        }
    }

}
