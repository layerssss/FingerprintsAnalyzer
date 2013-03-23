using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace FRADataStructs.DataStructs
{
    public class DoubleFloatArraySeq : List<DoubleFloatArray>,IDataStruct
    {
        public Bitmap Draw(out double maxValue,out double minValue,out int maxArrLen)
        {
            maxValue = this.Max<DoubleFloatArray>(tarr => tarr.Any() ? tarr.Max() : 0);
            minValue = this.Min<DoubleFloatArray>(tarr => tarr.Any() ? tarr.Min() : 0);
            maxArrLen = (int)this.Max<DoubleFloatArray>(tarr => tarr.Count);
            if (maxArrLen == 0)
            {
                Bitmap b1 = new Bitmap(this.Count, 50);
                Graphics g1 = Graphics.FromImage(b1);
                g1.FillRectangle(Brushes.Red, 0, 0, b1.Width, b1.Height);
                return b1;
            }
            Bitmap b = new Bitmap(this.Count, maxArrLen);
            Graphics g = Graphics.FromImage(b);
            g.FillRectangle(Brushes.Red, 0, 0, b.Width, b.Height);
            for (int x = 0; x < this.Count; x++)
            {
                for (int y = 0; y < this[x].Count; y++)
                {
                    int rgb = (int)((this[x][y] - minValue) * 255 / (maxValue - minValue));
                    b.SetPixel(x, b.Height - 1 - y,
                            Color.FromArgb(rgb, rgb, rgb));
                }
            }
            return b;
        }
        public void Allocate(int n,int arrLen,int arrValue)
        {
            this.Clear();
            for (int i = 0; i < n; i++)
            {
                DoubleFloatArray arr = new DoubleFloatArray();
                arr.Allocate(arrLen, arrLen);
                this.Add(arr);
            }
        }
        public void Allocate(int n, int arrLen)
        {
            this.Allocate(n, arrLen, 0);
        }
        public void Allocate(int n)
        {
            this.Allocate(n, 0);
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (DoubleFloatArray arr in this)
            {
                arr.Serialize(writer);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            int j = reader.ReadInt32();
            this.Clear();
            for (int i = 0; i < j; i++)
            {
                DoubleFloatArray arr = new DoubleFloatArray();
                arr.Deserialize(reader);
                this.Add(arr);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormDoubleFloatArraySeq f = new Controls.FormDoubleFloatArraySeq();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
