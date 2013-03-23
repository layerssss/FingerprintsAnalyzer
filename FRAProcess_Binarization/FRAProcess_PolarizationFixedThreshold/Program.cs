using System;
using System.Collections.Generic;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_Polarization
{
    class Program
    {
        static int Main(string[] args)
        {
            FTP p = new FTP();
            return p.Execute(args);
        }
    }
    class FTP : FRAProcess.Processes.Polarization
    {
        DoubleFloat Threadshold;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            base.Input(inputter);
            Threadshold = inputter.GetArg<DoubleFloat>("灰度阀值");
        }
        public override int Polarize(int i, int j)
        {
            return (OriginalImg.Value[i][j] > Threadshold.Value) ? 1 : 0;
        }
    }
}
