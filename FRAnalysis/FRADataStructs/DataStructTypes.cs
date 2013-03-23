using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FRADataStructs
{
    /// <summary>
    /// 管理各种数据结构类型和其名称，提供创建数据结构实例的接口。
    /// </summary>
    public class DataStructTypes : Dictionary<string, DataStructInitialHandler>
    {

        /// <summary>
        /// 加载一个 <see cref="DataStructTypes"/> 的实例.
        /// </summary>
        public DataStructTypes()
            : base()
        {
            this.registerTypes();
        }
        /// <summary>
        /// 注册各种类型
        /// </summary>
        void registerTypes()
        {
            #region 删改应谨慎的基础数据类型
            this.AddType<DataStructs.GrayLevelImage>("灰度图");
            this.AddType<DataStructs.Macro>("宏");
            #endregion
            this.AddType<DataStructs.StringData>("字符串");
            this.AddType<DataStructs.Integer>("整数");
            this.AddType<DataStructs.DoubleFloat>("浮点数");
            this.AddType<DataStructs.BoolData>("布尔值");
            this.AddType<DataStructs.OrientationGraph>("走向图");
            this.AddType<DataStructs.IntergerGraph>("整数图");
            this.AddType<DataStructs.PoincareIndexes>("庞加莱指数");
            this.AddType<DataStructs.BinaryGraph>("二值图");
            this.AddType<DataStructs.PointLocation>("点坐标");
            this.AddType<DataStructs.PointLocationSet>("点坐标集");
            this.AddType<DataStructs.Minutiae>("细节点");
            this.AddType<DataStructs.MinutiaeSet>("细节点集");
            this.AddType<DataStructs.LocalStructure>("局部结构");
            this.AddType<DataStructs.LocalStructureSet>("局部结构集合");
            this.AddType<DataStructs.LocalStructureTestSets>("局部结构测试对集合");
            this.AddType<DataStructs.IntegerPairs>("整数对集合");
            this.AddType<DataStructs.SegmentationArea>("切割区域");
            this.AddType<DataStructs.PortionSize>("尺寸");
            this.AddType<DataStructs.Window>("窗口");
            this.AddType<DataStructs.DoubleFloatArray>("数组");
            this.AddType<DataStructs.IntegerArray>("整数数组");
            this.AddType<DataStructs.IntegerArraySeq>("整数数组序列");
            this.AddType<DataStructs.DoubleFloatArraySeq>("数组序列");
            this.AddType<DataStructs.StabSet>("针刺集");
            this.AddType<DataStructs.BitmapFile>("bmp");
            this.AddType<DataStructs.TextAppender>("字符串添加记录");
            this.AddType<DataStructs.WeightFunction>("动态权函数");
            this.AddType<DataStructs.MatchingSchema>("匹配方案");
            this.AddType<DataStructs.DirectionGraph>("方向图");
            this.AddType<DataStructs.AngleRCSet>("方向脊线数对集合");
            this.AddType<DataStructs.Filter>("滤波器");
            this.AddType<DataStructs.FilterSet>("滤波器集合");
            this.AddType<DataStructs.LSArray>("矩阵");
            this.AddType<DataStructs.ComplexFilter>("复数滤波器");
        }
        #region 泛型逻辑与接口
        /// <summary>
        /// Adds the type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        void AddType<T>(string name)
            where T : IDataStruct, new()
        {
            T empty = new T();
            this.Add(name.ToLower(), DataStructTypes.initialHandler<T>);
        }
        /// <summary>
        /// 根据类型名称获取一个新的数据结构实例（只提供实例对象，不保证数据可用）
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public IDataStruct GetNew(string name)
        {
            try
            {
                return this[name.ToLower()]();
            }
            catch
            {
                throw (new Exception("不支持的数据类型"));
            }
        }
        /// <summary>
        /// 在程序中获得某类型的名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public string GetName<T>()
            where T : IDataStruct, new()
        {
            T empty = new T();
            foreach (string name in this.Keys)
            {
                if (DataStructTypes.initialHandler<T> == this[name])
                {
                    return name;
                }
            }
            throw (new Exception("没有添加到列表的数据类型"));
        }
        /// <summary>
        /// 获取一个未知类型的数据结构实例的类型名称
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public string GetName(IDataStruct data)
        {
            try
            {
                return this.First(tkvp => tkvp.Value().GetType() == data.GetType()).Key;
            }
            catch
            {
                throw (new Exception("没有添加到列表的数据类型"));
            }
        }
        /// <summary>
        /// 用泛型生成数据结构对象的构造函数的委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static IDataStruct initialHandler<T>()
            where T : IDataStruct, new()
        {
            return new T();
        } 
        #endregion
        #region 共享接口
        /// <summary>
        /// 尝试调用Draw方法，可能会产生异常.
        /// </summary>
        /// <param name="obj">目标实例.</param>
        /// <param name="img">图像.</param>
        /// <param name="color">颜色.</param>
        public static void TryDraw(object obj,System.Drawing.Image img, System.Drawing.Color color,System.Drawing.Font font)
        {
            var type = obj.GetType();
            var method = type.GetMethod("Draw");
            if (method.GetParameters()[0].ParameterType==typeof(DataStructs.GrayLevelImage))
            {
                var gimg = DataStructs.GrayLevelImage.FromBitmap(img as System.Drawing.Bitmap);
                method.Invoke(obj, new object[] { gimg });
                var bmp = new System.Drawing.Bitmap(gimg.Width, gimg.Height);
                gimg.DrawImage(bmp);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
                g.DrawImage(bmp, 0, 0, img.Width, img.Height);
                return;
            }
            if (method.GetParameters().Length == 1)
            {
                method.Invoke(obj, new object[] { img });
            }
            else
            {
                if (method.GetParameters()[1].ParameterType == typeof(System.Drawing.Color))
                {
                    method.Invoke(obj, new object[] { img, color == System.Drawing.Color.Empty ? System.Drawing.Color.Red : color });
                }
                else
                {
                    method.Invoke(obj, new object[] { img, font });
                }
            }
        }
        /// <summary>
        /// 获取一个关于该类数据的临时文件。
        /// </summary>
        /// <param name="rootData">数据.</param>
        /// <param name="fileName">文件名.</param>
        /// <returns>文件实例</returns>
        public FileInfo GetTempFile(IDataStruct rootData, string fileName)
        {
            var fs = new FileInfo(Application.StartupPath + "\\data." + this.GetName(rootData) + "\\" + fileName);
            if(!fs.Directory.Exists){
                fs.Directory.Create();
            }
            return fs;
        }
        /// <summary>
        /// 重新打开一个进程以实现异步呈现文件。
        /// </summary>
        /// <param name="fullPath">The full path.</param>
        public void AsyncPresent(IDataStruct rootData,IDataStruct data,DataStructs.GrayLevelImage img, string dataIdent)
        {
            var fs = this.GetTempFile(rootData, dataIdent + "." + this.GetName(data));
            {
                var writer = new BinaryWriter(fs.Create());
                data.Serialize(writer);
                writer.Close();
            }
            if (img != null)
            {
                var writer = new BinaryWriter(this.GetTempFile(rootData, "原图像.灰度图").Create());
                img.Serialize(writer);
                writer.Close();
            }
            Process.Start(new ProcessStartInfo() { 
                FileName=fs.Name,
                WorkingDirectory=fs.Directory.FullName
            });
        }
        #endregion
    }
    /// <summary>
    /// 用于创建一个数据结构体的委托，该委托的方法返回类型未具体到类型，所以委托的相等判断可用来进行类型表查找。
    /// </summary>
    public delegate IDataStruct DataStructInitialHandler(); 
}
