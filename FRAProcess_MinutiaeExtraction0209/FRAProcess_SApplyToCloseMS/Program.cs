using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SApplyToCloseMS
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SApplyToCloseMSProcess(), args);
        }
    }
    class FRAProcess_SApplyToCloseMSProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        MinutiaeSet MS;
        SegmentationArea SA;
        Integer W;
        Integer T;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MS = inputter.GetArg<MinutiaeSet>("细节点集");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            this.W = inputter.GetArg<Integer>("W");
            this.T = inputter.GetArg<Integer>("T");
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
                var win = Window.GetSquare(m.Location.I, m.Location.J, this.W.Value);
                var count = 0;
                win.ForEach((ti,tj) =>
                {
                    try
                    {
                        count += 1 - this.SA.Value[ti][tj];
                    }
                    catch
                    {
                        count++;
                    }
                });
                if (count<this.T.Value)
                {
                    this.newMS.Add(m);
                }
            }
        }
    }

}
