using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_OMCoreTransform
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OMCoreTransformProcess(), args);
        }
    }
    class FRAProcess_OMCoreTransformProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        SegmentationArea SA;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            OG = inputter.GetArg<OrientationGraph>("方向图");
            SA = inputter.GetArg<SegmentationArea>("有效区域");
        }
        GrayLevelImage CoreGraph;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("CORE强度", CoreGraph);
        }

        public override void Procedure()
        {
            this.CoreGraph = new GrayLevelImage();
            this.CoreGraph.Allocate(OG);
            double maxCore = 0;
            Console.WriteLine((Math2.DifOF(0.1 * Math.PI, 0.9 * Math.PI)/Math.PI).ToString());
            for(int i=0;i<this.CoreGraph.Height;i++)
            {
                for (int j = 0; j < this.CoreGraph.Width; j++)
                {
                    double sumCore = 0;
                    for(int ii=0;ii<this.OG.Height;ii++)
                    {
                        for (int jj = 0; jj < this.OG.Width; jj ++)
                        {
                            if (this.SA.IsInArea(ii, jj))
                            {
                                double core = Math2.DifOF(this.OG.Value[ii][jj],
                                    (Math.Atan2((i - ii), (jj - j)) + 1.5 * Math.PI) % (Math.PI));
                                int len=((i - ii) * (i - ii) + (jj - j) * (jj - j));
                                if (len == 0)
                                {
                                    continue;
                                }
                                core /= len;
                                sumCore += Math.Abs(core);
                            }
                        }
                    }
                    this.CoreGraph.Value[i][j] = sumCore;
                    if (sumCore > maxCore)
                    {
                        maxCore = sumCore;
                    }
                }
                this.Progress(i, this.CoreGraph.Height);
            }
            this.CoreGraph.FindAny((ti, tj, tv) =>
            {
                this.CoreGraph.Value[ti][tj] = tv / maxCore;
                return false;
            });
        }
    }

}
