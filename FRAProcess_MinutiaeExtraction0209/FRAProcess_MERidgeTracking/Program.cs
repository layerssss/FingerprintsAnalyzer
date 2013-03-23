using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MERidgeTracking
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MERidgeTrackingProcess(), args);
        }
    }
    class FRAProcess_MERidgeTrackingProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph TBG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.TBG = inputter.GetArg<BinaryGraph>("细化的二值图");
        }
        PointLocationSet BIs;
        PointLocationSet REs;
        PointLocationSet StartPoints;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("BIs", BIs);
            outputter.PutArg("REs", REs);
            outputter.PutArg("剩余图像", TBG);
            outputter.PutArg("开始点", StartPoints);
        }

        public override void Procedure()
        {
            this.BIs = new PointLocationSet();
            this.REs = new PointLocationSet();
            this.StartPoints = new PointLocationSet();
            int foundi=0;
            int foundj=0;
            List<PointLocation> left = new List<PointLocation>();
            while(this.TBG.FindAny((ti,tj,tv)=>{//寻找一个存在一个相邻黑像素且自己是黑的点
                if (tv != 0)
                {
                    return false;
                }
                var count = 0;
                Drawing2.EightConnectionArea((deltai, deltaj, k) => {
                    var i = ti + deltai;
                    var j = tj + deltaj;
                    if (this.TBG.IsInbound(i, j)&&this.TBG.Value[i][j] == 0)
                    {
                        count++;
                    }
                });
                if (count == 1)
                {
                    foundi = ti;
                    foundj = tj;
                    return true;
                }
                return false;
            })){
                this.REs.Add(foundi, foundj);
                this.StartPoints.Add(foundi, foundj);
                left.Add(new PointLocation(foundi, foundj));
                while (left.Any())
                {
                    var cur = left.First();
                    left.Remove(cur);
                    while (true)
                    {
                        this.TBG.Value[cur.I][cur.J] = 1;//把它变为白的
                        //寻找第一个相邻黑像素
                        int k = 0;
                        while ((!this.TBG.IsInbound(cur.I + Drawing2.EightConnectionAreaI[k], cur.J + Drawing2.EightConnectionAreaJ[k]))
                            || this.TBG.Value[cur.I + Drawing2.EightConnectionAreaI[k]][cur.J + Drawing2.EightConnectionAreaJ[k]] != 0)
                        {
                            k++;
                            if (k == Drawing2.EightConnectionAreaI.Length)//没有相邻黑像素
                            {
                                var kInLeft = -1;//检查周围是否有在left中的点
                                Drawing2.EightConnectionArea((ti, tj, tk) =>
                                {
                                    if (left.Any(tpl => tpl.I == cur.I + ti && tpl.J == cur.J + tj))
                                    {
                                        kInLeft = tk;
                                    }
                                });
                                if (kInLeft != -1)
                                {
                                    left.RemoveAll(tpl => tpl.I == cur.I + Drawing2.EightConnectionAreaI[kInLeft] && tpl.J == cur.J + Drawing2.EightConnectionAreaJ[kInLeft]);
                                }
                                else
                                {
                                    this.REs.Add(cur.I, cur.J);
                                }
                                break;
                            }
                        }
                        //把剩余的黑像素添加到left和BI里去（如果有的话）并把它变成白的
                        for (var tk = k+1; tk < Drawing2.EightConnectionAreaI.Length; tk++)
                        {
                            if (this.TBG.IsInbound(cur.I + Drawing2.EightConnectionAreaI[tk], cur.J + Drawing2.EightConnectionAreaJ[tk])
                                && this.TBG.Value[cur.I + Drawing2.EightConnectionAreaI[tk]][cur.J + Drawing2.EightConnectionAreaJ[tk]] == 0)
                            {
                                this.TBG.Value[cur.I + Drawing2.EightConnectionAreaI[tk]][cur.J + Drawing2.EightConnectionAreaJ[tk]] = 1;
                                left.Add(new PointLocation(cur.I + Drawing2.EightConnectionAreaI[tk], cur.J + Drawing2.EightConnectionAreaJ[tk]));
                                this.BIs.Add(cur.I + Drawing2.EightConnectionAreaI[tk], cur.J + Drawing2.EightConnectionAreaJ[tk]);
                                break;
                            }
                        }
                        if (k == Drawing2.EightConnectionAreaI.Length)
                        {
                            break;
                        }
                        cur.I += Drawing2.EightConnectionAreaI[k];
                        cur.J += Drawing2.EightConnectionAreaJ[k];
                    }
                }
            }
        }

    }

}
