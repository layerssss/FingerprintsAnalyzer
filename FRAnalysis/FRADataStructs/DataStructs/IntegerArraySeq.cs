using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class IntegerArraySeq :List<IntegerArray>, IDataStruct
    {
        public void Allocate(int count)
        {
            this.Clear();
            for (int i = 0; i < count; i++)
            {
                this.Add(new IntegerArray());
            }
        }

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Serialize(writer);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Allocate(reader.ReadInt32());
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Deserialize(reader);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            DoubleFloatArraySeq dfas = new DoubleFloatArraySeq();
            foreach (IntegerArray ia in this)
            {
                DoubleFloatArray dfa = new DoubleFloatArray();
                dfa.AddRange(ia.Clone(ti => (double)ti));
                dfas.Add(dfa);
            }
            return dfas.Present(originalImg, dataIdent + "(整数数组序列)");
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
