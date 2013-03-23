using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class DataStruct:IDataStruct
    {
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            throw new NotImplementedException();
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormDataStuct f = new Controls.FormDataStuct();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
