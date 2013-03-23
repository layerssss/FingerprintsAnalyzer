using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace FRADataStructs.DataStructs
{
    public class AngleRCSet : List<AngleRC>,IDataStruct
    {
        public void Add(double angle, int rC,double direction)
        {
            this.Add(new AngleRC(angle, rC,direction));
        }
        #region Drawing
        public void Draw(Bitmap img)
        {
            this.DrawingInfo.Initial(img, this);
            this.DrawingInfo.Draw(img);
        }
        public struct AngleRCDawingInfo
        {
            public int Height;
            public int Width;
            public void Initial(Bitmap img, AngleRCSet data)
            {
                this.Height = img.Height - 20;
                this.Width = img.Width - 20;
                this.data = data;

                this.maxRC = data.Max((titem) => titem.RC);
                this.stepY = this.Height / 2 / maxRC;
            }
            AngleRCSet data;
            public string Reverse(Point gPoint)
            {
                int x, y;
                this.ReversePoint(gPoint, out x, out y);
                var rc = (int)Math.Round((double)(y - this.Height / 2) / stepY);
                var angle = (Math.Atan2((double)(y - this.Height / 2), (double)(x - this.Width / 2)) / Math.PI+2) % 2;
                return string.Format("RC={0}  angle={1:F2}xPI maxRC={2}", rc, angle,this.maxRC);
            }
            public void Draw(Bitmap img)
            {
                Graphics g = Graphics.FromImage(img);
                g.FillRectangle(Brushes.White, 0, 0, img.Width, img.Height);
                var grayPen = new Pen(Color.Gray);
                for (int i = 0; i <= this.maxRC; i++)
                {
                    g.DrawLine(grayPen, this.TransformPoint(0, (int)this.Height / 2 - i * this.stepY), this.TransformPoint(this.Width - 1, (int)this.Height / 2 - i * this.stepY));
                    g.DrawLine(grayPen, this.TransformPoint(0, (int)this.Height / 2 + i * this.stepY), this.TransformPoint(this.Width - 1, (int)this.Height / 2 + i * this.stepY));
                }
                var yellowPen = new Pen(Color.Yellow);
                var bluePen = new Pen(Color.Blue);
                var redPen=new Pen(Color.Red);
                foreach (var item in this.data)
                {
                    double trueY = item.RC * stepY;
                    if (trueY == 0)
                    {
                        trueY = 0.5 * stepY;
                    }
                    var sin = Math.Sin(item.Angle);
                    var cos = Math.Cos(item.Angle);
                    double trueX;
                    if (sin == 0)
                    {
                        trueY = 0;
                        trueX = cos == 1 ? this.Width - 1 : 0;
                    }
                    else
                    {
                        if (sin < 0)
                        {
                            trueY *= -1;
                        }
                        trueX = (int)Math.Round(trueY * cos / sin);
                    }
                    this.DrawPoint((int)(this.Width / 2 + trueX), (int)(this.Height / 2 + trueY), g, bluePen);
                    g.DrawLine(yellowPen,
                        this.TransformPoint((int)this.Width / 2 , (int)this.Height / 2 ),
                        this.TransformPoint((int)(this.Width / 2 + cos * Math.Max(this.Width, this.Height)), (int)(this.Height / 2 + sin * Math.Max(this.Width, this.Height))));
                    g.DrawLine(redPen,
                        this.TransformPoint((int)(this.Width / 2 + trueX), (int)(this.Height / 2 + trueY)),
                        this.TransformPoint((int)(this.Width / 2 + trueX + 5.0 * Math.Cos(item.Direction)), (int)(this.Height / 2 + trueY + 5.0 * Math.Sin(item.Direction))));
                }
                this.DrawPoint((int)this.Width / 2, (int)this.Height / 2, g, new Pen(Color.Red));
            }
            public void DrawPoint(int x, int y, Graphics g, Pen pen)
            {
                g.DrawEllipse(pen, new Rectangle(this.TransformPoint(x - 10, y + 10), new Size(20, 20)));
            }
            public Point TransformPoint(int x, int y)
            {
                return new Point(x + 10, this.Height - y + 10);
            }
            public void ReversePoint(Point gPoint, out int x, out int y)
            {
                x = gPoint.X - 10;
                y = this.Height - (gPoint.Y - 10);
            }
            int stepY;
            int maxRC;
        }
        public AngleRCDawingInfo DrawingInfo; 
        #endregion
        #region IDataStruct 成员
        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (var item in this)
            {
                writer.Write(item.Angle);
                writer.Write(item.RC);
                writer.Write(item.Direction);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Clear();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                this.Add(reader.ReadDouble(), reader.ReadInt32(), reader.ReadDouble());
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormAngleRCSet f = new Controls.FormAngleRCSet();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class AngleRC
    {
        public AngleRC(double angle, int rC,double direction)
        {
            this.Angle = angle;
            this.RC = rC;
            this.Direction = direction;
        }
        public double Angle;
        public int RC;
        public double Direction;
    }
}
