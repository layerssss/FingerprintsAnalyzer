using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class StringData:IDataStruct
    {
        public string Value;
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Value);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Value = reader.ReadString();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormStringData f = new Controls.FormStringData();
            f.LoadData(this, dataIdent);
            f.ShowDialog();
            return true;
        }

        public IDataStruct BuildInstance()
        {
            return new StringData() { Value = "" };
        }

        #endregion
    }
}
