using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_UFGetBG
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_UFGetBGProcess(), args);
        }
    }
    class FRAProcess_UFGetBGProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph BG;
        SegmentationArea SA;
        Integer SRate;
        PointLocation refPoint;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            BG = inputter.GetArg<BinaryGraph>("二值图");
            SA = inputter.GetArg<SegmentationArea>("有效区域");
            SRate = inputter.GetArg<Integer>("采样率");
            refPoint = inputter.GetArg<PointLocation>("参考点");
        }
        IntegerArraySeq iqs;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("展开后二值图", iqs);
        }

        public override void Procedure()
        {
            iqs = new IntegerArraySeq();
            this.iqs.Allocate(this.SRate.Value);
            double deltaTheta = Math.PI * 2 / this.SRate.Value;
            for (int k = 0; k < this.SRate.Value; k++)
            {
                double theta = deltaTheta * k;
                for (int r = 0; ; r++)
                {
                    int i = this.refPoint.I - (int)(Math.Sin(theta) * r + 0.5);
                    int j = this.refPoint.J + (int)(Math.Cos(theta) * r + 0.5);
                    if (!SA.IsInArea(i, j))
                    {
                        break;
                    }
                    iqs[k].Add(this.BG.Value[i][j]);
                }
            }
        }
    }

}
