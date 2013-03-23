using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEKillSpikes
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEKillSpikesProcess(), args);
        }
    }
    class FRAProcess_MEKillSpikesProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet REs;
        PointLocationSet BIs;
        IntergerGraph MinutiaSkeleton;
        Integer RERET;
        Integer REBIT;
        Integer BIBIT;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.REs = inputter.GetArg<PointLocationSet>("RE");
            this.BIs = inputter.GetArg<PointLocationSet>("BI");
            this.RERET = inputter.GetArg<Integer>("孤立线长度阀值");
            this.REBIT = inputter.GetArg<Integer>("毛刺长度阀值");
            this.BIBIT = inputter.GetArg<Integer>("桥长度阀值");
            this.MinutiaSkeleton = inputter.GetArg<IntergerGraph>("原细节点骨架");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点骨架", this.MinutiaSkeleton);
        }

        public override void Procedure()
        {
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
                    FRADataStructs.DataStructs.Abstract.UpTreeNode<PointLocation> routeNode;
                    int routeLen;
                    if (MinutiaSkeleton.FindRoute(
                        p1.I, p1.J, p2.I, p2.J, this.RERET.Value,
                        out routeLen,
                        out routeNode,
                        2))
                    {
                        do
                        {
                            this.MinutiaSkeleton.Value[routeNode.Obj.I][routeNode.Obj.J] = 1;
                            routeNode = routeNode.Parent;
                        }
                        while (routeNode != null);
                        this.REs[k] = null;
                        this.REs[l] = null;
                    }
                }
            } 
            #endregion
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
                    FRADataStructs.DataStructs.Abstract.UpTreeNode<PointLocation> routeNode;
                    int routeLen;
                    if (MinutiaSkeleton.FindRoute(
                        p1.I, p1.J, p2.I, p2.J, this.REBIT.Value,
                        out routeLen,
                        out routeNode,
                        2))
                    {
                        this.MinutiaSkeleton.Value[routeNode.Obj.I][routeNode.Obj.J] = 2;
                        while (routeNode.Parent != null)
                        {
                            routeNode = routeNode.Parent;
                            this.MinutiaSkeleton.Value[routeNode.Obj.I][routeNode.Obj.J] = 1;
                        }
                        this.REs[k] = null;
                        this.BIs[l] = null;
                    }
                }
            }
            #endregion
            #region BIBI
            for (int k = 0; k < this.BIs.Count; k++)
            {
                for (int l = k+1; l < this.BIs.Count; l++)
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
                    FRADataStructs.DataStructs.Abstract.UpTreeNode<PointLocation> routeNode;
                    int routeLen;
                    if (MinutiaSkeleton.FindRoute(
                        p1.I, p1.J, p2.I, p2.J, this.BIBIT.Value,
                        out routeLen,
                        out routeNode,
                        2))
                    {
                        this.MinutiaSkeleton.Value[routeNode.Obj.I][routeNode.Obj.J] = 2;
                        routeNode = routeNode.Parent;
                        while (routeNode.Parent != null)
                        {
                            this.MinutiaSkeleton.Value[routeNode.Obj.I][routeNode.Obj.J] = 1;
                            routeNode = routeNode.Parent;
                        }
                        this.MinutiaSkeleton.Value[routeNode.Obj.I][routeNode.Obj.J] = 2;
                        this.BIs[k] = null;
                        this.BIs[l] = null;
                    }
                }
            }
            #endregion
        }
    }

}
