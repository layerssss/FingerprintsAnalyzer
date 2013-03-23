using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPCurveTest2
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPCurveTest2Process(), args);
        }
    }
    class FRAProcess_SPCurveTest2Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        Integer W;
        SegmentationArea SA;
        GrayLevelImage img;
        //Window win;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("走向图");
            this.W = inputter.GetArg<Integer>("W");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            //this.win = inputter.GetArg<Window>("计算样窗");
        }
        GrayLevelImage CORE;
        PointLocation MAXIMA;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            //outputter.PutArg("CORE强度", CORE);
            //outputter.PutArg("最大CORE强度点", MAXIMA);
        }

        public override void Procedure()
        {
            Window win = new Window();
            win = (Window)win.BuildInstance();
            this.MAXIMA = new PointLocation(0,0);
            while (win.Present(img, "win"))
            {
                CORE = new GrayLevelImage();
                CORE.Allocate(OG, 0);
                for (int i = 0; i < 1; i++)
                {
                    double maxCore = 0;
                    double bestWeightI = 0.9;
                    double bestWeightJ = 0.9;
                    FRAProcess_SPCurve.FRAProcess_SPCurveProcess.RefineCoreCenter(this.OG,this.W,this.CORE,this.MAXIMA,this.SA,win, out maxCore, bestWeightI, bestWeightJ);
                    win = Window.GetSquare(MAXIMA.I, MAXIMA.J, 80, OG);
                }

                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(img.Width, img.Height);
                img.DrawImage(bmp);
                MAXIMA.Draw(bmp, System.Drawing.Color.Red);
                GrayLevelImage view = GrayLevelImage.FromBitmap(bmp);
                CORE.Present(view, string.Format("CORE i={0} j={1}",MAXIMA.I,MAXIMA.J));

            }

        }
        
    }

}
