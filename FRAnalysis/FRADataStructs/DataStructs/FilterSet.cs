using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs.DataStructs
{
    public class FilterSet :List<Filter>, IDataStruct
    {

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
                var item = new Filter();
                item.Deserialize(reader);
                this.Add(item);
            }
        }

        public bool Present(GrayLevelImage originalImg, string dataIdent)
        {
            var types = new DataStructTypes();
            LocalStructure list = new LocalStructure();
            for (int i = 0; i < this.Count; i++)
            {
                list.Add("滤波器" + i.ToString("D8"), this[i]);
            }
            if (list.Present(originalImg, dataIdent + "(滤波器集合)"))
            {
                if (!list.Values.All(tdata => types.GetName(tdata) == types.GetName<Filter>()))
                {
                    System.Windows.Forms.MessageBox.Show("尝试保存集合但是集合中添加了不是滤波器的数据，故不会保存修改。", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return false;
                }
                this.Clear();
                foreach (var item in list.Values)
                {
                    this.Add(item as Filter);
                }
                return true;
            }
            return false;
        }

        public IDataStruct BuildInstance()
        {
            return new FilterSet();
        }

        #endregion
    }
}
