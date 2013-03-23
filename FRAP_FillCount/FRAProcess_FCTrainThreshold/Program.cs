using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;

namespace FRAProcess_FCTrainThreshold
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new p(), args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        Integer initialTFCmin;
        Integer fcMax;
        DoubleFloatArray fcsamples;
        
        DoubleFloat finalT = new DoubleFloat();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            initialTFCmin = inputter.GetArg<Integer>("过亮区间FC最大值");
            fcsamples = inputter.GetArg<DoubleFloatArray>("FC样本");
            fcMax = inputter.GetArg<Integer>("训练约束FC最大值");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<DoubleFloat>("最优T", finalT);
        }

        public override void Procedure()
        {
            int i = 0;
            while ((int)fcsamples[i] < initialTFCmin.Value)
            {
                i++;
                if (i >= fcsamples.Count)
                {
                    throw (new FRAProcess.AlgorithmException("T无法达到initialTFCmin，已溢出！"));
                }
            }
            while((int)fcsamples[i]>fcMax.Value)
            {
                i++;
                if (i >= fcsamples.Count)
                {
                    throw (new FRAProcess.AlgorithmException("T无法达到fcMax，已溢出！"));
                }
            }
            finalT.Value = ((double)i) / (double)(fcsamples.Count - 1);
        }
    }
}
