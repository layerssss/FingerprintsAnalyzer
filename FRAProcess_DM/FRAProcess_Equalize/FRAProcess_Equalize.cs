using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_Equalize
{
    class FRAProcess_Equalize
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_EqualizeProcess(), args);
        }
    }
    class FRAProcess_EqualizeProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原灰度图");
        }
        GrayLevelImage nimg = new GrayLevelImage();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("新灰度图", nimg);
        }

        public override void Procedure()
        {
            var count = new int[255];
            var hist = new double[255];
            var acum = new double[255];
            this.nimg.Allocate(this.img);
            this.img.ForEach((ti, tj, tv) =>
            {
                var gray = (byte)((tv * 254));
                count[gray]++;
            });
            for (int i = 0; i < 255; i++)
            {
                hist[i] = (double)count[i] / this.img.Width / this.img.Height;
            }
            var acumLast = 0.0;
            for (int i = 0; i < 255; i++)
            {
                acumLast += hist[i];
                acum[i] = acumLast;
            }
            this.img.ForEach((ti, tj, tv) =>
            {
                var gray = (byte)((tv * 254));
                this.nimg.ValueSet(ti, tj, acum[gray]);
            });
        }
    }

}
