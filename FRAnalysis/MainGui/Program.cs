using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using FRADataStructs;
using System.Runtime.InteropServices;
namespace MainGui
{
    static class Program
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(
            string lpClassName,
            string lpWindowName
        );
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            int hwnd,
            int wMsg,
            int wParam,
            ref int lParam
        );
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            if (args.Length == 0)
            {
                var handler = FindWindow(null, "指纹全局特征分析实验");
                if (handler!=0)
                {
                    int r=0;
                    SendMessage(handler, 9999, 0, ref r);
                    Application.Exit();
                }
                else
                {
                    Application.Run(new FormMain());
                }
            }
            else
            {
                DataStructTypes dataStructTypes = new DataStructTypes();
                string filename = args[0];
                string typestr = filename.Substring(filename.LastIndexOf('.') + 1);
                BinaryReader reader;
                try
                {
                    reader = new BinaryReader(File.OpenRead(filename));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取数据时发生以下错误：\r\n" + ex.Message, "读取数据时发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                IDataStruct data;
                try
                {
                    data = dataStructTypes.GetNew(typestr);
                    data.Deserialize(reader);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("读取数据时发生以下错误：\r\n" + ex.Message, "读取数据时发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    reader.Close();
                    return;
                }
                reader.Close();
                FRADataStructs.DataStructs.GrayLevelImage img = new FRADataStructs.DataStructs.GrayLevelImage();
                try
                {
                    BinaryReader imgReader = new BinaryReader(File.OpenRead(filename.Remove(filename.LastIndexOf('\\')) + "\\原图像.灰度图"));
                    img.Deserialize(imgReader);
                    imgReader.Close();
                }
                catch
                {
                    try
                    {
                        img = FRADataStructs.DataStructs.GrayLevelImage.FromBitmap(new System.Drawing.Bitmap(
                            filename.Remove(filename.LastIndexOf(".宏处理结果."))));
                    }
                    catch
                    {
                        img = null;
                    }
                }
                bool save;
                try
                {
                    save = data.Present(img, new FileInfo(filename).Name);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("查看数据时发生以下错误，可能查看该数据需要调用同一目录下的原图像。\r\n" + ex.Message, "查看数据时发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (save)
                {
                    BinaryWriter writer = new BinaryWriter(File.Create(filename));
                    try
                    {
                        data.Serialize(writer);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("修改数据时发生以下错误：\r\n" + ex.Message, "修改数据时发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    writer.Close();
                }
            }
        }
    }
}
