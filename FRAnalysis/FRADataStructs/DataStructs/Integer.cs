using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class Integer:IDataStruct
    {
        public int Value = 0;
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Value);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Value = reader.ReadInt32();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormInterger f = new Controls.FormInterger(this.Value, originalImg, dataIdent);
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.Value = f.I;
                return true;
            }
            return false;
        }


        #endregion

        #region IDataStruct 成员


        public IDataStruct BuildInstance()
        {
            return new Integer() { Value = 0 };
        }

        #endregion
    }
}
