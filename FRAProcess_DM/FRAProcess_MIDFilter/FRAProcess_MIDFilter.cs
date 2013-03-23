using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MIDFilter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MIDFilterProcess(), args);
        }
    }
    class FRAProcess_MIDFilterProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        Integer w;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.w = inputter.GetArg<Integer>("W");
        }

        GrayLevelImage filted;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("中值滤波结果", this.filted);
        }

        public override void Procedure()
        {
            var list = new List<double>();
            this.filted = new GrayLevelImage();
            this.filted.Allocate(this.img);
            this.filted.ForEach((ti, tj) =>
            {
                var win = Window.GetSquare(ti, tj, this.w.Value);
                if (!win.IsIn(this.img))
                {
                    return;
                }
                list.Clear();
                win.ForEach(this.img, tv =>
                {
                    list.Add(tv);
                });
                list.Sort();
                this.filted.Value[ti][tj] = list[(this.w.Value * 2 + 1) * this.w.Value + this.w.Value];
            });
        }
    }

}
