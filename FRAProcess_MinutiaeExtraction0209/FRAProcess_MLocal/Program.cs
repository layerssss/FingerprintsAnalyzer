using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MLocal
{
    class Program
    {
        [STAThreadAttribute]
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MLocalProcess(), args);
        }
    }
    public class FRAProcess_MLocalProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public Integer RCT;
        public DoubleFloat anglePIT;
        public DoubleFloat directionPIT;
        LocalStructureTestSets test;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.RCT = inputter.GetArg<Integer>("RC差错阀值");
            this.anglePIT = inputter.GetArg<DoubleFloat>("角度范围差错阀值(PI)");
            this.directionPIT = inputter.GetArg<DoubleFloat>("方向范围差错阀值(PI)");
            this.test = inputter.GetArg<LocalStructureTestSets>("1102测试对集");
        }
        public AngleRCSet Matched;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            //outputter.PutArg("匹配的对", this.matched);
        }

        public override void Procedure()
        {
            LocalStructureSet lss = new LocalStructureSet();
            Console.WriteLine("MATCHED:");
            for (int i = 0; i < 10; i++)
            {
                this.Result(this.test.GetRandomMatched());
            }
            Console.WriteLine("UNMATCHED:");
            for (int i = 0; i < 10; i++)
            {
                this.Result(this.test.GetRandomUnmatched());
            }
        }
        public void Result(LocalStructureTest pair)
        {
            this.Match(pair.Data[0]["DATA"] as AngleRCSet, pair.Data[1]["DATA"] as AngleRCSet);
            Console.Write("RAD   ");
            //var sum = 0.0;
            foreach (var item in this.Matched)
            {
                Console.Write("{0,2:d},{1,5:f2},{2,5:f2}   ;", item.RC, item.Angle, item.Direction);
                //sum += 1;//10.0 / item.RC;
            }
            Console.WriteLine();//"{0,7:f2}",sum);
        }
        public void Match(AngleRCSet data1, AngleRCSet data2)
        {
            this.Matched = new AngleRCSet();
            foreach (var item in data1)
            {
                AngleRC item2 = null;
                if (data2.Any(ti =>
                {
                    var rc1=item.RC;
                    var rc2=ti.RC;
                    var angle1=item.Angle/Math.PI;
                    var angle2 = ti.Angle / Math.PI;
                    var dir1 = item.Direction / Math.PI;
                    var dir2 = ti.Direction / Math.PI;
                    if (Math.Abs(rc1 - rc2) <= this.RCT.Value &&
                        ((angle1 + 2 - angle2) % 2 <= this.anglePIT.Value ||
                        (angle2 + 2 - angle1) % 2 <= this.anglePIT.Value) &&
                        ((dir1 + 2 - dir2) % 2 <= this.directionPIT.Value ||
                        (dir2 + 2 - dir1) % 2 <= this.directionPIT.Value)
                        )
                    {
                        item2 = ti;
                        return true;
                    }
                    return false;
                }))
                {
                    this.Matched.Add(Math2.DifDf(item.Angle ,item2.Angle), item.RC,Math2.DifDf( item.Direction , item2.Direction));
                }
            }
        }
    }

}
