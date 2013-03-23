using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_GetFilePath
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_GetFilePathProcess(), args);
        }
    }
    class FRAProcess_GetFilePathProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
        }
        StringData filePath;
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg("正在处理的文件路径",filePath);
        }

        public override void Procedure()
        {
            this.filePath = new StringData();
            try
            {
                filePath.Value = System.IO.File.ReadAllText("processingFilepath.txt");
            }
            catch (System.IO.FileNotFoundException)
            {
                filePath.Value = "系统内部数据";
            }
        }
    }

}
