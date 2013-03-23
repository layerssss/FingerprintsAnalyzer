using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFSExtract
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFSExtractProcess(), args);
        }
    }
    class FRAProcess_OFSExtractProcess : FRAProcess.FRAProcess
    {
        Integer sampleRate;
        PointLocation centerPoint;
        OrientationGraph og;
        SegmentationArea sa;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.sampleRate = inputter.GetArg<Integer>("采样数目");
            this.centerPoint = inputter.GetArg<PointLocation>("旋转中心点");
            this.og = inputter.GetArg<OrientationGraph>("方向图");
            this.sa = inputter.GetArg<SegmentationArea>("切割区域");
        }
        DoubleFloatArraySeq result;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<DoubleFloatArraySeq>("结果样本", result);
        }

        public override void Procedure()
        {
            this.result = new DoubleFloatArraySeq();
            this.result.Allocate(this.sampleRate.Value);
            double deltaTheta = Math.PI * 2 / this.sampleRate.Value;
            for (int k=0; k < this.sampleRate.Value; k++)
            {
                double theta = deltaTheta * k;
                for (int r = 0; ; r++)
                {
                    int i = this.centerPoint.I - (int)(Math.Sin(theta) * r);
                    int j = this.centerPoint.J + (int)(Math.Cos(theta) * r);
                    if (!this.sa.IsInArea(i, j))
                    {
                        break;
                    }
                    this.result[k].Add(
                        (2.5 * Math.PI + this.og.Value[i][j] - theta) % Math.PI
                        );
                }
            }
        }
    }

}
