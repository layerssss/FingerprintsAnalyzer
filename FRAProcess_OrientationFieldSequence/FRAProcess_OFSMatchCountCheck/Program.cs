using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFSMatchCountCheck
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFSMatchCountCheckProcess(), args);
        }
    }
    class FRAProcess_OFSMatchCountCheckProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        Integer mcountMin;
        Integer mcount;
        DoubleFloat mscore;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.mcountMin = inputter.GetArg<Integer>("最小匹配量");
            this.mcount = inputter.GetArg<Integer>("匹配量");
            this.mscore = inputter.GetArg<DoubleFloat>("匹配分数");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("最终匹配分数", mscore);
        }

        public override void Procedure()
        {
            if (mcount.Value < mcountMin.Value)
            {
                mscore.Value = 50000;
            }
        }
    }

}
