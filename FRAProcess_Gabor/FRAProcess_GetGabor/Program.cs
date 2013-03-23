using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetGabor
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetGaborProcess(), args);
        }
    }
    class FRAProcess_GetGaborProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
        }
        FilterSet f;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("TGF8-086", this.f);
        }

        public override void Procedure()
        {
            this.f = new FilterSet();
            var freqs = new[] { 0.1, 0.125, 0.13333 };
            for (int qfreq = 0; qfreq < 3; qfreq++)
            {
                var freq = freqs[qfreq];
                for (var qtheta = 0; qtheta < 8; qtheta++)
                {
                    var theta = Math2.PI(qtheta * 0.125);
                    f.Add(Filter.GaborFilter(10, freq, theta, 25,36));
                }
            }
        }
    }

}
