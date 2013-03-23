using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPadding
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPaddingProcess(), args);
        }
    }
    class FRAProcess_SPaddingProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        SegmentationArea SA;
        Integer Padding;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            this.Padding = inputter.GetArg<Integer>("衬距");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", this.SA);
        }

        public override void Procedure()
        {
            for (var p = 0; p < this.Padding.Value; p++)
            {
                for (var i = 0; i < this.SA.Height; i++)
                {
                    this.SA.Value[i][p]
                        = this.SA.Value[i][this.SA.Width - 1 - p]
                        = 0;
                }
                for (var j = 0; j < this.SA.Width; j++)
                {
                    this.SA.Value[p][j]
                        = this.SA.Value[this.SA.Height - 1 - p][j]
                        = 0;
                }
            }
        }
    }

}
