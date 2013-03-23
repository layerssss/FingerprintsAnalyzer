using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;

namespace FRAProcess_PolarizationDynamicThreshold_AVG
{
    class Program
    {
        static int Main(string[] args)
        {
            AVG p = new AVG();
            return p.Execute(args);
        }
    }
    class AVG :FRAProcess.Processes.Polarization
    {
        public Integer W;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            base.Input(inputter);
            W = inputter.GetArg<Integer>("窗口宽度的一半");
        }
        public override int Polarize(int i, int j)
        {
            double sum = 0;
            int count = 0;
            for (int iWindow = i - W.Value; iWindow < i + W.Value; iWindow++)
            {
                if (iWindow < 0 || iWindow >= OriginalImg.Height)//超出界限的忽略
                {
                    continue;
                }
                for (int jWindow = j - W.Value; jWindow < j + W.Value; jWindow++)
                {
                    if (jWindow < 0 || jWindow >= OriginalImg.Width)//超出界限的忽略
                    {
                        continue;
                    }
                    sum += OriginalImg.Value[iWindow][jWindow];
                    count++;
                }
            }
            double threadshold = sum / count;
            return (OriginalImg.Value[i][j] > threadshold) ? 1 : 0;
        }

    }
}
