using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_DrawSegmentationArea
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DrawSegmentationAreaProcess(), args);
        }
    }
    class FRAProcess_DrawSegmentationAreaProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        SegmentationArea sa;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            sa=inputter.GetArg<SegmentationArea>("切割区域");
            img = inputter.GetArg<GrayLevelImage>("原图像");
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
            this.sa.FindAny((ti, tj, tv) =>
            {
                if (tv == 0)
                {
                    bmp.BitmapObject.SetPixel(tj, ti, System.Drawing.Color.Blue);
                }
                return false;
            });
        }
    }

}
