using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DrawBinaryGraph
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
        BinaryGraph b;
        BitmapFile bmp;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            b = inputter.GetArg<BinaryGraph>("二值图");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图",bmp);
        }

        public override void Procedure()
        {
            GrayLevelImage g = new GrayLevelImage();
            g.Allocate(b.Width, b.Height);
            for (int i = 0; i < g.Height; i++)
            {
                for (int j = 0; j < g.Width; j++)
                {
                    g.Value[i][j] = b.Value[i][j];
                }
            }
            this.bmp = new BitmapFile();
            bmp.BitmapObject = new System.Drawing.Bitmap(g.Width, g.Height);
            g.DrawImage(bmp.BitmapObject);
        }
    }
}
