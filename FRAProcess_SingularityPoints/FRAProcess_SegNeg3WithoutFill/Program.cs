using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_SegNeg3WithoutFill
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SegNeg3WithoutFillProcess(), args);
        }
    }
    class FRAProcess_SegNeg3WithoutFillProcess : FRAProcess.FRAProcess
    {
        PoincareIndexes pi;
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.pi = inputter.GetArg<PoincareIndexes>("用负三标记可能是无效区域的庞卡莱指数图");
        }
        SegmentationArea sa;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("切割结果", sa);
        }

        public override void Procedure()
        {
            this.sa = new SegmentationArea();
            this.sa.Allocate(pi.Width, pi.Height);
            for (int i = 0; i < pi.Height; i++)
            {
                for (int j = 0; j < pi.Width; j++)
                {
                    if (this.pi.Value[i][j] != -3)
                    {
                        this.sa.Value[i][j] = 1;
                    }
                }
            }
        }
    }

}
