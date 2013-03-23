using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SNegOnPTLS
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SNegOnPTLSProcess(), args);
        }
    }
    class FRAProcess_SNegOnPTLSProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        SegmentationArea SA;
        Integer REWeight;
        Integer BIWeight;
        PointLocationSet REs;
        PointLocationSet BIs;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            this.REWeight = inputter.GetArg<Integer>("RE权值");
            this.BIWeight = inputter.GetArg<Integer>("BI权值");
            this.REs = inputter.GetArg<PointLocationSet>("REs");
            this.BIs = inputter.GetArg<PointLocationSet>("BIs");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("带负值切割区域", this.SA);
        }

        public override void Procedure()
        {
            foreach (var ptl in this.REs)
            {
                this.SA.Value[ptl.I][ptl.J] = -this.REWeight.Value;
            }
            foreach (var ptl in this.BIs)
            {
                this.SA.Value[ptl.I][ptl.J] = -this.BIWeight.Value;
            }
        }
    }

}
