using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class DoubleFloat:IDataStruct
    {
        public double Value;
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(Value);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            Value = reader.ReadDouble();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormDoubleFloat f = new Controls.FormDoubleFloat(Value, originalImg, dataIdent);
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
            return new DoubleFloat(){Value=0};
        }

        #endregion
    }
}
