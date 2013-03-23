using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;

namespace FRAProcess_PoincareIndexesCal1
{
    class Program
    {
        static int Main(string[] args)
        {
            PI1 p = new PI1();
            return p.Execute(args);
        }
    }
    public class PI1 : FRAProcess.FRAProcess
    {
        public OrientationGraph Orientation;
        public PoincareIndexes PoincareIndexes = new PoincareIndexes();
        OrientationGraph pitemp = new OrientationGraph();
        public DoubleFloat incrementThreshold;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.Orientation = inputter.GetArg<OrientationGraph>("方向图");
            this.incrementThreshold = inputter.GetArg<DoubleFloat>("PI增量阀值");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<PoincareIndexes>("单像素结果", this.PoincareIndexes);
        }
        public List<int> AroundI = new List<int>();
        public List<int> AroundJ = new List<int>();
        public PI1()
        {
            this.AroundI.Add(1);
            this.AroundJ.Add(-1);

            this.AroundI.Add(0);
            this.AroundJ.Add(-1);

            this.AroundI.Add(-1);
            this.AroundJ.Add(-1);

            this.AroundI.Add(-1);
            this.AroundJ.Add(0);

            this.AroundI.Add(-1);
            this.AroundJ.Add(1);

            this.AroundI.Add(0);
            this.AroundJ.Add(1);


            this.AroundI.Add(1);
            this.AroundJ.Add(1);

            this.AroundI.Add(1);
            this.AroundJ.Add(0);
        }
        List<double> direction = new List<double>();
        public virtual void ProcessAround(int i, int j) { }
        public override void Procedure()
        {
            pitemp.Allocate(Orientation.Width, Orientation.Height);
            PoincareIndexes.Allocate(Orientation.Width, Orientation.Height);
            for (int i = 0; i < Orientation.Height; i++)
            {
                for (int j = 0; j < Orientation.Width; j++)
                {
                    try
                    {
                        double lastTheta =
                            Orientation.Value[i + AroundI[AroundI.Count - 1]][j + AroundJ[AroundI.Count - 1]];
                        double curTheta;
                        this.ProcessAround(i, j);
                        for (int k = 0; k < AroundI.Count; k++)
                        {
                            curTheta =
                                Orientation.Value[i + AroundI[k]][j + AroundJ[k]];
                            #region 公式2010.11.01.1
                            double pIIncrement = (curTheta - lastTheta + Math.PI * 1.5) % Math.PI - Math.PI * 0.5;//0.63
                            
                            #endregion
                            if (Math.Abs(pIIncrement) > this.incrementThreshold.Value)
                            {
                                this.pitemp.Value[i][j] = -Math.PI * 3;
                                break;
                            }
                            this.pitemp.Value[i][j] += pIIncrement;
                            lastTheta = curTheta;
                        }
                        this.PoincareIndexes.Value[i][j] = (int)(this.pitemp.Value[i][j] / Math.PI);
                    }
                    catch {
                        this.PoincareIndexes.Value[i][j] = -3;
                    }
                }
            }
        }
    }
}
