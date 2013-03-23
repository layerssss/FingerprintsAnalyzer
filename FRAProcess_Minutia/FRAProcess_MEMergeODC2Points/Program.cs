using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_MEMergeODC2Points
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEMergeODC2PointsProcess(), args);
        }
    }
    class FRAProcess_MEMergeODC2PointsProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        IntergerGraph ODCGraph;
        StabSet ODCBoxes;
        Integer IntensityThreshold;
        OrientationGraph OG;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.ODCGraph = inputter.GetArg<IntergerGraph>("ODC图");
            this.ODCBoxes = inputter.GetArg<StabSet>("ODC盒子");
            this.IntensityThreshold = inputter.GetArg<Integer>("密度阀值");
            this.OG = inputter.GetArg<OrientationGraph>("走向图");
        }
        PointLocationSet REs;
        PointLocationSet BIs;
        IntergerGraph IntensityGraphTest;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("RE", REs);
            outputter.PutArg("BI", BIs);
            outputter.PutArg("密度图（测试）", this.IntensityGraphTest);
        }

        public override void Procedure()
        {
            this.IntensityGraphTest = new IntergerGraph();
            this.IntensityGraphTest.Allocate(OG.Width, OG.Height);
            this.REs = new PointLocationSet();
            this.BIs = new PointLocationSet();
            for (int i = 0; i < this.ODCGraph.Height; i++)
            {
                for (int j = 0; j < this.ODCGraph.Width; j++)
                {
                    PointLocationSet pls1=new PointLocationSet();
                    PointLocationSet pls3 = new PointLocationSet();
                    this.ODCBoxes.IsOpen(i, j,
                        (int)(this.OG.Value[i][j] *(this.ODCBoxes.Count-1)/Math.PI),
                        this.ODCGraph,
                        (ti, tj) =>
                        {
                            int tv = this.ODCGraph.Value[ti][tj];
                            if (tv == 1)
                            {
                                pls1.Add(new PointLocation(ti, tj));
                            }
                            if (tv == 3)
                            {
                                pls3.Add(new PointLocation(ti, tj));
                            }
                        },
                        (() => false)
                        );
                    if (pls1.Count >= IntensityThreshold.Value)
                    {
                        pls1.ForEach(tpl => this.ODCGraph.Value[tpl.I][tpl.J] = 2);
                        this.REs.Add(pls1.WeightCore());
                    }
                    if (pls3.Count >= IntensityThreshold.Value)
                    {
                        pls3.ForEach(tpl => this.ODCGraph.Value[tpl.I][tpl.J] = 2);
                        this.BIs.Add(pls3.WeightCore());
                    }
                    this.IntensityGraphTest.Value[i][j] = pls1.Count;
                }
            }
        }
    }

}
