using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
using System.Drawing;
namespace FRAProcess_TRidgeCountDDATest
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_TRidgeCountDDATestProcess(), args);
        }
    }
    class FRAProcess_TRidgeCountDDATestProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.BG = inputter.GetArg<BinaryGraph>("细化结果");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
        }

        public override void Procedure()
        {
            var first = new PointLocation(0,0);
            var second = new PointLocation(0, 0);
            var minInteval = new Integer() { Value = 5 };
            var tinyIntevalT = new Integer() { Value = 3 };
            while (true)
            {
                GrayLevelImage img = new GrayLevelImage();
                img.Allocate(this.BG);
                this.BG.FindAny((ti, tj, tv) =>
                {
                    img.Value[ti][tj] = tv;
                    return false;
                });
                if (!first.Present(img, "第一个点"))
                {
                    break;
                }
                Bitmap bmp = new Bitmap(img.Width, img.Height);
                img.DrawImage(bmp);
                first.Draw(bmp, Color.Red);
                img = GrayLevelImage.FromBitmap(bmp);
                if (!second.Present(img, "第二个点"))
                {
                    break;
                }
                if (!minInteval.Present(null, "最小间隔"))
                {
                    break;
                }
                if (!tinyIntevalT.Present(null, "tinyIntevalT"))
                {
                    break;
                }
                bool cancled;
                var rc=first.RidgeCountDDA(this.BG,minInteval.Value,second,(newI,newJ)=>{
                    return !this.SA.IsInArea(newI,newJ);
                }, out cancled, tinyIntevalT.Value);
                second.Draw(bmp, Color.Black);
                img = GrayLevelImage.FromBitmap(bmp);
                if (!img.Present(null, "结果：" + string.Format("ridgeCount={0},cancled={1};", rc, cancled)))
                {
                }
            }
        }
    }

}
