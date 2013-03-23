using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class MinutiaeSet : List<Minutiae>, IDataStruct
    {
        public void Add(int i, int j, double theta, MinutiaeType type)
        {
            this.Add(new Minutiae(i, j, theta, type));
        }
        public void Draw(GrayLevelImage img)
        {
            foreach (var m in this)
            {
                switch (m.Type)
                {
                    case MinutiaeType.RE:
                        m.DrawAs(img, -1);
                        break;
                    case MinutiaeType.BI:
                        m.DrawAs(img, -1);
                        break;
                }

            }
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (var m in this)
            {
                m.Serialize(writer);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Clear();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var m = new Minutiae();
                m.Deserialize(reader);
                this.Add(m);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            var newImg = originalImg.Clone();
            this.Draw(newImg);
            newImg.Present(originalImg, dataIdent+"-细节点集-");
            return false;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
