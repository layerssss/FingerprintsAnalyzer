using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using FRADataStructs.DataStructs;
namespace FRAProcess_CallMacro
{
    class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            return FRAProcess.FRAProcess.Execute(new ProcessCallMacro(), args);
        }
    }
    class ProcessCallMacro : FRAProcess.FRAProcess
    {
        List<string> listGlobalData=new List<string>();
        List<string> listResult=new List<string>();
        public override void PreInput(FRAProcess.FRAProcessInputter inputter)
        {
            inputter.GetArg<Macro>("目标宏");
        }
        public override void Input(FRAProcess.FRAProcessInputter inputter)
        {
            Process p = Process.Start(new ProcessStartInfo()
            {
                FileName = File.ReadAllText("..\\..\\macroRuntimePath.txt") + "\\FRAMacroRuntime.exe",
                Arguments = " 目标宏.宏",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            });
            p.WaitForExit();
            if (p.ExitCode != 0)
            {
                Console.WriteLine("编译不成功");
                Console.WriteLine(p.StandardOutput.ReadToEnd());
                throw (new Exception());
            }
            while (!p.StandardOutput.EndOfStream)
            {
                string line = p.StandardOutput.ReadLine();
                if (line.Contains("错误(第"))
                {
                    throw (new Exception("目标宏编译不成功"));
                }
                if (line.StartsWith("需要全局数据："))
                {
                    this.listGlobalData.Add(line.Substring("需要全局数据：".Length));
                }
                if (line.StartsWith("输出结果："))
                {
                    this.listResult.Add(line.Substring("输出结果：".Length));
                }
            }
            //inputter.GetArg<GrayLevelImage>("原图像");
            foreach (string input in this.listGlobalData)
            {

                int i = input.LastIndexOf('.');
                inputter.GetArg(input.Substring(i + 1), input.Remove(i));
            }
        }
        public override void Output(FRAProcess.FRAProcessOutputter outputter)
        {
            
            foreach (string output in this.listResult)
            {
                int i = output.LastIndexOf('.');
                FRADataStructs.IDataStruct data = outputter.DataStructTypes.GetNew(output.Substring(i + 1));
                if (outputter.Processing)
                {
                    File.Copy("原图像.灰度图.宏处理结果." + output, output, true);
                }
                else
                {
                    outputter.PutArg(output.Substring(i + 1), output.Remove(i), data);
                }
            }
        }
        public override void Procedure()
        {
            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo()
                    {
                        FileName = File.ReadAllText("..\\..\\macroRuntimePath.txt") + "\\FRAMacroRuntime.exe",
                        Arguments = " \"目标宏.宏\" \"原图像.灰度图\"",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    },
            };
            p.Start();
            while (!p.HasExited)
            {
                try
                {
                    Console.WriteLine(p.StandardOutput.ReadLine());
                    System.Threading.Thread.Sleep(20);
                }
                catch { }
            }
            if (p.ExitCode != 0)
            {
                throw (new FRAProcess.AlgorithmException("宏运行失败"));
            }
        }
    }
}
