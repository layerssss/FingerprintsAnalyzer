using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs.Abstract
{
    public abstract class DoubleFloatGraph:Graph<double>,IDataStruct
    {

        public double AVG()
        {
            double sum = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (double.IsInfinity(this.Value[i][j]) || double.IsNaN(this.Value[i][j]))
                    {
                        continue;
                    }
                    sum += this.Value[i][j];
                }
            }
            return sum / (Height * Width);
        }
        public PointLocation GetMAXIMA(int W)
        {
            double maxValue = 0;
            int maxI = 0;
            int maxJ = 0;
            this.FindAny((ti, tj) =>
            {
                double sumValue = 0;
                int countValue = 0;
                Window win = Window.GetSquare(ti, tj, W, this);
                win.ForEach((tii, tjj) =>
                {
                    sumValue += this.Value[tii][tjj];
                    countValue++;
                });
                if (sumValue / countValue > maxValue)
                {
                    maxValue = sumValue / countValue;
                    maxI = ti;
                    maxJ = tj;
                }
                return false;
            });
            return new PointLocation(maxI, maxJ);
        }

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            DoubleFloatGraph og = this;
            writer.Write(og.Width);
            writer.Write(og.Height);
            for (int i = 0; i < og.Height; i++)
            {
                for (int j = 0; j < og.Width; j++)
                {
                    writer.Write(og.Value[i][j]);
                }
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            DoubleFloatGraph og = this;
            og.Width = reader.ReadInt32();
            og.Height = reader.ReadInt32();
            og.Value = new double[og.Height][];
            for (int i = 0; i < og.Height; i++)
            {
                og.Value[i] = new double[og.Width];
                for (int j = 0; j < og.Width; j++)
                {
                    og.Value[i][j] = reader.ReadDouble();
                }
            }
        }

        public abstract bool Present(GrayLevelImage originalImg, string dataIdent);

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
