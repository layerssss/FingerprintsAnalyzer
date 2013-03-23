using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
   public enum MinutiaeType : int
    {
        BI = 0,
        RE = 1
    }
    public class Minutiae : IDataStruct
    {
        public Minutiae() { }
        public Minutiae(int i,int j,double theta,MinutiaeType type)
        {
            this.Location = new PointLocation(i, j);
            this.Theta = theta;
            this.Type = type;
        }
        public PointLocation Location;
        public Double Theta;
        public Double Angle
        {
            get
            {
                return this.Theta;
            }
            set
            {
                this.Theta = value;
            }
        }
        public int I
        {
            get {
                return this.Location.I;
            }
            set
            {
                this.Location.I = value;
            }
        }
        public int J
        {
            get
            {
                return this.Location.J;
            }
            set
            {
                this.Location.J = value;
            }
        }
        public  MinutiaeType Type;
        public void DrawAs(GrayLevelImage img, double value)
        {
            Classes.Drawing2.EightConnectionArea((ti, tj, k) =>
            {
                try
                {
                    img.Value[this.Location.I + ti][this.Location.J + tj] = value;
                }
                catch { }
            });
            try
            {
                for (var l = 0; l < 10; l++)
                {
                    img.Value
                        [this.Location.I - (int)(Math.Round(Math.Sin(this.Theta)) * l)]
                        [this.Location.J + (int)(Math.Round(Math.Cos(this.Theta)) * l)] = value;
                }
            }
            catch { }
        }
        public void Draw(GrayLevelImage img)
        {
            this.DrawAs(img, -1);
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            this.Location.Serialize(writer);
            writer.Write(this.Theta);
            writer.Write((int)this.Type);
        }
        

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Location = new PointLocation();
            this.Location.Deserialize(reader);
            this.Theta = reader.ReadDouble();
            this.Type = (MinutiaeType)reader.ReadInt32();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            var newImg = originalImg.Clone();
            this.DrawAs(newImg, 0.9);
            newImg.Present(originalImg, dataIdent + "-细节点-");
            return false;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
