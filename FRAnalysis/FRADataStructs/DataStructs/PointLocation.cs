using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class PointLocation:IDataStruct,IEquatable<PointLocation>
    {
        public PointLocation()
            :this(0,0)
        {
        }
        public PointLocation(int i, int j)
        {
            this.I = i;
            this.J = j;
        }
        public bool CloseToBoundary<T>(Abstract.Graph<T> graph, int padding)
        {
            return CloseToBoundary(graph, padding, this.I, this.J);
        }
        public static bool CloseToBoundary<T>(Abstract.Graph<T> graph, int padding, int i, int j)
        {
            return i < padding || i + padding >= graph.Height || j < padding || j + padding >= graph.Width;
        }
        public int RidgeCountDDA(BinaryGraph binaryGraph, int minInteval, int i, int j, Func<int, int, bool> cancleCondition, out bool cancled,int tinyIntervalCountMax)
        {
            double increI, increJ,curI,curJ;
            int maxLen, lastI, lastJ,count,inteval,tinyIntevalCount;
            bool lastBlack;
            {//初始化
                tinyIntevalCount = 0;
                cancled = false;
                lastBlack = true;
                count = 0;
                inteval = 0;
                var deltaI = i - this.I;
                var deltaJ = j - this.J;
                maxLen=(int)Math.Round(Math.Sqrt(deltaI * deltaI + deltaJ * deltaJ));
                maxLen=(int)Math.Round(Math.Sqrt(deltaI * deltaI + deltaJ * deltaJ));
                increI = (double)deltaI / (double)maxLen;
                increJ = (double)deltaJ / (double)maxLen;
                curI = (double)this.I;
                curJ = (double)this.J;
                lastI = (int)Math.Round(curI);
                lastJ = (int)Math.Round(curJ);
            }
            for (int len = 1; len <= maxLen; len++)
            {
                curI += increI;
                curJ += increJ;
                var nowI = (int)Math.Round(curI);
                var nowJ = (int)Math.Round(curJ);
                if (cancleCondition(nowI, nowJ))
                {
                    cancled = true;
                    return 0;
                }
                var nowBlack = false;
                if (binaryGraph.Value[nowI][nowJ] == 0)
                {
                    nowBlack = true;
                }
                bool intoBlack = false;
                if (!lastBlack && nowBlack)
                {
                    intoBlack = true;
                }
                if (lastI != nowI && lastJ != nowJ)//交叉
                {
                    if (!nowBlack && binaryGraph.Value[lastI][lastJ] == 1)
                    {
                        if (binaryGraph.Value[lastI][nowJ] == 0 && binaryGraph.Value[nowI][lastJ] == 0)
                        {
                            intoBlack = true;
                        }
                    }
                }
                if (intoBlack)
                {
                    if (inteval > minInteval)
                    {
                        count++;
                        tinyIntevalCount = 0;
                    }
                    else
                    {
                        tinyIntevalCount++;
                        if (tinyIntevalCount >= tinyIntervalCountMax)
                        {
                            cancled = true;
                            return 0;
                        }
                    }
                    inteval = 0;
                }
                lastBlack = nowBlack;
                inteval++;
                lastI = nowI;
                lastJ = nowJ;
            }

            return count;
        }
        public int RidgeCountDDA(BinaryGraph binaryGraph, int minInteval, PointLocation another, Func<int, int, bool> cancleCondition, out bool cancled, int tinyIntervalCountMax)
        {
            return this.RidgeCountDDA(binaryGraph,minInteval, another.I, another.J, cancleCondition,out cancled,tinyIntervalCountMax);
        }
        public int I;
        public int J;
        public void Draw(System.Drawing.Image img,System.Drawing.Color color)
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            g.DrawLine(new System.Drawing.Pen(color, 2), this.J - 8, this.I - 8, this.J + 8, this.I + 8);
            g.DrawLine(new System.Drawing.Pen(color, 2), this.J + 8, this.I - 8, this.J - 8, this.I + 8);
        }
        public int Distance(PointLocation another)
        {
            return (int)Math.Round(Math.Sqrt((this.I - another.I) * (this.I - another.I) + (this.J - another.J) * (this.J - another.J)));
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(I);
            writer.Write(J);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.I = reader.ReadInt32();
            this.J = reader.ReadInt32();

        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormPointLocation f = new Controls.FormPointLocation();
            f.LoadData(this, dataIdent, originalImg);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            return new PointLocation() { I = 0, J = 0 };
        }

        #endregion

        #region IEquatable<PointLocation> 成员

        public bool Equals(PointLocation other)
        {
            return this.I == other.I && this.J == other.J;
        }

        #endregion
    }
}
