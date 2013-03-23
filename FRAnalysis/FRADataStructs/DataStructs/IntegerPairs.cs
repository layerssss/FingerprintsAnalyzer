using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace FRADataStructs.DataStructs
{
    public class IntegerPairs : List<IntegerPair>, IDataStruct
    {
        public void Draw(Bitmap img)
        {
            int maxA;
            int minA;
            int maxB;
            int minB;
            int width;
            int height;
            maxA = this.Max(tpair => tpair.A);
            minA = this.Min(tpair => tpair.A);
            maxB = this.Max(tpair => tpair.B);
            minB = this.Min(tpair => tpair.B);
            if (maxA < 0) { maxA = 0; }
            if (maxB < 0) { maxB = 0; }
            if (minA > 0) { minA = 0; }
            if (minB > 0) { minB = 0; }
            width = img.Width - 21;
            height = img.Height - 21;
            Func<int, int> GetX = (b) =>
            {
                return 10 + (int)Math.Round((double)(b - minB) * width / (maxB - minB));
            };
            Func<int, int> GetY = (a) =>
            {
                return 10 + (int)Math.Round((double)(a - minA) * height / (maxA - minA));
            };
            Graphics g = Graphics.FromImage(img);
            g.FillRectangle(Brushes.White, 0, 0, img.Width, img.Height);
            #region 画网格
            for (var a = minA; a <= maxA; a++)
            {
                var y = GetY(a);
                g.DrawLine(new Pen(Color.LightGray), 0, y, img.Width - 1, y);
            }
            for (var b = minB; b <= maxB; b++)
            {
                var x = GetX(b);
                g.DrawLine(new Pen(Color.LightGray), x, 0, x, img.Height - 1);
            }
            #endregion
            #region 画原点
            g.DrawEllipse(new Pen(Color.Red), GetX(0) - 10, GetY(0) - 10, 20, 20);
            #endregion
            foreach (var pair in this)
            {
                g.DrawEllipse(new Pen(Color.Blue), GetX(pair.B) - 5, GetY(pair.A) - 5, 10, 10);
            }

        }
        public void GetData(int x, int y, out int a, out int b,Image img)
        {
            int maxA;
            int minA;
            int maxB;
            int minB;
            int width;
            int height;
            maxA = this.Max(tpair => tpair.A);
            minA = this.Min(tpair => tpair.A);
            maxB = this.Max(tpair => tpair.B);
            minB = this.Min(tpair => tpair.B);
            if (maxA < 0) { maxA = 0; }
            if (maxB < 0) { maxB = 0; }
            if (minA > 0) { minA = 0; }
            if (minB > 0) { minB = 0; }
            width = img.Width - 21;
            height = img.Height - 21;
            a = (int)Math.Round((double)(y - 10) * (maxA - minA) / (height) + minA);
            b = (int)Math.Round((double)(x - 10) * (maxB - minB) / (width) + minB);
        }
        public void Add(int a, int b)
        {
            this.Add(new IntegerPair(a, b));
        }
        public new void Add(IntegerPair pair)
        {
            if (this.Contains(pair))
            {
                throw(new Exception("尝试添加重复的元素"));
            }
            base.Add(pair);
        }

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (var pair in this)
            {
                writer.Write(pair.A);
                writer.Write(pair.B);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Clear();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                this.Add(reader.ReadInt32(), reader.ReadInt32());
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormIntegerPairs f = new Controls.FormIntegerPairs();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class IntegerPair:IEquatable<IntegerPair>
    {
        public IntegerPair(int a, int b)
        {
            this.A = a;
            this.B = b;
        }
        public int A=0;
        public int B=0;

        #region IEquatable<IntegerPair> 成员

        public bool Equals(IntegerPair other)
        {
            return this.A == other.A && this.B == other.B;
        }

        #endregion
    }
}
