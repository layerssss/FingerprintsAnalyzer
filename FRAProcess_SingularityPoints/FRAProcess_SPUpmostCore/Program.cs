using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_SPUpmostCore
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPUpmostCoreProcess(), args);
        }
    }
    class FRAProcess_SPUpmostCoreProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        PoincareIndexes pi;
        Integer ijmax;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.pi = inputter.GetArg<PoincareIndexes>("庞加莱指数");
            this.ijmax = inputter.GetArg<Integer>("最大容忍区域");
        }
        PointLocation result;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("参考点", result);
        }

        public override void Procedure()
        {
            List<int> listI = new List<int>();
            List<int> listJ = new List<int>();
            bool found = false;
            for (int i = 0; i < pi.Height; i++)
            {
                for (int j = 0; j < pi.Width; j++)
                {
                    if(pi.Value[i][j]!=-1&&pi.Value[i][j]!=-2)
                    {
                        continue;
                    }
                    if (found)
                    {
                        if (Math.Abs(i - listI[0]) > ijmax.Value)
                        {
                            break;
                        }
                        if (Math.Abs(j - listJ[0]) > ijmax.Value)
                        {
                            break;
                        }
                    }
                    else
                    {
                        found = true;
                    }
                    listI.Add(i);
                    listJ.Add(j);
                }
            }
            try
            {
                this.result = new PointLocation()
                {
                    I = (int)listI.Average(),
                    J = (int)listJ.Average()
                };
            }
            catch {
                this.result = new PointLocation();
                this.result.BuildInstance();
            }
        }
    }

}
