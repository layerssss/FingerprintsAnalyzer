using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class ComplexFilter :Abstract.Graph<Classes.Complex>,IDataStruct
    {
        public int W;
        public void Allocate(int w, Classes.Complex defaulVal)
        {
            this.W = w;
            base.Allocate(w * 2 + 1, w * 2 + 1, defaulVal);
        }
        public override void Allocate(int width, int height, Classes.Complex defaultValue)
        {
            if (defaultValue == null)
            {
                defaultValue = new Classes.Complex(0, 0);
            }
            base.Allocate(width, height, defaultValue);
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {

            writer.Write(this.Width);
            writer.Write(this.Height);
            this.FindAny((ti, tj, tv) =>
            {
                writer.Write(tv.R);
                writer.Write(tv.I);
                return false;
            });
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            base.Allocate(reader.ReadInt32(), reader.ReadInt32());
            this.W = (base.Width - 1) / 2;
            this.FindAny((ti, tj) =>
            {
                this.ValueSet(ti, tj,new Classes.Complex( reader.ReadDouble(),reader.ReadDouble()));
                return false;
            });
        }
        
        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            var f = new Filter();
            f.W = this.W;
            f.Height = this.Height;
            f.Width = this.Width;
            f.Value = this.Clone(tc => tc.R).Value;
            return f.Present(originalImg, "复数滤波器->" + dataIdent);
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
