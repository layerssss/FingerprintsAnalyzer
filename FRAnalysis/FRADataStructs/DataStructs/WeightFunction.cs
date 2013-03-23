using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class WeightFunction : IDataStruct
    {
        public double a;
        public double b;
        public double GetW(double d)
        {
            return 1 / (d + a) + b;
        }
        public void Allocate()
        {

        }

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(a);
            writer.Write(b);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.a = reader.ReadDouble();
            this.b = reader.ReadDouble();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormWeightFunction f = new Controls.FormWeightFunction();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            return new WeightFunction() { a = 1, b = 0 };
        }

        #endregion
    }
}
