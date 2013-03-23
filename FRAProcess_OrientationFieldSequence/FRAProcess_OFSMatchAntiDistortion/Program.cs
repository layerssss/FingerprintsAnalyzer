using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OFSMatchAntiDistortion
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OFSMatchAntiDistortionProcess(), args);
        }
    }
    class FRAProcess_OFSMatchAntiDistortionProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloatArraySeq OFS1;
        DoubleFloatArraySeq OFS2;
        Integer RotationTolarence;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OFS1 = inputter.GetArg<DoubleFloatArraySeq>("第一个OFS");
            this.OFS2 = inputter.GetArg<DoubleFloatArraySeq>("第二个OFS");
            this.RotationTolarence = inputter.GetArg<Integer>("旋转容忍度");
        }
        DoubleFloat matchingScore;
        Integer matchingCount;
        Integer rotation;
        DoubleFloat diff_avg_change;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<DoubleFloat>("匹配分数", matchingScore);
            outputter.PutArg<Integer>("匹配量", matchingCount);
            outputter.PutArg<Integer>("旋转", rotation);
            outputter.PutArg<DoubleFloat>("diff_avg_变化总量", diff_avg_change);
        }

        public override void Procedure()
        {
            this.diff_avg_change = new DoubleFloat() { Value = 0 };
            this.matchingCount = new Integer() { Value = 0 };
            this.matchingScore = new DoubleFloat() { Value = double.MaxValue };
            this.rotation = new Integer();
            int mCount;
            double mScore;
            double last_diff_avg = 0;
            for (int rot = -this.RotationTolarence.Value; rot < this.RotationTolarence.Value; rot++)//旋转枚举
            {
                mCount = 0;
                mScore = 0;
                for (int theta1 = 0; theta1 < this.OFS1.Count; theta1++)
                {
                    int theta2 = (theta1 + rot + this.OFS1.Count) % this.OFS1.Count;
                    List<double> diff = new List<double>();
                    for (int r = 0; r < this.OFS1[theta1].Count && r < this.OFS2[theta2].Count; r++)
                    {
                        mCount++;
                        diff.Add(this.OFS1[theta1][r] - this.OFS2[theta2][r]);
                    }
                    double diff_avg = diff.Average();
                    for (int r = 0; r < diff.Count; r++)
                    {
                        mScore += Math.Abs(diff[r] - diff_avg);
                    }
                    if (last_diff_avg != 0)
                    {
                        this.diff_avg_change.Value += Math.Abs(diff_avg - last_diff_avg);
                    }
                    last_diff_avg = diff_avg;
                }
                if (mScore < this.matchingScore.Value)//更优
                {
                    this.matchingScore.Value = mScore;
                    this.matchingCount.Value = mCount;
                    this.rotation.Value = rot;
                }
            }
            Console.WriteLine("mScore={0};mCount={1};rot={2};", matchingScore.Value, matchingCount.Value, rotation.Value);
        }
    }

}
