using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_MEGetLocalStructure
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_MEGetLocalStructureProcess(), args);
        }
    }
    class FRAProcess_MEGetLocalStructureProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        OrientationGraph OG;
        BinaryGraph BG;
        MinutiaeSet MS;
        Integer minD;
        Integer tineIntervalCountT;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.OG = inputter.GetArg<OrientationGraph>("初步提取");
            this.BG = inputter.GetArg<BinaryGraph>("细化结果");
            this.MS = inputter.GetArg<MinutiaeSet>("过滤后的细节点集");
            this.minD = inputter.GetArg<Integer>("最小脊线间距");
            this.tineIntervalCountT = inputter.GetArg<Integer>("允许的最大基线间距过小次数");
        }
        LocalStructureSet LSS;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("1102局部结构", this.LSS);
        }

        public override void Procedure()
        {
            this.LSS = new LocalStructureSet();
            foreach (var baseP in this.MS)
            {
                var lS = new LocalStructure();
                var data = new AngleRCSet();
                foreach (var p in this.MS)
                {
                    if(p.Location.Equals(baseP.Location)){
                        continue;
                    }
                    bool cancled;
                    var rc=baseP.Location.RidgeCountDDA(this.BG,this.minD.Value,p.Location,
                        (ti,tj)=>{
                            return false;
                        },
                        out cancled,this.tineIntervalCountT.Value);
                    if (cancled)
                    {
                        continue;
                    }
                    var angle = (Math.Atan2(baseP.Location.I - p.Location.I, p.Location.J - baseP.Location.J) - baseP.Angle + Math2.PI(4)) % Math2.PI(2);
                    data.Add(angle, rc, (p.Angle - baseP.Angle + Math2.PI(2)) % Math2.PI(2));
                }
                
                #region 记录数据
                lS.Add("BASE", baseP);
                lS.Add("DATA", data);
                lS.PrimaryData = baseP;
                if (data.Count > 10)
                {
                    this.LSS.Add(lS);
                }
                
                #endregion
            }
        }
    }

}
