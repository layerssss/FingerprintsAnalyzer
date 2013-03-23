using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_TestAtan2
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_TestAtan2Process(), args);
        }
    }
    class FRAProcess_TestAtan2Process : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            
        }
        DoubleFloatArray arr;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("样本", arr);
        }

        public override void Procedure()
        {
            arr = new DoubleFloatArray();
            for (double i = 0; i < 100; i++)
            {
                arr.Add(Math.Atan2(-Math.Sin(Math.PI*i / 50), -Math.Cos(Math.PI *i/ 50 ))+Math.PI);
            }
        }
    }

}
