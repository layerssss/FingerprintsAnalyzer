using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs.DataStructs;
using FRADataStructs.DataStructs.Classes;
namespace FRAProcess_AppendDoubleFloatToCsv
{
    class Program
    {
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new FRAProcess_AppendDoubleFloatToCsvProcess(), args);
        }
    }
    class FRAProcess_AppendDoubleFloatToCsvProcess : FRAProcess.FRAProcess
    {
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            base.PreInput(inputter);
        }
        DoubleFloat data;
        StringData fname;
        StringData linehead;
        TextAppender t = new TextAppender();
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            data = inputter.GetArg<DoubleFloat>("数据");
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
            t.Text += "," + data.Value;
            t.Text += "\r\n";
            t.Encoding = Encoding.Default;
        }
    }

}
