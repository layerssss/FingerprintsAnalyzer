using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_OpenDirectionCount
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_OpenDirectionCountProcess(), args);
        }
    }
    class FRAProcess_OpenDirectionCountProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        Integer closeTolarence;
        StabSet stabSet;
        BinaryGraph bg;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.closeTolarence = inputter.GetArg<Integer>("闭合容忍度");
            this.stabSet = inputter.GetArg<StabSet>("针刺集");
            this.bg = inputter.GetArg<BinaryGraph>("二值图");
        }
        IntergerGraph odcGraph;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<IntergerGraph>("ODC图", this.odcGraph);
        }

        public override void Procedure()
        {
            this.odcGraph = new IntergerGraph();
            this.odcGraph.Allocate(this.bg.Width, this.bg.Height);
            for (int i = 0; i < bg.Height; i++)
            {
                for (int j = 0; j < bg.Width; j++)
                {
                    if (bg.Value[i][j] == 1)
                    {
                        odcGraph.Value[i][j] = 0;
                        continue;
                    }
                    #region 进入第一个Close域先
                    int k = 0;
                    while (this.stabSet.IsOpen(i, j, k, bg,
                        tv => tv == 1,
                        closeTolarence.Value))
                    {
                        k++;
                        if (k == this.stabSet.Count)
                        {
                            throw (new FRAProcess.AlgorithmException("Stab too short!"));
                            //全部都是Open了
                        }
                    }
                    #endregion
                    bool closing = true;
                    for (; k < this.stabSet.Count; k++)
                    {
                        if (closing)
                        {//等待Open
                            if (this.stabSet.IsOpen(i, j, k, bg,
                        tv => tv == 1,
                        closeTolarence.Value))
                            {
                                this.odcGraph.Value[i][j]++;
                                closing = false;
                            }
                        }
                        else
                        {//等待Close
                            if (!this.stabSet.IsOpen(i, j, k, bg,
                        tv => tv == 1,
                        closeTolarence.Value))
                            {
                                closing = true;
                            }
                        }
                    }
                }
            }
        }
    }

}
