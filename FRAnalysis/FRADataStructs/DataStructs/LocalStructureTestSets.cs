using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class LocalStructureTestSets : List<LocalStructureTest>, IDataStruct
    {

        Random r = new Random();
        public LocalStructureTest GetRandomMatched()
        {
            if (this.Count < 1)
            {
                throw (new Exception());
            }
            return this[r.Next(this.Count)];
        }
        public LocalStructureTest GetRandomUnmatched()
        {
            if (this.Count <2)
            {
                throw (new Exception());
            }
            int i1 = r.Next(this.Count);
            int i2;
            do
            {
                i2 = r.Next(this.Count);
            } while (i2 == i1);
            return new LocalStructureTest { 
                Data=new []{this[i1].Data[0],this[i2].Data[1]},
                Img=new []{this[i1].Img[0],this[i2].Img[1]}
            };
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (var item in this)
            {
                if (item.Data[0] != null)
                {
                    writer.Write(true);
                    item.Data[0].Serialize(writer);
                }
                else
                {
                    writer.Write(false);
                }
                if (item.Data[1] != null)
                {
                    writer.Write(true);
                    item.Data[1].Serialize(writer);
                }
                else
                {
                    writer.Write(false);
                }
                if (item.Img[0] != null)
                {
                    writer.Write(true);
                    item.Img[0].Serialize(writer);
                }
                else
                {
                    writer.Write(false);
                }
                if (item.Img[1] != null)
                {
                    writer.Write(true);
                    item.Img[1].Serialize(writer);
                }
                else
                {
                    writer.Write(false);
                }
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Clear();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var pair = new LocalStructureTest();
                if (reader.ReadBoolean())
                {
                    pair.Data[0] = new LocalStructure();
                    pair.Data[0].Deserialize(reader);
                }
                if (reader.ReadBoolean())
                {
                    pair.Data[1] = new LocalStructure();
                    pair.Data[1].Deserialize(reader);
                }
                if (reader.ReadBoolean())
                {
                    pair.Img[0] = new GrayLevelImage();
                    pair.Img[0].Deserialize(reader);
                }
                if (reader.ReadBoolean())
                {
                    pair.Img[1] = new GrayLevelImage();
                    pair.Img[1].Deserialize(reader);
                }
                this.Add(pair);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormLocalStructureTestSets f = new Controls.FormLocalStructureTestSets();
            f.LoadData(this, originalImg, dataIdent);
            f.ShowDialog();
            return true;
        }

        public IDataStruct BuildInstance()
        {
            return this;
        }

        #endregion
    }
    public class LocalStructureTest
    {
        public LocalStructure[] Data=new LocalStructure[2];
        public GrayLevelImage[] Img=new GrayLevelImage[2];
    }
}
