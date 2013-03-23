using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class Macro:IDataStruct
    {
        #region IDataStruct 成员
        public string Text;
        public string MD5PART;
        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        public static string ByteArrayToHexString(byte[] buf,int offset,int len)
        {
            len += offset;
            var sb = new StringBuilder();
            for (int i = offset; i < len; i++)
            {
                sb.Append(buf[i].ToString("X").PadRight(2,'0'));
            }
            return sb.ToString();
        }
        public bool CalculateMD5 = true;
        public void Serialize(System.IO.BinaryWriter writer)
        {
            if (this.CalculateMD5)
            {
                System.Security.Cryptography.MD5CryptoServiceProvider get_md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                this.MD5PART = ByteArrayToHexString(get_md5.ComputeHash(Encoding.UTF8.GetBytes(this.Text)), 0, 6);
            }
            writer.Write(0);
            writer.Write(this.MD5PART);
            writer.Write(Text);
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            if (reader.ReadInt32() != 0)
            {
                reader.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                System.IO.StreamReader sr = new System.IO.StreamReader(reader.BaseStream);
                this.Text = sr.ReadToEnd();
                return;
            }
            this.MD5PART = reader.ReadString();
            this.Text = reader.ReadString();
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormMacro f = new Controls.FormMacro();
            f.LoadData(this,originalImg, dataIdent);
            f.ShowDialog();
            return (f.ShouldSave);
        }

        public IDataStruct BuildInstance()
        {
            return new Macro()
            {
                Text = ""
            };
        }

        #endregion
    }
}
