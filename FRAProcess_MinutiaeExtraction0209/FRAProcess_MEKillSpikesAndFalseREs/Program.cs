using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEKillSpikesAndFalseREs
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEKillSpikesAndFalseREsProcess(), args);
        }
    }
    class FRAProcess_MEKillSpikesAndFalseREsProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet REs;
        PointLocationSet BIs;
        Integer RERET;
        Integer REBIT;
        Integer BIBIT;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.REs = inputter.GetArg<PointLocationSet>("REs");
            this.BIs = inputter.GetArg<PointLocationSet>("BIs");
            this.RERET = inputter.GetArg<Integer>("孤立线长度阀值");
            this.REBIT = inputter.GetArg<Integer>("毛刺长度阀值");
            this.BIBIT = inputter.GetArg<Integer>("桥长度阀值");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("过滤后的REs", REs);
            outputter.PutArg("过滤后的BIs", BIs);
        }

        public override void Procedure()
        {
            #region REBI
            for (int k = 0; k < this.REs.Count; k++)
            {
                for (int l = 0; l < this.BIs.Count; l++)
                {
                    PointLocation p1 = this.REs[k];
                    PointLocation p2 = this.BIs[l];
                    if (p1 == null || p2 == null)
                    {
                        continue;
                    }
                    if (Math.Abs(p1.I - p2.I) >= this.REBIT.Value || Math.Abs(p1.J - p2.J) >= this.REBIT.Value)
                    {
                        continue;
                    }
                    this.REs[k] = null;
                    this.BIs[l] = null;
                }
            }
            this.REs.RemoveAll(tp => tp == null);
            this.BIs.RemoveAll(tp => tp == null);
            #endregion
            #region RERE
            for (int k = 0; k < this.REs.Count; k++)
            {
                for (int l = k + 1; l < this.REs.Count; l++)
                {
                    PointLocation p1 = this.REs[k];
                    PointLocation p2 = this.REs[l];
                    if (p1 == null || p2 == null)
                    {
                        continue;
                    }
                    if (Math.Abs(p1.I - p2.I) >= this.RERET.Value || Math.Abs(p1.J - p2.J) >= this.RERET.Value)
                    {
                        continue;
                    }
                    this.REs[k] = null;
                    this.REs[l] = null;
                }
            }
            this.REs.RemoveAll(tp => tp == null);
            #endregion
            #region BIBI
            for (int k = 0; k < this.BIs.Count; k++)
            {
                for (int l = k + 1; l < this.BIs.Count; l++)
                {
                    PointLocation p1 = this.BIs[k];
                    PointLocation p2 = this.BIs[l];
                    if (p1 == null || p2 == null)
                    {
                        continue;
                    }
                    if (Math.Abs(p1.I - p2.I) >= this.BIBIT.Value || Math.Abs(p1.J - p2.J) >= this.BIBIT.Value)
                    {
                        continue;
                    }
                    this.BIs[k] = null;
                    this.BIs[l] = null;
                }
            }
            this.BIs.RemoveAll(tp => tp == null);
            this.BIs.RemoveAll(tp => tp == null);
            #endregion
        }
    }

}
