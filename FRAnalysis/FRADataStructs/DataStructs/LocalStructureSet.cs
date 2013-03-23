using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class LocalStructureSet : List<LocalStructure>,IDataStruct
    {
        public void Allocate()
        {

        }

        #region IDataStruct 成员

        public void Serialize(System.IO.BinaryWriter writer)
        {
            writer.Write(this.Count);
            foreach (var item in this)
            {
                item.Serialize(writer);
            }
        }

        public void Deserialize(System.IO.BinaryReader reader)
        {
            this.Clear();
            var count = reader.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                var item = new LocalStructure();
                item.Deserialize(reader);
                this.Add(item);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            var types=new DataStructTypes();
            LocalStructure list = new LocalStructure();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add("局部结构" + i.ToString("D8"), this[i]);
            }
            if (list.Present(originalImg, dataIdent + "(局部结构集合)"))
            {
                if (!list.Values.All(tdata => types.GetName(tdata) == types.GetName<LocalStructure>()))
                {
                    System.Windows.Forms.MessageBox.Show("尝试保存集合但是集合中添加了不是局部结构的数据，故不会保存修改。", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                this.Clear();
                foreach (var item in list.Values)
                {
                    this.Add(item as LocalStructure);
                }
                return true;
            }
            return false;
        }

        public IDataStruct BuildInstance()
        {
            return new LocalStructureSet();
        }

        #endregion
    }
}
