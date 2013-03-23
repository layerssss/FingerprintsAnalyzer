using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_GetPointLocationSet
{
    class Program
    {
        static void Main(string[] args)
        {
            FRAProcess.FRAProcess.Execute(new p(), args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        Integer n;
        List<PointLocation> pll;
        PointLocationSet pls;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            pll=new List<PointLocation>();
            for (int i = 0; i < n.Value; i++)
            {
                PointLocation pl = inputter.GetArg<PointLocation>("点坐标" + (i + 1));
                if (pl != null)
                {
                    pll.Add(pl);
                }
            }
        }
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            n = inputter.GetArg<Integer>("点坐标个数");
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<PointLocationSet>("点坐标集", pls);
        }

        public override void Procedure()
        {
            pls = new PointLocationSet();
            pls.AddRange(this.pll);
        }
    }
}
