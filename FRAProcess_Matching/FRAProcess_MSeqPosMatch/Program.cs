using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MSeqPosMatch
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MSeqPosMatchProcess(), args);
        }
    }
    class FRAProcess_MSeqPosMatchProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntegerArraySeq ias1;
        IntegerArraySeq ias2;
        Integer rTolarence;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.ias1 = inputter.GetArg<IntegerArraySeq>("细节点位置序列1");
            this.ias2 = inputter.GetArg<IntegerArraySeq>("细节点位置序列2");
            rTolarence = inputter.GetArg<Integer>("旋转容忍度");
        }
        Integer rotate;
        DoubleFloat score;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("旋转", rotate);
            outputter.PutArg("匹配分数", score);
        }

        public override void Procedure()
        {
            this.rotate = new Integer();
            this.score = new DoubleFloat();
            double minScore = double.MaxValue;
            for (int r = -rTolarence.Value; r < rTolarence.Value; r++)
            {
                double sumScore = 0;
                int counter = 0;
                for (int k1 = 0; k1 < ias1.Count; k1++)
                {
                    int k2 = (k1 + r + ias1.Count) % ias1.Count;
                    for (int l = 0; l < ias1[k1].Count; l++)
                    {
                        if (ias1[k1][l] == 1)
                        {
                            sumScore += getScoreTobeadd(k2, l, ias2);
                            counter++;
                        }
                    }
                }
                for (int k1 = 0; k1 < ias1.Count; k1++)
                {
                    int k2 = (k1 + r + ias1.Count) % ias1.Count;
                    for (int l = 0; l < ias2[k2].Count; l++)
                    {
                        if (ias2[k2][l] == 1)
                        {
                            sumScore += getScoreTobeadd(k1, l, ias1);
                            counter++;
                        }
                    }
                }
                sumScore /= counter;
                if (sumScore <= minScore)
                {
                    minScore = sumScore;
                    this.rotate.Value = r;
                }
            }
            this.score.Value = minScore;
        }
        int getScoreTobeadd(int k, int l, IntegerArraySeq seq)
        {
            for (int size = 1; ; size++)
            {
                for (int deltaK =  - size; deltaK <= size; deltaK++)
                {
                    int tk = (k + seq.Count + deltaK) % seq.Count;
                    for (int tl = l - size; tl <= l + size; tl++)
                    {
                        if (tl < 0)
                        {
                            continue;
                        }
                        if (tl >= seq[tk].Count||seq[tk][tl] == 1)
                        {
                            return deltaK * deltaK + (tl - l) * (tl - l);
                        }
                    }
                }
            }
        }
    }

}
