using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_FourierTrans
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_FourierTransProcess(), args);
        }
    }
    public class FRAProcess_FourierTransProcess : FRAProcess.FRAProcess
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
        ComplexFilter domainimg;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("频域图", this.domainimg);
        }

        public override void Procedure()
        {
            this.domainimg = new ComplexFilter();
            this.domainimg.Allocate(PadBit(this.img.Width), PadBit(this.img.Height),null);
            this.img.ForEach((ti, tj, tv) =>
            {
                this.domainimg.Value[ti][tj] = new Complex(tv, 0);
            });
            for (int i = 0; i < this.domainimg.Height; i++)
            {
                FFTline(this.domainimg.Value[i]);
            }
            var tmp = new ComplexFilter();
            tmp.Allocate(this.domainimg.Height, this.domainimg.Width, null);
            this.domainimg.ForEach((ti, tj, tv) =>
            {
                tmp.Value[tj][ti] = tv;
            });
            for (int i = 0; i < tmp.Height; i++)
            {
                FFTline(tmp.Value[i]);
            }
            this.domainimg.ForEach((ti, tj) =>
            {
                domainimg.Value[ti][tj] = tmp.Value[tj][ti];
            });
            
        }
        public static void FFTline(Complex[] F)
        {
            var N = F.Length;
            int r = (int)(Math.Log(N + 1) / Math.Log(2));

            //进行倒位序运算
            BitReOrder(F, r);

            //计算Wr因子, 加权系数 
            Complex[] W = new Complex[N / 2];
            double angle;
            for (int i = 0; i < N / 2; i++)
            {
                angle = -i * Math.PI * 2 / N;

                W[i] = new Complex(Math.Cos(angle), Math.Sin(angle));
            }

            //采用蝶形算法进行快速傅立叶变换
            int DFTn;//第DFTn级
            int k;//分级有多少个蝶形运算
            int d;//蝶形运算的偏移
            int p;//index

            Complex X1, X2;
            for (DFTn = 0; DFTn < r; DFTn++)
            {//第几级蝶形运算
                for (k = 0; k < (1 << (r - DFTn - 1)); ++k)
                {//当前列所有的蝶形进行运算
                    p = 2 * k * (1 << DFTn);
                    for (d = 0; d < (1 << DFTn); ++d)
                    {//相邻蝶形运算
                        //p = k * (1 << (k + 1)) + d;
                        X1 = F[p + d];
                        X2 = F[p + d + (1 << DFTn)];
                        F[p + d] = X1 + X2 * W[d * (1 << (r - DFTn - 1))];
                        F[p + d + (1 << DFTn)] = X1 - X2 * W[d * (1 << (r - DFTn - 1))];
                    }
                }
            }
            for (int i = 0; i < N; ++i)
            {
                F[i] /= N;
            }
        }
        static void BitReOrder(Complex[] sequ, int r)
        {
            Complex dTemp;
            int x;
            for (int i = 0; i < (1 << r); ++i)
            {
                x = BitReverse(i, r);
                if (x > i)
                {
                    dTemp = sequ[i];
                    sequ[i] = sequ[x];
                    sequ[x] = dTemp;
                }
            }    
        }
        static int BitReverse(int src, int size)
        {
            int tmp = src;
            int des = 0;
            for (int i = size - 1; i >= 0; i--)
            {
                des = ((tmp & 0x1) << i) | des;
                tmp = tmp >> 1;
            }
            return des;
        }
        public static int PadBit(int src)
        {
            int i=1;
            while (i < src)
            {
                i <<= 1;
            }
            return i;
        }
    }

}
