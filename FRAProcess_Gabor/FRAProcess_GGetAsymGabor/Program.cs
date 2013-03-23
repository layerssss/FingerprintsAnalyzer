using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GGetAsymGabor
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GGetAsymGaborProcess(), args);
        }
    }
    class FRAProcess_GGetAsymGaborProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
        }
        FilterSet fs;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("AGF8-7", this.fs);
        }

        public override void Procedure()
        {
            this.fs = new FilterSet();
            for (var qtheta = 0; qtheta < 8; qtheta++)
            {
                var theta = Math2.PI(qtheta * 0.125);
                fs.Add(Filter.GetFilter(10, (deltai, deltaj) =>
                {
                    var sin = Math.Sin(theta);
                    var cos = Math.Cos(theta);
                    var xtheta = deltaj * sin + deltai * cos;
                    var ytheta = -deltaj * cos + deltai * sin;
                    var cos2pifx = Math.Cos(Math2.PI(2 * 0.1 * xtheta));

                    return (ytheta > 0 ? 1 : -1) * Math.Exp(-0.5 * (Math2.Sqr(xtheta) / 25 + Math2.Sqr(ytheta) / 25))
                     * cos2pifx;
                }));
            }
        }
    }

}
