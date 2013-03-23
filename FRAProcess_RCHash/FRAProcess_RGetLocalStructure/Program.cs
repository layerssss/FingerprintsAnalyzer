using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_RGetLocalStructure
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_RGetLocalStructureProcess(), args);
        }
    }
    class FRAProcess_RGetLocalStructureProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        LSArray sArr;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.sArr = inputter.GetArg<LSArray>("RC支持矩阵");
        }
        LocalStructureSet LSS = new LocalStructureSet();
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("0411局部结构", this.LSS);
        }

        public override void Procedure()
        {
            int count = sArr.Width;
            for (int i = 0; i < count; i++)
            {
                var baseP = this.sArr.Value[i][(i+1)%count]["PI"] as Minutiae;
                var lS = new LocalStructure();
                var data = new AngleRCSet();

                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var p = this.sArr.Value[i][j]["PJ"] as Minutiae;
                    var rc = this.sArr.Value[i][j]["RC"] as DoubleFloat;
                    var s = this.sArr.Value[i][j]["SUPPORT"] as DoubleFloat;
                    if (s.Value < 4)
                    {
                        continue;
                    }
                    var angle = (Math.Atan2(baseP.Location.I - p.Location.I, p.Location.J - baseP.Location.J) - baseP.Angle + Math2.PI(4)) % Math2.PI(2);
                    data.Add(angle, (int)rc.Value, (p.Angle - baseP.Angle + Math2.PI(2)) % Math2.PI(2));
                }


                #region 记录数据
                lS.Add("BASE", baseP);
                lS.Add("DATA", data);
                lS.PrimaryData = baseP;
                if (data.Count >= 4)
                {
                    this.LSS.Add(lS);
                }

                #endregion
            }
        }
    }

}
