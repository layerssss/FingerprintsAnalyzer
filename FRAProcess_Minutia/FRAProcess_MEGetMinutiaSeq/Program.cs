using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEGetMinutiaSeq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEGetMinutiaSeqProcess(), args);
        }
    }
    class FRAProcess_MEGetMinutiaSeqProcess : FRAProcess.FRAProcess
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
            outputter.PutArg("细节点序列", mSeq);
        }

        public override void Procedure()
        {
            mSeq = new IntegerArraySeq();
            this.mSeq.Allocate(this.sRate.Value);
            double deltaTheta = Math.PI * 2 / this.sRate.Value;
            #region 第一阶段，分配长度
            for (int k = 0; k < this.sRate.Value; k++)
            {
                if (refPoint.I == 0 || refPoint.J == 0||refPoint.I==mSkeleton.Height-1||refPoint.J==mSkeleton.Width-1)
                {
                    continue;
                }
                double theta = deltaTheta * k;
                int r = 0;
                int i, j;
                #region 进入第一个Close
                for (; ; r++)
                {
                    i = this.refPoint.I - (int)(Math.Sin(theta) * r + 0.5);
                    j = this.refPoint.J + (int)(Math.Cos(theta) * r + 0.5);
                    if (i == 0 || j == 0 || i == mSkeleton.Height - 1 || j == mSkeleton.Width - 1)
                    {
                        goto ok;
                    }
                    if (isClosing(i, j))
                    {
                        break;
                    }
                }
                #endregion
                bool closing = true;
                do
                {
                    r++;
                    i = this.refPoint.I - (int)(Math.Sin(theta) * r + 0.5);
                    j = this.refPoint.J + (int)(Math.Cos(theta) * r + 0.5);
                    if (closing)
                    {//等待Open
                        if (!this.isClosing(i, j))
                        {
                            this.mSeq[k].Add(0);
                            closing = false;
                        }
                    }
                    else
                    {//等待Close
                        if (!this.isClosing(i, j))
                        {
                            closing = true;
                        }
                    }
                } while (this.SA.IsInArea(i, j));

            ok: this.mSeq[k].Add(0);
            } 
            #endregion
            #region 第二阶段，记录点
            foreach (PointLocation m in this.minutiae)
            {
                int sum = 0;
                double i=refPoint.I;
                double j=refPoint.J;
                double len = Math.Sqrt((m.I - refPoint.I) * (m.I - refPoint.I) + (m.J - refPoint.J) * (m.J - refPoint.J));
                if (len == 0)
                {
                    continue;
                }
                double deltaI = (m.I - i) / len;
                double deltaJ = (m.J - j) / len;
                int k = (int)((Math.Atan2(-(i - m.I), -(m.J - j)) + Math.PI) / deltaTheta);
                #region 进入第一个Close
                while (true)
                {
                    i += deltaI;
                    j += deltaJ;
                    len--;
                    if ((int)(i + 0.5) == 0 || (int)(j + 0.5) == 0 || (int)(i + 0.5) == mSkeleton.Height - 1 || (int)(j + 0.5) == mSkeleton.Width - 1)
                    {
                        goto ok2;
                    }
                    if (isClosing((int)(i+0.5), (int)(j+0.5)))
                    {
                        break;
                    }
                }
                #endregion
                bool closing = true;
                do
                {
                    i += deltaI;
                    j += deltaJ;
                    len--;
                    if (closing)
                    {//等待Open
                        if (!this.isClosing((int)(i + 0.5), (int)(j + 0.5)))
                        {
                            sum++;
                            closing = false;
                        }
                    }
                    else
                    {//等待Close
                        if (!this.isClosing((int)(i + 0.5), (int)(j + 0.5)))
                        {
                            closing = true;
                        }
                    }
                } while (len > 0);
                if (sum < this.mSeq[k].Count)
                {
                    this.mSeq[k][sum] = 1;
                }
            ok2: len++;
            }
            #endregion
        }
        /// <summary>
        /// Determines whether the specified i is closing(在脊线上).
        /// </summary>
        /// <param name="i">The i.</param>
        /// <param name="j">The j.</param>
        /// <returns>
        /// 	<c>true</c> if the specified i is closing; otherwise, <c>false</c>.
        /// </returns>
        public bool isClosing(int i, int j)
        {
            return this.mSkeleton.Value[i][j] != 1 || this.mSkeleton.Value[i + 1][j] != 1;
        }
    }

}
