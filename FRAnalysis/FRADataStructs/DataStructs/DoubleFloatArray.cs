using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class DoubleFloatArray:Abstract.Array<double>,IDataStruct
    {
        public void DrawFigure(System.Drawing.Image img,System.Drawing.Pen pen,bool connected)
        {
            this.DrawFigure(img, pen, this.Min(), this.Max(), connected);
        }
        public void DrawFigure(System.Drawing.Image img, System.Drawing.Pen pen, double yBase, double yMax, bool connected)
        {

            int h = img.Height - 40;
            int w = img.Width - 40;
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            g.FillRectangle(System.Drawing.Brushes.LightGray, 0, 0, img.Width, img.Height);
            g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Black, 2), 20, 20, w, h);
            if (!connected)
            {
                this.DrawFigurePoints(img, pen, yBase, yMax);
                return;
            }
            double lastValue = this[0];
            for (int i = 1; i < this.Count; i++)
            {
                Classes.Drawing2.DrawLine(g, pen,
                    i * w / this.Count,
                    (int)((lastValue - yBase) * h / (yMax - yBase)),
                    (i + 1) * w / this.Count,
                    (int)((this[i] - yBase) * h / (yMax - yBase)),
                    20,
                    20,
                    img.Height);
                lastValue = this[i];
            }
            
        }
        void DrawFigurePoints(System.Drawing.Image img, System.Drawing.Pen pen, double yBase, double yMax)
        {

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            int h = img.Height - 40;
            int w = img.Width - 40;
            for (int i = 1; i < this.Count; i++)
            {
                Classes.Drawing2.DrawPoint(g,pen,
                    (i + 1) * w / this.Count,
                    (int)((this[i] - yBase) * h / (yMax - yBase)),
                    20,
                    20,
                    img.Height);
            }
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                writer.Write(this[i]);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            Allocate(reader.ReadInt32());
            for (int i = 0; i < this.Count; i++)
            {
                this[i] = reader.ReadDouble();
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormDoubleFloatArray f = new Controls.FormDoubleFloatArray();
            f.LoadData(this, dataIdent);
            f.ShowDialog();
            return false;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
