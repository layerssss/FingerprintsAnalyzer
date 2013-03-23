using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_GetStabSet
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetStabSetProcess(), args);
        }
    }
    class FRAProcess_GetStabSetProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloat deltaTheta;
        Integer length;
        Integer startLength;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            deltaTheta = inputter.GetArg<DoubleFloat>("Theta增量");
            length = inputter.GetArg<Integer>("针刺长度");
            this.startLength = inputter.GetArg<Integer>("开始长度");
        }
        StabSet stabSet;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("针刺", stabSet);
        }

        public override void Procedure()
        {
            this.stabSet = new StabSet();
            List<PointLocationSet> lpls = new List<PointLocationSet>();
            for (double theta = 0; theta < Math.PI*2; theta += deltaTheta.Value)
            {
                PointLocationSet pls = new PointLocationSet();
                FRADataStructs.DataStructs.Classes.Math2.DDALineEach(
                    (int)(Math.Cos(theta) * (startLength.Value) + 0.5),
                    (int)(Math.Sin(theta) * (startLength.Value) + 0.5),
                    (int)(Math.Cos(theta) * (startLength.Value + length.Value) + 0.5),
                    (int)(Math.Sin(theta) * (startLength.Value + length.Value) + 0.5),
                    (x, y) => pls.Add(new PointLocation(-y, x)));
                lpls.Add(pls);
            }
            PointLocationSet lastpls = lpls[0];
            for (int i = 1; i < lpls.Count; i++)
            {
                if (!lpls[i].Equals(lastpls))
                {
                    this.stabSet.Add(lpls[i]);
                    lastpls = lpls[i];
                }
            }
        }
    }

}
