using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MergePLSets
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MergePLSetsProcess(), args);
        }
    }
    class FRAProcess_MergePLSetsProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet pls1;
        PointLocationSet pls2;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            pls1 = inputter.GetArg<PointLocationSet>("点坐标集1");
            pls2 = inputter.GetArg<PointLocationSet>("点坐标集2");
        }
        PointLocationSet npls;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("结果点坐标集", npls);
        }

        public override void Procedure()
        {
            npls = new PointLocationSet();
            npls.AddRange(pls1.Union(pls2));
        }
    }
}
