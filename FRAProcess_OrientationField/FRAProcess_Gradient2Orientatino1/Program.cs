using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRAProcess;
using FRADataStructs.DataStructs;

namespace FRAProcess_Gradient2Orientatino1
{
    class Program
    {
        static int Main(string[] args)
        {
            G2O1 g = new G2O1();
            return g.Execute(args);
        }
    }
    class G2O1 : FRAProcess.Processes.OrientationExtraction
    {
        public Integer W;
        public GrayLevelImage Coherence = new GrayLevelImage();
        public override void Procedure()
        {
            base.Procedure();
            Coherence.Allocate(InputImage.Width, InputImage.Height);
            for (int i = 0; i < InputImage.Height; i++)
            {
                for (int j = 0; j < InputImage.Width ; j++)
                {
                    double Gxy = 0;
                    double Gxx = 0;
                    double Gyy = 0;
                    Window win = Window.GetSquare(i, j, W.Value, InputImage);
                    win.ForEach((ti, tj) =>
                    {
                        if (ti == 0 || tj == 0||ti==InputImage.Height-1||tj==InputImage.Width-1)
                        {
                            return;
                        }
                        double dX = deltaX(ti, tj);
                        double dY = deltaY(ti, tj);
                        Gxy += dX * dY;
                        Gxx += dX * dX;
                        Gyy += dY * dY;
                    });
                    Orientation.Value[i][j] = Math.PI / 2 + 0.5 * Math.Atan2(Gxy * 2, Gxx - Gyy);
                    Coherence.Value[i][j] = Math.Sqrt((Gxx - Gyy) * (Gxx - Gyy) + 4 * Gxy * Gxy) / (Gxx + Gyy);
                }
            }
        }
        double deltaX(int i, int j)
        {
            return InputImage.Value[i][j + 1] - InputImage.Value[i][j - 1];
        }
        double deltaY(int i, int j)
        {
            return InputImage.Value[i - 1][j] - InputImage.Value[i + 1][j];
        }
        public override void Input(FRAProcessInputter inputter)
        {
            base.Input(inputter);
            this.W = inputter.GetArg<Integer>("滑动窗口大小的一半");
        }
        public override void Output(FRAProcessOutputter outputter)
        {
            base.Output(outputter);
            outputter.PutArg<GrayLevelImage>("连贯性", this.Coherence);

        }
    }
}
