using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class TextAppender:IDataStruct
    {
        public string Filename;
        public string Text;
        public Encoding Encoding;
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(Filename);
            writer.Write(Text);
            writer.Write(this.Encoding.CodePage);
            System.IO.File.AppendAllText(Filename, Text,this.Encoding);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Filename = reader.ReadString();
            this.Text = reader.ReadString();
            this.Encoding = Encoding.GetEncoding(reader.ReadInt32());
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            return System.Windows.Forms.MessageBox.Show("以下信息已被添加到了" + Filename + "中,是否再次添加？\r\n" + Text, "字符串添加记录-" + dataIdent, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes;
        }

        public IDataStruct BuildInstance()
        {
            throw (new NotImplementedException());
        }

        #endregion
    }
}
