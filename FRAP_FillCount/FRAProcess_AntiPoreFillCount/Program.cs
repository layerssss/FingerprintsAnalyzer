using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_AntiPoreFillCount
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
        BinaryGraph binaryGraph;
        Window window;
        BinaryGraph fCTemp = new BinaryGraph();
        public override void Procedure()
        {
            fCTemp.Allocate(binaryGraph.Width, binaryGraph.Height);
            Console.Write("FillCount=");
            Console.WriteLine(this.binaryGraph.FillCount(
                window.IMin,
                window.JMin,
                window.IMax,
                window.JMax, 0, 1, fCTemp));
        }

        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.binaryGraph = inputter.GetArg<BinaryGraph>("二值图");
            this.window = inputter.GetArg<Window>("窗口");
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
        }
    }
    
}
