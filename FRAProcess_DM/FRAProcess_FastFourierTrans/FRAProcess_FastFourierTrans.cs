using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_FastFourierTrans
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_FastFourierTransProcess(), args);
        }
    }
    class FRAProcess_FastFourierTransProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
        }
        Filter domainimg;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("频域图", this.domainimg);
        }

        public override void Procedure()
        {
            this.domainimg = new Filter();
            this.domainimg.Allocate(this.img);
            this.domainimg.ForEach((u, v) =>
            {
                var sum = 0.0;
                this.img.ForEach((ti, tj, tv) =>
                {
                    sum += tv * Math.Cos(Math2.PI(2 * ((double)u * ti / this.img.Height + (double)v * tj / this.img.Width)));
                });
                this.domainimg.Value[u][v] = sum / this.img.Width / this.img.Height;
                this.Progress(u, this.domainimg.Height);
            });
        }
    }

}
