using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace FRADataStructs.DataStructs
{
    public class LSArray :Abstract.Graph<LocalStructure>, IDataStruct,IDrawable
    {
        public LSArray()
        {
        }
        

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Width);
            writer.Write(this.Height);
            this.ForEach((ti, tj, tls) =>
            {
                writer.Write(tls != null);
                if (tls != null)
                {
                    tls.Serialize(writer);
                }
            });
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Allocate(reader.ReadInt32(), reader.ReadInt32());
            this.ForEach((ti, tj) =>
            {
                if (reader.ReadBoolean())
                {
                    var ls = new LocalStructure();
                    ls.Deserialize(reader);
                    this.ValueSet(ti, tj, ls);
                }
            });
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormLSArray f = new Controls.FormLSArray();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDrawable 成员

        public void Draw(Bitmap img,Font font)
        {
            var g = Graphics.FromImage(img);
            this.DrawInfo = new LSArrayDrawInfo(img.Width,img.Height);
            this.DrawInfo.wStep = ((float)img.Width) / this.Width;
            this.DrawInfo.hStep = ((float)img.Height) / this.Height;
            this.ForEach((ti, tj,tls) =>
            {
                var str = "";
                try
                {
                    str = (tls.PrimaryData as DoubleFloat).Value.ToString();
                }
                catch {
                    try
                    {
                        str = (tls.PrimaryData as StringData).Value.ToString();
                    }
                    catch { }
                }
                g.DrawString(str, font, Brushes.Black, this.DrawInfo.wStep * tj, this.DrawInfo.hStep * ti);
            });
        }
        public LSArrayDrawInfo DrawInfo;
        public string Reverse(int x, int y)
        {
            return string.Format("I:{0}   J:{1}", (int)(y / this.DrawInfo.hStep), (int)(x / this.DrawInfo.wStep));
        }

        #endregion
    }
    public class LSArrayDrawInfo
    {
        public LSArrayDrawInfo(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
        public float wStep;
        public float hStep;
        public int Width;
        public int Height;
    }
}
