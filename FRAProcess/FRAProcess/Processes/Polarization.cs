using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRAProcess.Processes
{
    public abstract class Polarization : Binarization
    {
        public override void Procedure()
        {
            base.Procedure();
            for (int i = 0; i < OriginalImg.Height; i++)
            {
                for (int j = 0; j < OriginalImg.Width; j++)
                {
                    Result.Value[i][j] = Polarize(i, j);
                    //Result.Value[i][j] = (OriginalImg.GrayLeval[i][j] > Threadshold.Value) ? 1 : 0;
                }
                //Console.WriteLine("已完成" + i + "/" + OriginalImg.Height);
            }
        }
        public abstract int Polarize(int i, int j);
    }
}
