using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_TestCut
{
    class Program
    {
        static int Main(string[] args)
        {
            p p = new p();
            return p.Execute(args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        public GrayLevelImage Img;
        public Window Window;
        public DoubleFloat MaskIntensity;
        public override void Procedure()
        {
            for (int i = 0; i < Img.Height; i++)
            {
                for (int j = 0; j < Img.Width; j++)
                {
                    if (!Window.ContainPosition(i, j))
                    {
                        Img.Value[i][j] = Img.Value[i][j] * MaskIntensity.Value;
                    }
                }
            }
        }

        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            Img = inputter.GetArg<GrayLevelImage>("原图像");
            Window = inputter.GetArg<Window>("窗口");
            MaskIntensity = inputter.GetArg<DoubleFloat>("遮罩深度（越小越明显！）");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<GrayLevelImage>("遮罩结果",Img);
        }
    }
}
