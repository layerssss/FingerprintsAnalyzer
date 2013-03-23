using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_BRecover
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_BRecoverProcess(), args);
        }
    }
    class FRAProcess_BRecoverProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        StabSet recoverSlit;
        BinaryGraph BG;
        SegmentationArea rawArea;
        OrientationGraph OF;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.recoverSlit = inputter.GetArg<StabSet>("修复均匀针刺");
            this.BG = inputter.GetArg<BinaryGraph>("原二值图");
            this.rawArea = inputter.GetArg<SegmentationArea>("可靠区域");
            this.OF = inputter.GetArg<OrientationGraph>("方向场");
        }
        BinaryGraph newBG;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("二值图", newBG);
        }

        public override void Procedure()
        {
            newBG = BG.Clone();
            newBG.FindAny(
                (i, j) =>
                {
                    if (rawArea.IsInArea(i, j))
                    {
                        return false;
                    }
                    int stabNumRecover = (int)(this.OF.Value[i][j] * (this.recoverSlit.Count - 1) / Math.PI / 2);
                    int sumRecover = 0;
                    double countRecover = 0;
                    this.recoverSlit.IsOpen(
                        i,
                        j,
                        stabNumRecover,
                        BG,
                        (ti, tj) =>
                        {
                            if (ti != i || tj != j)
                            {
                                sumRecover += BG.Value[ti][tj];
                                countRecover++;
                            }
                        },
                        () => false);
                    this.recoverSlit.IsOpen(
                        i,
                        j,
                        stabNumRecover + (this.recoverSlit.Count - 1) / 2,
                        BG,
                        (ti, tj) =>
                        {
                            if (ti != i || tj != j)
                            {
                                sumRecover += BG.Value[ti][tj];
                                countRecover++;
                            }
                        },
                        () => false);
                    if (countRecover == 0)
                    {
                        this.newBG.Value[i][j] = 1;
                    }
                    else
                    {
                        this.newBG.Value[i][j] = (int)(((double)sumRecover) / countRecover + 0.51);
                    }
                    return false;
                });
        }
    }

}
