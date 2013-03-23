using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DrawPointLocationSet
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DrawPointLocationSetProcess(), args);
        }
    }
    class FRAProcess_DrawPointLocationSetProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet pls;
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.pls = inputter.GetArg<PointLocationSet>("点集");
            this.img = inputter.GetArg<GrayLevelImage>("背景图");
        }
        BitmapFile bmp;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("预览", bmp);
        }

        public override void Procedure()
        {
            bmp = new BitmapFile();
            bmp.BitmapObject = new System.Drawing.Bitmap(img.Width, img.Height);
            img.DrawImage(bmp.BitmapObject);
            foreach (PointLocation pl in this.pls)
            {
                pl.Draw(bmp.BitmapObject, System.Drawing.Color.Red);
            }
        }
    }

}
