using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetHist
{
    class FRAProcess_GetHist
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetHistProcess(), args);
        }
    }
    class FRAProcess_GetHistProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原灰度图");
        }
        IntegerArray hist;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("直方图", this.hist);
        }

        public override void Procedure()
        {
            var count = new int[255];

            this.img.ForEach((ti, tj, tv) =>
            {
                var gray = (byte)((tv * 254));
                try
                {
                    count[gray]++;
                }
                catch { }
            });
            this.hist = new IntegerArray();
            this.hist.AddRange(count);
        }
    }

}
