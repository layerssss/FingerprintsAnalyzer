using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SPCurveTestGetCurve
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPCurveTestGetCurveProcess(), args);
        }
    }
    class FRAProcess_SPCurveTestGetCurveProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        Integer W;
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("走向图");
            this.W = inputter.GetArg<Integer>("W");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
        }
        GrayLevelImage Curve;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CURVE强度", Curve);
        }

        public override void Procedure()
        {
            Curve = new GrayLevelImage();
            Curve.Allocate(OG, 0);
            Window win = Window.GetSquare(1, 1, Math.Max(Curve.Width, Curve.Height), Curve);
            //for (int i = 0; i < 1; i++)
            //{
                //FRAProcess_SPCurve. RefineCoreCenter(win);
            //    win = Window.GetSquare(MAXIMA.I, MAXIMA.J, win.Width / 4, OG);
            //}
                uint a = uint.MaxValue - 10;
                uint b = uint.MaxValue - 10;
                uint c = a + b;
            Console.WriteLine(string.Format("{0};{1};{2}",a,b,c));

        }
        
    }

}
