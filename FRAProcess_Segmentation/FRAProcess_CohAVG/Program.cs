using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_CohAVG
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_CohAVGProcess(), args);
        }
    }
    class FRAProcess_CohAVGProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        GrayLevelImage coh;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            this.coh = inputter.GetArg<GrayLevelImage>("连贯性");
        }
        SegmentationArea sa;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<SegmentationArea>("切割", sa);
        }

        public override void Procedure()
        {
            double avg = coh.AVG();
            Console.Write(avg.ToString());
            sa = new SegmentationArea();
            sa.Allocate(coh.Width, coh.Height);
            for (int i = 0; i < coh.Height; i++)
            {
                for (int j = 0; j < coh.Width; j++)
                {
                    if (double.IsNaN(coh.Value[i][j]))
                    {
                        sa.Value[i][j] = 0;
                        continue; 
                    }
                    sa.Value[i][j] = coh.Value[i][j] < avg ? 0 : 1;
                }
            }
            IntergerGraph tsa = sa.Clone();
            tsa.Fill(0, 0, 0, 0, tsa.Height, tsa.Width, 0, 1);
            this.sa.Accumulate((ti, twi) => ti - twi + 1, tsa);
        }
    }

}
