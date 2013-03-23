using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEGetMinutiaeSet
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEGetMinutiaeSetProcess(), args);
        }
    }
    class FRAProcess_MEGetMinutiaeSetProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PointLocationSet REs;
        PointLocationSet BIs;
        OrientationGraph OG;
        BinaryGraph BG;
        Integer W;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.REs = inputter.GetArg<PointLocationSet>("过滤后的REs");
            this.BIs = inputter.GetArg<PointLocationSet>("过滤后的BIs");
            this.BG = inputter.GetArg<BinaryGraph>("OS2二值图");
            this.W = inputter.GetArg<Integer>("W");
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
        }
        MinutiaeSet MS;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("细节点集", this.MS);
        }

        public override void Procedure()
        {
            this.MS = new MinutiaeSet();
            foreach (var location in this.REs)
            {
                this.MS.Add(location.I, location.J, GetTheta(this.BG, location, this.W.Value,this.OG.Value[location.I][location.J]), MinutiaeType.RE);
            }
            foreach (var location in this.BIs)
            {
                this.MS.Add(location.I, location.J, GetTheta(this.BG, location, this.W.Value, this.OG.Value[location.I][location.J]), MinutiaeType.BI);
            }
        }
        static int fillIdent = 2;
        public static double GetTheta(BinaryGraph bg, PointLocation location,int w,double orientation)
        {
            var cur = new PointLocationSet();
            cur.Add(location);
            for (int i = 0; i < w; i++)
            {
                var newCur = new PointLocationSet();
                foreach (var pl in cur)
                {
                    if (bg.ValueGet(pl) == fillIdent || bg.ValueGet(pl) == 1)
                    {
                        continue;
                    }
                    bg.ValueSet(pl, fillIdent);
                    Drawing2.EightConnectionArea((deltai, deltaj, k) =>
                    {
                        newCur.Add(pl.I + deltai, pl.J + deltaj);
                    });
                }
                cur = newCur;
            }
            var win = Window.GetSquare(location.I, location.J, w);
            int count = 0;
            try{
                cur.ForEach((tpl) =>
                {
                    var ti = tpl.I;
                    var tj = tpl.J;
                    count += (((tj - location.J) * Math.Cos(orientation) + (location.I - ti) * Math.Sin(orientation)) > 0 ? 1 : -1) * bg.Value[ti][tj];
                });
            }
            catch{}
            return count > 0 ? orientation : orientation + Math.PI;
        }
    }

}
