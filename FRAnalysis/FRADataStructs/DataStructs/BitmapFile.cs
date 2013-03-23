using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class BitmapFile:IDataStruct
    {
        public System.Drawing.Bitmap BitmapObject;
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            this.BitmapObject.Save(writer.BaseStream, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.BitmapObject = new System.Drawing.Bitmap(reader.BaseStream);
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            GrayLevelImage gimg = GrayLevelImage.FromBitmap(this.BitmapObject);
            if (gimg.Present(originalImg, dataIdent))
            {
                gimg.DrawImage(this.BitmapObject);
                return true;
            }
            return false;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
