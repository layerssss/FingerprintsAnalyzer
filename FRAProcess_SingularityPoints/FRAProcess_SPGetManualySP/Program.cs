using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
using System.IO;


namespace FRAProcess_SPGetManualySP
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_SPGetManualySPProcess(), args);
        }
    }
    class FRAProcess_SPGetManualySPProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        StringData filePath;
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            filePath = inputter.GetArg<StringData>("正在处理的文件路径");
        }
        PointLocation refP;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("手动标出的参考点", refP);
        }

        public override void Procedure()
        {
            refP = new PointLocation();
            try
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(filePath.Value + ".宏处理结果.手动标出的参考点.点坐标"));
                refP.Deserialize(reader);
                reader.Close();
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }

}
