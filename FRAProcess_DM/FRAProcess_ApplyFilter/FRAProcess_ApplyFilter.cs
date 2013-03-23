using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_ApplyFilter
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_ApplyFilterProcess(), args);
        }
    }
    class FRAProcess_ApplyFilterProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage img;
        Filter filter;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.img = inputter.GetArg<GrayLevelImage>("原图像");
            this.filter = inputter.GetArg<Filter>("需要使用的滤波器");
        }
        GrayLevelImage filted;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("滤波结果", this.filted);
        }

        public override void Procedure()
        {
            this.filted = new GrayLevelImage();
            this.filted.Allocate(this.img);
            this.img.ForEach((ti, tj, tv) =>
            {
                var win = Window.GetSquare(ti, tj, this.filter.W);
                if (!win.IsIn(this.img))
                {
                    return;
                }
                win.ForEach((i, j) =>
                {
                    this.filted.Value[ti][tj] += this.img.Value[i][j] * this.filter.Value[i - win.IMin][j - win.JMin];
                });
                if (this.filted.Value[ti][tj] > 1)
                {
                    this.filted.Value[ti][tj] = 1;
                }
                if (this.filted.Value[ti][tj] < 0)
                {
                    this.filted.Value[ti][tj] = 0;
                }
            });
        }
    }

}
