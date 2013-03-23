using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_TestFCSampling
{
    class Program
    {
        static int Main(string[] args)
        {
            p p = new p();
            return p.Execute(args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        public override void Procedure()
        {
            BinaryGraph fCTemp = new BinaryGraph();
            BinaryGraph bGraph = new BinaryGraph();
            bGraph.Allocate(img.Width, img.Height);
            fCTemp.Allocate(img.Width, img.Height);
            arr.Allocate(srate.Value+1);
            for (int curThreshold = 0; curThreshold <= srate.Value; curThreshold++)
            {
                for (int i = window.IMin; i < window.IMax; i++)
                {
                    for (int j = window.JMin; j < window.JMax; j++)
                    {
                        bGraph.Value[i][j] =
                            (img.Value[i][j] * srate.Value > curThreshold) ? 1 : 0;
                    }
                }
                arr[curThreshold] = bGraph.FillCount(
                    window.IMin,
                    window.JMin,
                    window.IMax,
                    window.JMax,
                    0,
                    1,
                    fCTemp);
            }
        }
        Integer srate;
        GrayLevelImage img;
        Window window;
        DoubleFloatArray arr = new DoubleFloatArray();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            srate = inputter.GetArg<Integer>("取样率");
            img = inputter.GetArg<GrayLevelImage>("灰度图");
            window = inputter.GetArg<Window>("FC计算窗口");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<DoubleFloatArray>("FC样本", arr);
        }
    }
}
