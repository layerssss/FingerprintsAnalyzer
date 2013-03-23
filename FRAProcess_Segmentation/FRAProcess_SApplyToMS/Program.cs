using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SApplyToMS
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SApplyToMSProcess(), args);
        }
    }
    class FRAProcess_SApplyToMSProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        MinutiaeSet MS;
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MS = inputter.GetArg<MinutiaeSet>("细节点集");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");

        }
        MinutiaeSet newMS;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("过滤后的细节点集", this.newMS);
        }

        public override void Procedure()
        {
            this.newMS = new MinutiaeSet();
            foreach (Minutiae m in this.MS)
            {
                if (this.SA.IsInArea(m.Location.I, m.Location.J))
                {
                    this.newMS.Add(m);
                }
            }
        }
    }

}
