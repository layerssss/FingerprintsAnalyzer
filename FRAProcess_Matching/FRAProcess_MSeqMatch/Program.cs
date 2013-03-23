using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MSeqMatch
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MSeqMatchProcess(), args);
        }
    }
    class FRAProcess_MSeqMatchProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntegerArraySeq ias1;
        IntegerArraySeq ias2;
        Integer rTolarence;
        Integer hTolarence;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.ias1 = inputter.GetArg<IntegerArraySeq>("细节点序列1");
            this.ias2 = inputter.GetArg<IntegerArraySeq>("细节点序列2");
            rTolarence = inputter.GetArg<Integer>("旋转容忍度");
            hTolarence = inputter.GetArg<Integer>("竖直容忍度");
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
            string str1="non-match";
            string str2 = "non-match";
            this.rotate = new Integer();
            this.score = new DoubleFloat();
            double minScore = double.MaxValue;
            for (int r = -rTolarence.Value; r < rTolarence.Value+1; r++)
            {
                strInfo = "";
                int innerCounter1 = 1;
                int sumScore1 = 0;
                for (int k1 = 0; k1 < ias1.Count; k1++)
                {
                    int k2 = (k1 + r + ias1.Count) % ias1.Count;
                    for (int l = 0; l < ias1[k1].Count; l++)
                    {
                        if (ias1[k1][l] == 1)
                        {
                            sumScore1 += getScoreTobeadd(k2, l, ias2, ref innerCounter1); 
                        }
                    }
                }
                strInfo += string.Format("sumScore:{0};counter:{1}\r\n", sumScore1, innerCounter1);
                string infostr1 = strInfo;
                strInfo = "";
                int innerCounter2 = 1;
                int sumScore2 = 0;
                for (int k1 = 0; k1 < ias1.Count; k1++)
                {
                    int k2 = (k1 + r + ias1.Count) % ias1.Count;
                    for (int l = 0; l < ias2[k2].Count; l++)
                    {
                        if (ias2[k2][l] == 1)
                        {
                            sumScore2 += getScoreTobeadd(k1, l, ias1, ref innerCounter2);
                        }
                    }
                }
                strInfo += string.Format("sumScore:{0};counter:{1}\r\n", sumScore2, innerCounter2);
                double score = (double)(sumScore1 / innerCounter1 + sumScore2 / innerCounter2);
                if (score < minScore)
                {

                    minScore = score;
                    this.rotate.Value = r;
                    str1 = infostr1;
                    str2 = strInfo;
                }
            }
            this.score.Value = minScore;
            System.Windows.Forms.MessageBox.Show(rotate.Value.ToString());
            System.Windows.Forms.MessageBox.Show(str1, "1->2:");
            System.Windows.Forms.MessageBox.Show(str2, "2->1:");
        }
        int getScoreTobeadd(int k, int l, IntegerArraySeq seq, ref int innerCounter)
        {
                        if (l < 10)
                        {
                            return 0;
                        }
            for (int deltakK = 0; deltakK < seq.Count/2; deltakK++)
            {
                for (int deltaL = 0; deltaL < hTolarence.Value*2; deltaL++)
                {
                    int k1 = (k + seq.Count - deltakK) % seq.Count;
                    int k2 = (k + deltakK) % seq.Count;
                    int ll = l + deltaL / 2 * (deltaL % 2 == 0 ? -1 : 1);
                    if (ll < 0)
                    {
                        continue;
                    }
                    if (ll >= seq[k1].Count || ll >= seq[k2].Count)
                    {
                        //if (Math.Abs(deltakK) > 10)
                        //{
                        //    innerCounter++;
                        //}
                        this.strInfo += k + ":" + l +" out of region"+deltakK+"!\r\n";
                        return Math.Abs(deltakK) * Math.Abs(deltakK);
                    }
                    if(seq[k1][ll]==1||seq[k2][ll]==1){
                        innerCounter++;
                        if (Math.Abs(deltakK) > 50)
                        {
                            this.strInfo += k + ":" + l + ":" + ll + " high deltaK " + deltakK + "!\r\n";
                            //System.Windows.Forms.MessageBox.Show(k + ":" + l+":"+ll+":"+deltakK);
                        }
                        else
                        {
                            //

                            this.strInfo += k + ":" + l + ":" + ll + " match " + deltakK + "!\r\n";
                        }
                        return Math.Abs(deltakK) * Math.Abs(deltakK);
                    }
                }
            }
            //System.Windows.Forms.MessageBox.Show(k + ":" + l);
            this.strInfo += k + ":" + l + "miss" + seq.Count + "!\r\n";
            innerCounter++;
            return Math.Abs(seq.Count) * Math.Abs(seq.Count) * 4;
        }
        string strInfo="";
    }

}
