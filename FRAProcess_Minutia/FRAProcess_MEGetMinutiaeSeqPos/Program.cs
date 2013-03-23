using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEGetMinutiaeSeqPos
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEGetMinutiaeSeqPosProcess(), args);
        }
    }
    class FRAProcess_MEGetMinutiaeSeqPosProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet minutiae;
        PointLocation refPoint;
        SegmentationArea SA;
        IntergerGraph mSkeleton;
        Integer sRate;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.minutiae = inputter.GetArg<PointLocationSet>("细节点");
            this.refPoint = inputter.GetArg<PointLocation>("参考点");
            this.SA = inputter.GetArg<SegmentationArea>("切割区域");
            this.mSkeleton = inputter.GetArg<IntergerGraph>("细节点骨架");
            this.sRate = inputter.GetArg<Integer>("取样率");
        }
        IntegerArraySeq mSeq;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点位置序列", mSeq);
        }

        public override void Procedure()
        {
            #region MyRegion
            //this.result = new DoubleFloatArraySeq();
            //this.result.Allocate(this.sampleRate.Value);
            //double deltaTheta = Math.PI * 2 / this.sampleRate.Value;
            //for (int k = 0; k < this.sampleRate.Value; k++)
            //{
            //    double theta = deltaTheta * k;
            //    for (int r = 0; ; r++)
            //    {
            //        int i = this.centerPoint.I - (int)(Math.Sin(theta) * r);
            //        int j = this.centerPoint.J + (int)(Math.Cos(theta) * r);
            //        if (!this.sa.IsInArea(i, j))
            //        {
            //            break;
            //        }
            //        this.result[k].Add(
            //            (2.5 * Math.PI + this.og.Value[i][j] - theta) % Math.PI
            //            );
            //    }
            //} 
            #endregion
            mSeq = new IntegerArraySeq();
            this.mSeq.Allocate(this.sRate.Value);
            double deltaTheta = Math.PI * 2 / this.sRate.Value;
            #region 第一阶段，分配长度
            for (int k = 0; k < this.sRate.Value; k++)
            {
                double theta = deltaTheta * k;
                for (int r = 0; ; r++)
                {
                    int i = this.refPoint.I - (int)(Math.Sin(theta) * r);
                    int j = this.refPoint.J + (int)(Math.Cos(theta) * r);
                    if (!this.SA.IsInArea(i, j))
                    {
                        break;
                    }
                    this.mSeq[k].Add(0);
                }
            }
            #endregion
            #region 第二阶段，记录点
            foreach (PointLocation m in this.minutiae)
            {
                double i = refPoint.I;
                double j = refPoint.J;
                int len = (int)Math.Sqrt((m.I - refPoint.I) * (m.I - refPoint.I) + (m.J - refPoint.J) * (m.J - refPoint.J));
                int k = (int)((Math.Atan2(-(i - m.I), -(m.J - j)) + Math.PI) / deltaTheta);
                if (this.mSeq[k].Count > len)
                {
                    this.mSeq[k][len] = 1;
                }
                
            }
            #endregion
        }
    }

}
