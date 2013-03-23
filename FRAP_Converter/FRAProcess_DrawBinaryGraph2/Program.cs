using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DrawBinaryGraph2
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DrawBinaryGraph2Process(), args);
        }
    }
    class FRAProcess_DrawBinaryGraph2Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph bg;
        DoubleFloat intensity;
        DoubleFloat bgintensity;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.bg = inputter.GetArg<BinaryGraph>("二值图");
            this.intensity = inputter.GetArg<DoubleFloat>("前景灰度值");
            this.bgintensity = inputter.GetArg<DoubleFloat>("背景灰度值");
        }
        GrayLevelImage gli;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", gli);

        }

        public override void Procedure()
        {
            this.gli = new GrayLevelImage();
            gli.Allocate(bg.Width, bg.Height);
            for (int i = 0; i < bg.Height; i++)
            {
                for (int j = 0; j < bg.Width; j++)
                {
                    this.gli.Value[i][j] = (bg.Value[i][j] == 1 ? bgintensity.Value : intensity.Value);
                }
            }
        }
    }

}
