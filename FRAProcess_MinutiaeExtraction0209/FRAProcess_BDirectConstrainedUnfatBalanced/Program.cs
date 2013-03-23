using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BDirectConstrainedUnfatBalanced
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BDirectConstrainedUnfatBalancedProcess(), args);
        }
    }
    class FRAProcess_BDirectConstrainedUnfatBalancedProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        BinaryGraph BG;
        DoubleFloat DirectionRange;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
            this.BG = inputter.GetArg<BinaryGraph>("增强后的二值图");
            this.DirectionRange = inputter.GetArg<DoubleFloat>("走向场范围");
        }
        BinaryGraph newBG;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细化结果", newBG);
        }
        public void TryHitTemplate(int i, int j, int deltaI, int deltaJ, Action<int, int> act)
        {
            int newi = i + deltaI;
            int newj = j + deltaJ;
            if (!BG.IsInbound(newi, newj) || BG.Value[newi][newj] != 0)
            {
                return;
            }
            for (int l = 0; l < Drawing2.FourConnectionAreaI.Length; l++)
            {
                int li = Drawing2.FourConnectionAreaI[l];
                int lj = Drawing2.FourConnectionAreaJ[l];
                if (!BG.IsInbound(newi + li, newj + lj))
                {
                    continue;
                }
                if (!BG.IsInbound(i + li, j + lj))
                {
                    continue;
                }
                if (BG.Value[i + li][j + lj] == 0)
                {
                    if (BG.Value[newi + li][newj + lj] != 0)
                    {
                        return;
                    }
                }
            }
            act(newi, newj);
        }
        public override void Procedure()
        {
            var directedI = new int[] { -1, +0, +1, +0 };
            var directedJ = new int[] { +0, -1, +0, +1 };
            bool changed;
            Action<int, int> hitAct = (tnewi, tnewj) =>
            {
                this.BG.Value[tnewi][tnewj] = 2;
                this.newBG.Value[tnewi][tnewj] = 1;
                changed = true;
            };
            do
            {
                changed = false;
                newBG = BG.Clone();
                BG.FindAny((ti, tj) =>
                {
                    if (BG.Value[ti][tj] == 1)
                    {
                        var k = (int)(this.OG.Value[ti][tj] * 2 / Math.PI-DirectionRange.Value + 0.5) % 2;
                        var kplus = (int)(this.OG.Value[ti][tj] * 2 / Math.PI+DirectionRange.Value + 0.5) % 2;
                        this.TryHitTemplate(ti, tj, directedI[k], directedJ[k], hitAct);
                        this.TryHitTemplate(ti, tj, directedI[k + 2], directedJ[k + 2], hitAct);
                        if (kplus != k)
                        {
                            this.TryHitTemplate(ti, tj, directedI[kplus], directedJ[kplus], hitAct);
                            this.TryHitTemplate(ti, tj, directedI[kplus + 2], directedJ[kplus + 2], hitAct);
                        }
                    }
                    return false;
                });
                BG = new BinaryGraph(newBG.Clone());
            }
            while (changed);
        }
    }

}
