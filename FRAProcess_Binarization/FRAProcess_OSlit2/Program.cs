using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OSlit2
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OSlit2Process(), args);
        }
    }
    class FRAProcess_OSlit2Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OF;
        GrayLevelImage img;
        StabSet binarizationSlit;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OF = inputter.GetArg<OrientationGraph>("走向图");
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.binarizationSlit = inputter.GetArg<StabSet>("二值化均匀针刺");
        }
        BinaryGraph BG;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("OS2二值图", BG);
        }
        public override void Procedure()
        {
            BG = new BinaryGraph();
            BG.Allocate(img.Width, img.Height, 1);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    int stabNum=(int)(this.OF.Value[i][j]*(this.binarizationSlit.Count-1)/Math.PI/2);
                    int stabNumO=(stabNum+(this.binarizationSlit.Count-1)/4)%((this.binarizationSlit.Count-1)/2);
                    double sumOF = 0;
                    int countOF = 0;
                    double sumOOF = 0;
                    int countOOF = 0;
                    this.binarizationSlit.IsOpen(
                        i,
                        j,
                        stabNum,
                        BG,
                        (ti, tj) =>
                        {
                            sumOF += this.img.Value[ti][tj];
                            countOF++;
                        },
                        (() => false));
                    this.binarizationSlit.IsOpen(
                        i,
                        j,
                        stabNum+(this.binarizationSlit.Count-1)/2,
                        BG,
                        (ti, tj) =>
                        {
                            sumOF += this.img.Value[ti][tj];
                            countOF++;
                        },
                        (() => false));
                    this.binarizationSlit.IsOpen(
                        i,
                        j,
                        stabNumO,
                        BG,
                        (ti, tj) =>
                        {
                            sumOOF += this.img.Value[ti][tj];
                            countOOF++;
                        },
                        (() => false));
                    this.binarizationSlit.IsOpen(
                        i,
                        j,
                        stabNumO + (this.binarizationSlit.Count-1) / 2,
                        BG,
                        (ti, tj) =>
                        {
                            sumOOF += this.img.Value[ti][tj];
                            countOOF++;
                        },
                        (() => false));
                    sumOF /= countOF;
                    sumOOF /= countOOF;
                    if (sumOOF > sumOF)
                    {
                        BG.Value[i][j] = 0;
                    }
                    else
                    {
                        BG.Value[i][j] = 1;
                    }
                }
            }
        }
    }

}
