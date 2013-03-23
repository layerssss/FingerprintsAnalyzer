using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
using FRAProcess_FourierTrans;
namespace FRAProcess_IFourierTrans
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_IFourierTransProcess(), args);
        }
    }
    class FRAProcess_IFourierTransProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        ComplexFilter domainimg;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {

            this.domainimg = inputter.GetArg<ComplexFilter>("频域图");
        }

        ComplexFilter img;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("还原后图像", this.img);
        }

        public override void Procedure()
        {
            this.img = new ComplexFilter();
            this.img.Allocate(FRAProcess_FourierTransProcess.PadBit(this.domainimg.Width), FRAProcess_FourierTransProcess.PadBit(this.domainimg.Height), null);
            this.domainimg.ForEach((ti, tj, tv) =>
            {
                
                this.img.Value[ti][tj] = tv;
            });
            for (int i = 0; i < this.img.Height; i++)
            {
                IFFTline(this.img.Value[i]);
            }
            var tmp = new ComplexFilter();
            tmp.Allocate(this.img.Height, this.img.Width, null);
            this.img.ForEach((ti, tj, tv) =>
            {
                tmp.Value[tj][ti] = tv;
            });
            for (int i = 0; i < tmp.Height; i++)
            {
                IFFTline(tmp.Value[i]);
            } 
            this.img.ForEach((ti, tj) =>
            {
                img.Value[ti][tj] = tmp.Value[tj][ti];
            });
            
        }

        private void IFFTline(Complex[] p)
        {
            FRAProcess_FourierTransProcess.FFTline(p);
            for (int i = 0; i < p.Length; i++)
            {
                p[i] *= p.Length;
            }
        }
    }

}
