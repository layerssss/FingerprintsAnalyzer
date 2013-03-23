using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class IntegerArray :Abstract.Array<int>,IDataStruct
    {
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
            this.Clear();
            int count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                this.Add(reader.ReadInt32());
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            DoubleFloatArray dfa = new DoubleFloatArray();
            dfa.AddRange(this.Clone(ti => (double)ti));
            return dfa.Present(originalImg, dataIdent + "(整数数组)");
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
