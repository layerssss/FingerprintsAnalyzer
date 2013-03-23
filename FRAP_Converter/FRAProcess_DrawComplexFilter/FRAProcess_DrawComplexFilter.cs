using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_DrawComplexFilter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DrawComplexFilterProcess(), args);
        }
    }
    class FRAProcess_DrawComplexFilterProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        ComplexFilter filter;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.filter = inputter.GetArg<ComplexFilter>("滤波器");
        }
        GrayLevelImage img;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("滤波器转换结果", this.img);
        }

        public override void Procedure()
        {
            this.img = new GrayLevelImage();
            this.img.Allocate(this.filter);
            this.filter.ForEach((ti, tj, tv) =>
            {
                this.img.Value[ti][tj] = tv.R;
            });
        }
    }

}
