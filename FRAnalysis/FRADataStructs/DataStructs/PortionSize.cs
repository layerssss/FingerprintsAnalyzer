using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class PortionSize : IDataStruct
    {
        public int W;
        public int H;
        public void Allocate()
        {

        }

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(W);
            writer.Write(H);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.W = reader.ReadInt32();
            this.H = reader.ReadInt32();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            System.Windows.Forms.MessageBox.Show("W:" + this.W + "\r\nH:" + this.H);
            return false;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
