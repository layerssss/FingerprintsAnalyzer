using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FRADataStructs
{
    /// <summary>
    /// 数据接口，所有方法必须以静态方法来描述！
    /// </summary>
    public interface IDataStruct
    {
        void Serialize(System.IO.BinaryWriter writer);
        void Deserialize(System.IO.BinaryReader reader);
        bool Present(DataStructs.GrayLevelImage originalImg,string dataIdent);
        IDataStruct BuildInstance();
    }
}
