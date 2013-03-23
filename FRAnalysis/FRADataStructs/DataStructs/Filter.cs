using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class Filter :Abstract.Graph<double>, IDataStruct
    {
        public int W;
        public void Allocate(int w,double defaulVal)
        {
            this.W = w;
            base.Allocate(w * 2 + 1, w * 2 + 1, defaulVal);
        }
        public void Allocate(int w)
        {
            this.Allocate(w, default(double));
        }
        public double GetOutput(Abstract.Graph<double> graph, int i, int j)
        {
            var sum = 0.0;
            for (var ti = -this.W; ti <= this.W; ti++)
            {
                for (var tj = -this.W; tj <= this.W; tj++)
                {
                    sum += graph.Value[i + ti][j + tj] * this.Value[ti + W][tj + W];
                }
            }
            return sum;
        }

        public static Filter GaborFilter(int w, double f, double theta, double deltaXsq, double deltaYsq)
        {
            var sin = Math.Sin(theta);
            var cos = Math.Cos(theta);
            return GetFilter(w, (i, j) =>
            {
                var xtheta = j * sin + i * cos;
                var ytheta = -j * cos + i * sin;
                var cos2pifx = Math.Cos(Classes.Math2.PI(2 * f * xtheta));
                return Math.Exp(-0.5 * (Classes.Math2.Sqr(xtheta) / deltaXsq + Classes.Math2.Sqr(ytheta) / deltaYsq))
                    * cos2pifx;
            });
        }
        public static Filter GetFilter(int w, Func<int, int, double> exp)
        {
            var filter = new Filter();
            filter.Allocate(w);
            for (var j = -w; j <= w; j++)
            {
                for (var i = -w; i <= w; i++)
                {
                    filter.ValueSet(i + w, j + w,
                        exp(i, j));
                }
            }
            return filter;
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Width);
            writer.Write(this.Height);
            this.FindAny((ti, tj, tv) =>
            {
                writer.Write(tv);
                return false;
            });
        }
        public void Draw(GrayLevelImage img)
        {
            img.Allocate(this);
            var max = this.Value.Max(tds => tds.Max());
            var min = this.Value.Min(tds => tds.Min());
            var range = max - min;
            this.FindAny((ti, tj, tv) =>
            {
                img.ValueSet(ti, tj, (tv - min) / range);
                return false;
            });
        }
        public void Deserialize(System.IO.BinaryReader reader)
        {
            base.Allocate(reader.ReadInt32(),reader.ReadInt32());
            this.W = (base.Width-1)/2;
            this.FindAny((ti, tj) =>
            {
                this.ValueSet(ti, tj, reader.ReadDouble());
                return false;
            });
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            var img =new GrayLevelImage();
            this.Draw(img);
            return img.Present(originalImg, dataIdent + " min=" + this.Value.Min(tds => tds.Min()) + " max=" + this.Value.Max(tds => tds.Max()));
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
