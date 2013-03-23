#define TRAINMIN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_PolarizationDynamicThreshold_FillingCount
{
    class Program
    {
        static int Main(string[] args)
        {
            FillingCount p = new FillingCount();
            return p.Execute(args);
        }
    }
    public class FillingCount : FRAProcess.Processes.Polarization
    {
        public Integer FCWindowWidth;
        public Integer FCMin;
        public DoubleFloat DeltaThreadshold;
        double threadshold = 0.0;//阀值初始训练值
        BinaryGraph fCTemp = new BinaryGraph();//计算填充次数用的临时二值图
        BinaryGraph threadsholdTrainTemp = new BinaryGraph();//训练阀值时的效果预览临时二值图
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            base.Input(inputter);
            this.FCWindowWidth = inputter.GetArg<Integer>("FC窗口宽度的一半");
            this.FCMin = inputter.GetArg<Integer>("FC最小值");
            this.DeltaThreadshold = inputter.GetArg<DoubleFloat>("阀值训练速度");
        }
        double threadMin;
        public override void Procedure()
        {
            fCTemp.Allocate(OriginalImg.Width, OriginalImg.Height);
            threadsholdTrainTemp.Allocate(OriginalImg.Width, OriginalImg.Height);
            this.threadMin = OriginalImg.AVG()*0.3;
            base.Procedure();
        }
        public override int Polarize(int i, int j)
        {
            #region 生成窗口
            int iMin = i - FCWindowWidth.Value;
            if (iMin < 0) { iMin = 0; }
            int jMin = j - FCWindowWidth.Value;
            if (jMin < 0) { jMin = 0; }
            int iMax = i + FCWindowWidth.Value;
            if (iMax > fCTemp.Height) { iMax = fCTemp.Height; }
            int jMax = j + FCWindowWidth.Value;
            if (jMax > fCTemp.Width) { jMax = fCTemp.Width; }
            #endregion
            double oldt = threadshold;
            while (true)
            {
                #region 生成效果预览
                for (int iWindow = iMin; iWindow < iMax; iWindow++)
                {
                    for (int jWindow = jMin; jWindow < jMax; jWindow++)
                    {
                        threadsholdTrainTemp.Value[iWindow][jWindow] =
                            (OriginalImg.Value[iWindow][jWindow] > threadshold ? 1 : 0);
                    }
                }
                #endregion
                #region 评估生成的效果
                int fC = threadsholdTrainTemp.FillCount(iMin, jMin, iMax, jMax, 0, 1, this.fCTemp);
                #endregion
                #region 微移
                if (fC >= FCMin.Value)
                {
                    int a = (OriginalImg.Value[i][j] > threadshold ? 1 : 0);
                    threadshold -= DeltaThreadshold.Value;//退避
                    oldt = threadshold;
                    return a;
                }
                threadshold += DeltaThreadshold.Value;
                #region 调试：溢出保护
                if (threadshold > 1)
                {
                    Interupt("ThreadShold已溢出");
                    return 0;
                } 
                #endregion
                if (threadshold < threadMin)
                {
                    Console.Write("ThreadShold已回滚");
                    threadshold = oldt;
                    return 0;
                }
                #endregion
            }
        }
    }
}
