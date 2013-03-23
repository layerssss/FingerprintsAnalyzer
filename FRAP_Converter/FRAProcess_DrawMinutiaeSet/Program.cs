using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_DrawMinutiaeSet
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_DrawMinutiaeSetProcess(), args);
        }
    }
    class FRAProcess_DrawMinutiaeSetProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        MinutiaeSet MS;
        GrayLevelImage img;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MS = inputter.GetArg<MinutiaeSet>("细节点集");
            this.img = inputter.GetArg<GrayLevelImage>("背景图");
        }
        BitmapFile bmp;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点集", this.bmp);
        }

        public override void Procedure()
        {
            this.bmp = new BitmapFile();
            this.bmp.BitmapObject = new System.Drawing.Bitmap(this.img.Width, this.img.Height);
            this.MS.Draw(this.img);
            this.img.DrawImage(this.bmp.BitmapObject);
        }
    }

}
