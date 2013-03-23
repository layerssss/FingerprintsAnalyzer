using System;
using System.Collections.Generic;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_PoincareIndexesTest
{
    class Program
    {
        static int Main(string[] args)
        {
            piTest p = new piTest();
            return p.Execute(args);
        }
    }
    class piTest : FRAProcess.FRAProcess
    {
        PoincareIndexes pi = new PoincareIndexes();
        OrientationGraph og = new OrientationGraph();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            og = inputter.GetArg<OrientationGraph>("原图像");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<PoincareIndexes>("测试PI", pi);
        }
        public override void Procedure()
        {
            pi.Allocate(og.Width, og.Height);
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    pi.Value[i][j] = (i % 5) - 2;
                }
            }
        }
    }
}
