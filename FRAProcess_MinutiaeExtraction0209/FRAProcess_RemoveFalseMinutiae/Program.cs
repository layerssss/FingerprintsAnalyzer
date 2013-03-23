using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_RemoveFalseMinutiae
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_RemoveFalseMinutiaeProcess(), args);
        }
    }
    class FRAProcess_RemoveFalseMinutiaeProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        MinutiaeSet MS;
        BinaryGraph tBG;
        Integer W;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.MS = inputter.GetArg<MinutiaeSet>("细节点集");
            this.tBG = inputter.GetArg<BinaryGraph>("细化结果");
            this.W = inputter.GetArg<Integer>("细节点间距阀值");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("过滤后的细节点", this.MS);
        }

        public override void Procedure()
        {
            IntergerGraph mMap = new IntergerGraph();
            mMap.Allocate(this.tBG, 0);
            MS.ForEach(tm =>
            {
                mMap.Value[tm.I][tm.J]++;
            });
            var fillIdent = 2;
            foreach (var firstM in this.MS)
            {
                if (mMap.ValueGet(firstM.Location) == 0)
                {
                    continue;
                }
                var win = Window.GetSquare(firstM.I, firstM.J, this.W.Value, this.tBG);
                bool foundSecondM = false;//确保每次填充只找并改一个细节点
                this.tBG.FillAdvance(firstM.I, firstM.J, win.IMin, win.JMin, win.IMax, win.JMax,
                    tv =>
                    {
                        return tv != fillIdent && tv != 1;
                    },
                    (ti, tj, tv) =>
                    {
                        if (mMap.Value[ti][tj] != 0)
                        {
                            if (foundSecondM||
                                (mMap.Value[ti][tj]>1?//出现重合
                                false:
                                (ti==firstM.I&&tj==firstM.J)))
                            {
                                return fillIdent;
                            }
                            foundSecondM = true;
                            mMap.Value[ti][tj] --;
                            mMap.Value[firstM.I][firstM.J]--;
                        }
                        return fillIdent;
                    },Drawing2.EightConnectionAreaI,Drawing2.EightConnectionAreaJ);
                fillIdent++;
            }
            this.MS.RemoveAll(tm => mMap.ValueGet(tm.Location) == 0);
        }
    }

}
