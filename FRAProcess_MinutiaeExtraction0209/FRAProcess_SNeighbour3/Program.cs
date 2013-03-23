using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_SNeighbour3
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SNeighbour3Process(), args);
        }
    }
    class FRAProcess_SNeighbour3Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        BinaryGraph N3;
        Integer W;
        Integer T;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.N3 = inputter.GetArg<BinaryGraph>("三邻点图");
            this.W = inputter.GetArg<Integer>("W");
            this.T = inputter.GetArg<Integer>("阀值");
        }
        SegmentationArea SA;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割区域", this.SA);
        }

        public override void Procedure()
        {
            this.SA = new SegmentationArea();
            this.SA.Allocate(this.N3);
            this.SA.FindAny((ti, tj) =>
            {
                var win = Window.GetSquare(ti, tj, this.W.Value, this.SA);
                var count = 0;
                win.ForEach((tti,ttj)=>{
                    count++;
                });
                this.SA.Value[ti][tj] = count > this.T.Value ? 0 : 1;
                return false;
            });
        }
    }

}
