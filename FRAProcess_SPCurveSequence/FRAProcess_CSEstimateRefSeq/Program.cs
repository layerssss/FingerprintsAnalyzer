using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_CSEstimateRefSeq
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CSEstimateRefSeqProcess(), args);
        }
    }
    class FRAProcess_CSEstimateRefSeqProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloatArray StandartCurveSample;
        DoubleFloatArray TargetCurveSample;
        DoubleFloat CurveSampleRotate;
        PointLocation RefPoint;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.StandartCurveSample = inputter.GetArg<DoubleFloatArray>("参考CURVE强度样本");
            this.TargetCurveSample = inputter.GetArg<DoubleFloatArray>("CURVE强度样本");
            this.CurveSampleRotate = inputter.GetArg<DoubleFloat>("CURVE强度样本旋转度");
            this.RefPoint = inputter.GetArg<PointLocation>("CURVE强度样本基准点");
        }
        
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("参考点", RefPoint);
        }

        public override void Procedure()
        {
            double minDif = double.MaxValue;
            int minDeltaL = 0;
            for (int deltaL = 0; deltaL < this.StandartCurveSample.Count / 2; deltaL++)
            {
                double sum = 0;
                for (int i = deltaL; i < this.StandartCurveSample.Count; i++)
                {
                    sum += this.StandartCurveSample[i] - this.TargetCurveSample[i - deltaL];
                }
                sum /= this.StandartCurveSample.Count - deltaL;
                sum = Math.Abs(sum);
                if (sum < minDif)
                {
                    minDif = sum;
                    minDeltaL = deltaL;
                }
            }
            RefPoint.I += (int)Math.Round(Math.Sin(this.CurveSampleRotate.Value) * minDeltaL);
            RefPoint.J -= (int)Math.Round(Math.Cos(this.CurveSampleRotate.Value) * minDeltaL);
        }
    }

}
