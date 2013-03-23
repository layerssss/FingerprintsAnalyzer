using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class PointLocationSet:List<PointLocation>,IDataStruct,IEquatable<PointLocationSet>
    {
        public void Add(int i, int j)
        {
            this.Add(new PointLocation(i, j));
        }
        public void Draw(System.Drawing.Bitmap img)
        {
            foreach (var point in this)
            {
                point.Draw(img, System.Drawing.Color.Red);
            }
        }
        public PointLocation WeightCore()
        {
            return new PointLocation((int)this.Average<PointLocation>(tpl => tpl.I), (int)this.Average<PointLocation>(tpl => tpl.J));
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (PointLocation pointLocation in this)
            {
                pointLocation.Serialize(writer);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            int count = reader.ReadInt32();
            while (count > 0)
            {
                PointLocation pointLocation = new PointLocation();
                pointLocation.Deserialize(reader);
                this.Add(pointLocation);
                count--;
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormPointLocationSet f = new Controls.FormPointLocationSet();
            f.LoadData(this, dataIdent, originalImg);
            f.ShowDialog();
            return false;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region IEquatable<PointLocationSet> 成员

        public bool Equals(PointLocationSet other)
        {
            if (this.Count != other.Count) { return false; }
            for (int i = 0; i < this.Count; i++)
            {
                if(!this[i].Equals(other[i])){
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
