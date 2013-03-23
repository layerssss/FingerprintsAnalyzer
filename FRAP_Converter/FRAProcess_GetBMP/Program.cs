using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;

namespace FRAProcess_GetBMP
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new p(), args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        BitmapFile bmp = new BitmapFile();
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            img = inputter.GetArg<GrayLevelImage>("灰度图");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<BitmapFile>("预览", bmp);
        }

        public override void Procedure()
        {
            bmp.BitmapObject = new System.Drawing.Bitmap(img.Width, img.Height);
            img.DrawImage(bmp.BitmapObject);
        }
    }
}
