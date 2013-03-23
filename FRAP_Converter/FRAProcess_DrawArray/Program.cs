using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_DrawArray
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
        BitmapFile bmp;
        DoubleFloatArray arr;
        Integer w;
        Integer h;
        DoubleFloat yMin;
        DoubleFloat yMax;
        BoolData connectPoints;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.arr = inputter.GetArg<DoubleFloatArray>("数据");
            this.w = inputter.GetArg<Integer>("图表宽度");
            this.h = inputter.GetArg<Integer>("图表高度");
            this.yMin = inputter.GetArg<DoubleFloat>("纵轴最小值代表的数据");
            this.yMax = inputter.GetArg<DoubleFloat>("纵轴最大值代表的数据");
            this.connectPoints = inputter.GetArg<BoolData>("是否用线把点连起来");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<BitmapFile>("图表", bmp);
        }

        public override void Procedure()
        {
            bmp = new BitmapFile();
            bmp.BitmapObject = new System.Drawing.Bitmap(w.Value, h.Value);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp.BitmapObject);
            g.FillRectangle(System.Drawing.Brushes.White, 0, 0, bmp.BitmapObject.Width, bmp.BitmapObject.Height);
            arr.DrawFigure(bmp.BitmapObject, new System.Drawing.Pen(System.Drawing.Color.Red, 3), yMin.Value, yMax.Value, this.connectPoints.Value);
        }
    }
}
