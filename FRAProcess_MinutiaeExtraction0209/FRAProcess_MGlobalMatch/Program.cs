using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MGlobalMatch
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MGlobalMatchProcess(), args);
        }
    }
    class FRAProcess_MGlobalMatchProcess : FRAProcess.FRAProcess
    {
        FRAProcess_MLocal.FRAProcess_MLocalProcess local = new FRAProcess_MLocal.FRAProcess_MLocalProcess();
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        LocalStructureSet lss1;
        LocalStructureSet lss2;
        GrayLevelImage img1;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.local.RCT = inputter.GetArg<Integer>("RC差错阀值");
            this.local.anglePIT = inputter.GetArg<DoubleFloat>("角度范围差错阀值(PI)");
            this.local.directionPIT = inputter.GetArg<DoubleFloat>("方向范围差错阀值(PI)");
            this.lss1 = inputter.GetArg<LocalStructureSet>("1102局部结构集合1");
            this.lss2 = inputter.GetArg<LocalStructureSet>("1102局部结构集合2");
            this.img1 = inputter.GetArg<GrayLevelImage>("原图像");
        }
        public DoubleFloat Score;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("匹配分数",  Score);
        }

        public override void Procedure()
        {
            HoughTansformation2D ht = new HoughTansformation2D(300, 200, 1, 5, Math2.PI(0.5), Math2.PI(0.02), Math2.PI(0.15));
            foreach (var ls1 in lss1)
            {
                foreach (var ls2 in lss2)
                {
                    this.local.Match(ls1["DATA"] as AngleRCSet, ls2["DATA"] as AngleRCSet);
                    var matched = false;
                    if (this.local.Matched.Count == 2)
                    {
                        //if(this.local.Matched.Any(titem => titem.RC < 3)){
                        //    matched = true;
                        //}
                    }
                    else
                    {
                        if (this.local.Matched.Count > 3)
                        {
                            matched = true;
                        }
                    }
                    if (!matched)
                    {
                        continue;
                    }
                    var m1 = ls1["BASE"] as Minutiae;
                    var m2 = ls2["BASE"] as Minutiae;
                    ht.Vote(m1.Location, m1.Angle, m2.Location, m2.Angle, 1);
                }
            }
            int i, j;
            double angle;
            var count = ht.GetBest(out i, out j, out angle);
            this.Score = new DoubleFloat() { Value = -(double)count / (double)Math.Min(lss1.Count, lss2.Count) };
            //System.Windows.Forms.MessageBox.Show(string.Format("{0} {1} {2}", lss1.Count, lss2.Count, count));

            //{
            //    var ls = new LocalStructure();
            //    ls.Present(null, "接受两个灰度图");
            //    try
            //    {
            //        var img = new GrayLevelImage();
            //        img.Allocate(ls.PrimaryData as GrayLevelImage, 1);
            //        (ls.PrimaryData as GrayLevelImage).FindAny((ti, tj, tv) =>
            //        {
            //            var cos = Math.Cos(-angle);
            //            var sin = Math.Sin(-angle);
            //            var tti = (int)(ti * cos - tj * sin);
            //            var ttj = (int)(tj * cos + ti * sin);
            //            try
            //            {
            //                img.Value[tti - i][ttj - j] = tv;
            //            }
            //            catch { }
            //            return false;
            //        });
            //        ls[ls.PrimaryDataKey] = img;
            //        ls.Present(img, string.Format("{0}*{1}={2}  weight={3}  i={4} j={5} angle={6}", lss1.Count, lss2.Count, lss1.Count * lss2.Count, count, i, j, angle));
            //    }
            //    catch { }
            //}

        }
    }

}
