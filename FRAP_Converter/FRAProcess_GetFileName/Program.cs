using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_GetFileName
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new p(), args);
        }
    }
    class p : FRAProcess.FRAProcess
    {
        StringData filename = new StringData();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            ;
        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<StringData>("正在处理的文件名", filename);
        }

        public override void Procedure()
        {
            try
            {
                filename.Value = System.IO.File.ReadAllText("processingFilename.txt");
            }
            catch(System.IO.FileNotFoundException )
            {
                filename.Value = "系统内部数据";
            }
        }
    }
}
