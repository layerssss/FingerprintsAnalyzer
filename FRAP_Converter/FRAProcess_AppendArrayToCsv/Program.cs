using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
namespace FRAProcess_AppendArrayToCsv
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
        DoubleFloatArray arr;
        StringData fname;
        StringData linehead;
        TextAppender t = new TextAppender();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            arr = inputter.GetArg<DoubleFloatArray>("数据");
            fname = inputter.GetArg<StringData>("CSV文件名");
            linehead = inputter.GetArg<StringData>("行首");

        }

        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            outputter.PutArg<TextAppender>("追加记录", t);
        }

        public override void Procedure()
        {
            t.Filename = fname.Value;
            t.Text = linehead.Value;
            t.Encoding = Encoding.Default;
            foreach (double d in arr)
            {
                t.Text += ","+d.ToString();
            }
            t.Text += "\r\n";
        }
    }
}
