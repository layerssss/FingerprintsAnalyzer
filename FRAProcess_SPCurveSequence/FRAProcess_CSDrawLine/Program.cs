using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSDrawLine
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSDrawLineProcess(), args);
        }
    }
    class FRAProcess_CSDrawLineProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage CURVE;
        PointLocation baseCurve;
        DoubleFloat theta;
        DoubleFloatArray refSeq;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.refSeq = inputter.GetArg<DoubleFloatArray>("CURVE强度样本");
            this.CURVE = inputter.GetArg<GrayLevelImage>("CURVE强度图");
            this.baseCurve = inputter.GetArg<PointLocation>("CURVE强度样本基准点");
            this.theta = inputter.GetArg<DoubleFloat>("CURVE强度样本旋转度");
        }
        BitmapFile bmpCURVE;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("OptimalPosition", bmpCURVE);
        }

        public override void Procedure()
        {
            this.bmpCURVE = new BitmapFile() { BitmapObject = new System.Drawing.Bitmap(CURVE.Width, CURVE.Height) };
            this.CURVE.DrawImage(this.bmpCURVE.BitmapObject);
            for (int l = 0; l < refSeq.Count; l++)
            {
                int ii = baseCurve.I - (int)Math.Round(Math.Sin(theta.Value) * l);
                int jj = baseCurve.J + (int)Math.Round(Math.Cos(theta.Value) * l);
                this.bmpCURVE.BitmapObject.SetPixel(jj, ii, System.Drawing.Color.Red);
            }

        }
    }

}
