using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_InfoRateSampling
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new p(), args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        GrayLevelImage img;
        Window window;
        DoubleFloat deltaT;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            img = inputter.GetArg<GrayLevelImage>("原图像");
            window = inputter.GetArg<Window>("样窗");
            deltaT = inputter.GetArg<DoubleFloat>("阀值步进");
        }
        DoubleFloatArray arr;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("IR样本", arr);
        }

        public override void Procedure()
        {
            arr = new DoubleFloatArray();
            arr.Allocate(0);
            int lastInfoCount = 0;
            for (double t = 0; t < 1; t += deltaT.Value)
            {
                int InfoCount = 0;
                for (int i = 0; i < img.Height; i++)
                {
                    for (int j = 0; j < img.Width; j++)
                    {
                        if (!window.ContainPosition(i, i))
                        {
                            continue;
                        }
                        if (img.Value[i][j] < t)
                        {
                            InfoCount++;
                        }
                    }
                }
                arr.Add(InfoCount - lastInfoCount);
                lastInfoCount = InfoCount;
            }
        }
    }
}
