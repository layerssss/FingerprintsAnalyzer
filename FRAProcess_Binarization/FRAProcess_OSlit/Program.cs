using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OSlit
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OSlitProcess(), args);
        }
    }
    class FRAProcess_OSlitProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OF;
        GrayLevelImage img;
        Integer slitLen;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OF = inputter.GetArg<OrientationGraph>("走向图");
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.slitLen = inputter.GetArg<Integer>("slit长度");
        }
        BinaryGraph BG;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", BG);
        }

        public override void Procedure()
        {
            BG = new BinaryGraph();
            BG.Allocate(img.Width, img.Height);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    try
                    {
                        double thetaOF = OF.Value[i][j];
                        double thetaOOF = (thetaOF + 0.5 * Math.PI) % Math.PI;
                        double sumOF = 0;
                        double sumOOF = 0;
                        for (int k = 1; k < this.slitLen.Value; k++)
                        {
                            sumOF += img.Value
                                [i - (int)(Math.Sin(thetaOF) * k)]
                                [j + (int)(Math.Cos(thetaOF) * k)];
                            sumOOF += img.Value
                                [i - (int)(Math.Sin(thetaOOF) * k)]
                                [j + (int)(Math.Cos(thetaOOF) * k)];
                        }
                        if (sumOOF > sumOF)
                        {
                            BG.Value[i][j] = 0;
                        }
                        else
                        {
                            BG.Value[i][j] = 1;
                        }
                    }
                    catch
                    {
                        BG.Value[i][j] = 1;
                    }
                }
            }
        }
    }

}
