using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_ExtendImage
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_ExtendImageProcess(), args);
        }
    }
    class FRAProcess_ExtendImageProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        Integer ExtW;
        Integer ExtH;
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            ExtH = inputter.GetArg<Integer>("上下边距");
            ExtW = inputter.GetArg<Integer>("左右边距");
            img = inputter.GetArg<GrayLevelImage>("原图像");
        }
        GrayLevelImage newImg;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("处理后图像", newImg);
        }

        public override void Procedure()
        {
            newImg = new GrayLevelImage();
            newImg.Allocate(img.Width + this.ExtW.Value * 2, img.Height + this.ExtH.Value * 2, 1);
            img.FindAny((ti, tj, tv) =>
            {
                newImg.Value[ti + this.ExtH.Value][tj + this.ExtW.Value] = tv;
                return false;
            });
            for (int i = 0; i < this.img.Height; i++)
            {
                for (int j = 0; j < this.ExtW.Value; j++)
                {
                    newImg.Value[this.ExtH.Value + i][j] = this.img.Value[i][0];
                    newImg.Value[this.ExtH.Value + i][this.ExtW.Value + this.img.Width + j] = this.img.Value[i][this.img.Width - 1];
                }
            }
            for (int j = 0; j < this.img.Width ; j++)
            {
                for (int i = 0; i < this.ExtH.Value; i++)
                {
                    newImg.Value[i][this.ExtW.Value + j] = this.img.Value[0][j];
                    newImg.Value[this.ExtH.Value + this.img.Height + i][this.ExtW.Value + j] = this.img.Value[this.img.Height - 1][j];
                }
            }
        }
    }

}
