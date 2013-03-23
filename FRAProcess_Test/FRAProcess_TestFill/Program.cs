using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAProcess_TestFill
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
        FRADataStructs.DataStructs.BinaryGraph b;
        public override void Procedure()
        {
            int o = b.Value[0][0];
            int n=o == 0 ? 1 : 0;
            b.Fill(0, 0, 0, 0, b.Height, b.Width, o, n);
            Console.WriteLine(o + "=>" + n);
        }

        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            b = inputter.GetArg<FRADataStructs.DataStructs.BinaryGraph>("二值图");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<FRADataStructs.DataStructs.BinaryGraph>("填充结果", b);
        }
    }
}
