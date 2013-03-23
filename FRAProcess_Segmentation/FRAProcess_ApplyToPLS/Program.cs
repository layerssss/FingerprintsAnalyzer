using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_ApplyToPLS
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_ApplyToPLSProcess(), args);
        }
    }
    class FRAProcess_ApplyToPLSProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet pls;
        SegmentationArea area;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.pls = inputter.GetArg<PointLocationSet>("点坐标集");
            this.area = inputter.GetArg<SegmentationArea>("切割区域");
        }
        PointLocationSet npls;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("新点坐标集", npls);
        }

        public override void Procedure()
        {
            npls = new PointLocationSet();
            foreach (PointLocation pl in pls)
            {
                if (area.IsInArea(pl.I, pl.J))
                {
                    npls.Add(pl);
                }
            }
        }
    }

}
