using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace FRADataStructs.DataStructs
{
    public class LocalStructure : Dictionary<string, IDataStruct>, IDataStruct
    {
        public void Allocate()
        {
            
        }
        public void Draw(System.Drawing.Image img,System.Drawing.Font font)
        {
            if (this.PrimaryData != null)
            {
                DataStructTypes.TryDraw(this.PrimaryData, img, System.Drawing.Color.Empty, font);
            }
        }
        public string PrimaryDataKey = "";
        public IDataStruct PrimaryData
        {
            get {
                try
                {
                    return this[this.PrimaryDataKey];
                }
                catch
                {
                    return null;
                }
            }
            set
            {
                try
                {
                    this.PrimaryDataKey = this.First(tkvp => object.ReferenceEquals(tkvp.Value, value)).Key;
                }
                catch
                {
                    throw (new Exception("该实例不属于这个局部结构"));
                }
            }
        }
        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.PrimaryDataKey);
            var types=new DataStructTypes();
            writer.Write(this.Count);
            foreach (var kvp in this)
            {
                writer.Write(kvp.Key);
                writer.Write(types.GetName(kvp.Value));
                kvp.Value.Serialize(writer);
            }
        }
        public void Deserialize(System.IO.BinaryReader reader)
        {
            var types = new DataStructTypes();
            this.PrimaryDataKey = reader.ReadString();
            this.Clear();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var key = reader.ReadString();
                var type = reader.ReadString();
                var data = types.GetNew(type);
                data.Deserialize(reader);
                this.Add(key, data);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            Controls.FormLocalStructure f = new Controls.FormLocalStructure();
            f.LoadData(this, originalImg, dataIdent);
            return f.ShowDialog() == System.Windows.Forms.DialogResult.OK;
        }

        public IDataStruct BuildInstance()
        {
            return new LocalStructure();
        }

        #endregion
    }
}
