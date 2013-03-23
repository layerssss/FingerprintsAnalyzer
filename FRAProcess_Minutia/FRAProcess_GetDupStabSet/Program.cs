using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetDupStabSet
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetDupStabSetProcess(), args);
        }
    }
    class FRAProcess_GetDupStabSetProcess : FRAProcess.FRAProcess
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
            outputter.PutArg("均匀针刺", stabSet);
        }

        public override void Procedure()
        {
            StabSet lpls = this.stabSet = new StabSet();
            for (double theta = 0; theta <= Math.PI * 2; theta += deltaTheta.Value)
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
        }
    }

}
