using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace FRADataStructs.DataStructs
{
    public class BoolData:IDataStruct
    {
        public bool Value;
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Value);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Value = reader.ReadBoolean();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            DialogResult dr = MessageBox.Show("当前值为：" + this.Value.ToString() + "\r\n请选择新值", "布尔值-" + dataIdent, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.Cancel)
            {
                return false;
            }
            this.Value = dr == DialogResult.Yes;
            return true;
        }

        public IDataStruct BuildInstance()
        {
            return new BoolData() { Value = false };
        }

        #endregion
    }
}
