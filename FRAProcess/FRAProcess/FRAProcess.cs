using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FRADataStructs;
namespace FRAProcess
{
    public abstract class FRAProcess
    {
        #region 定义实例时用的抽象成员
        abstract public void Procedure();
        public abstract void Input(FRAProcessInputter inputter);
        public abstract void Output(FRAProcessOutputter outputter);
        public virtual void PreInput(FRAProcessInputter inputter)
        {
        } 
        #endregion
        #region 私有
        FRADataStructs.DataStructTypes dataStructTypes = new FRADataStructs.DataStructTypes();
        List<KeyValuePair<FRADataStructs.IDataStruct, KeyValuePair<string, string>>> inputs = new List<KeyValuePair<IDataStruct, KeyValuePair<string, string>>>(); 
        void saveAsInput<T>(T data, string path)
            where T:IDataStruct,new()
        {
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(System.IO.File.Create(path));
            data.Serialize(bw);
            bw.Close();
        }
        T loadFromOutput<T>(string path)
            where T:IDataStruct,new()
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(System.IO.File.OpenRead(path));
            T data = new T();
            data.Deserialize(br);
            br.Close();
            return data;
        }
        void callProcess(string exeFilename,string workingDirectory)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process()
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    FileName = exeFilename,
                    WorkingDirectory = workingDirectory,
                    Arguments = "-process -release",
                    UseShellExecute=false

                }
            };
            p.Start();
            p.WaitForExit();
            string output = p.StandardOutput.ReadToEnd();
            if (p.ExitCode != 0)
            {
                throw (new AlgorithmException("被调用的进程遇到错误已退出:\r\n" + output));
            }
            Console.WriteLine(output);
        }
        string plan;
        #endregion
        #region 外部接口
        public int Execute(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("这是一个FRA实验平台的处理过程，请在实验平台主界面或者FRA宏中调用。");
                Console.WriteLine("按任意键继续...");
                Console.ReadKey();
                return 1;
            }
            System.IO.StreamWriter output;
            switch (args[0])
            {
                case "-arglist":
                    output = new System.IO.StreamWriter("arglist.txt");
                    try
                    {
                        this.PreInput(new FRAProcessInputter(false, output, dataStructTypes,this.plan));
                        this.PreInput(new FRAProcessInputter(true, null, dataStructTypes, this.plan));
                        this.Input(new FRAProcessInputter(false, output, this.dataStructTypes, this.plan));
                        this.Output(new FRAProcessOutputter(false, output, this.dataStructTypes, this.plan));
                        output.Close();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("获取参数列表时发生错误：");
                        Console.WriteLine(ex.Message);
                        return 2;
                    }
                case "-preinputs":
                    output = new System.IO.StreamWriter("preinputs.txt");
                    try
                    {
                        this.PreInput(new FRAProcessInputter(false, output, dataStructTypes, this.plan));
                        output.Close();
                        return 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("获取参数列表时发生错误：");
                        Console.WriteLine(ex.Message);
                        output.Close();
                        return 2;
                    }
                case "-process":
                    if (args.Length > 1)
                    {
                        this.plan = args[1].TrimStart('-');
                    }
                    else
                    {
                        this.plan = "release";
                    }
                    try
                    {
                        this.PreInput(new FRAProcessInputter(true, null, dataStructTypes, this.plan));
                        this.Input(new FRAProcessInputter(true, null, this.dataStructTypes, this.plan));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("获取输入参数时发生错误");
                        Console.WriteLine(ex.Message);
                        return 3;
                    }
                    try
                    {
                        this.Interupt("现在可以附加进程以进行调试");
                        this.Procedure();
                    }
                    catch (AlgorithmException ex)
                    {
                        Console.WriteLine("算法检测到错误已要求停止：");
                        Console.WriteLine(ex.Message);
                        return 4;
                    }
                    try
                    {
                        this.Output(new FRAProcessOutputter(true, null, this.dataStructTypes, this.plan));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("导出结果时发生错误：");
                        Console.WriteLine(ex.Message);
                        return 5;
                    }
                    return 0;
                default:
                    Console.WriteLine("Wrong usage! Available arguments: -arglist , -process. ");
                    return 1;
            }
        }
        public static int Execute(FRAProcess p, string[] args)
        {
            return p.Execute(args);
        } 
        #endregion
        #region 内部接口
        public void Interupt(string info)
        {
            if (this.plan == "debug")
            {
                Console.WriteLine("DEBUG:HOLD:" + info.Replace("\n", ""));
                while (Console.ReadLine() != "NEXT") ;
            }
        }
        public void Progress(int val, int max)
        {
            Console.WriteLine("DEBUG:PROGRESS:" + val + "/" + max);
        }
        public void SaveAsInput<T>(T data, string argName) where T:IDataStruct,new()
        {
            this.saveAsInput(data, argName + "." + dataStructTypes.GetName<T>());
        }
        public void SaveAsInput<T>(T data, string argName, string procType) where T : IDataStruct, new()
        {
            this.saveAsInput(data, "..\\" + procType + "\\" + argName + "." + dataStructTypes.GetName<T>());
        }
        public T LoadFromOutput<T>(string argName) where T : IDataStruct, new()
        {
            return this.loadFromOutput<T>(argName + "." + dataStructTypes.GetName<T>());
        }
        public T LoadFromOutput<T>(string argName, string procType) where T : IDataStruct, new()
        {
            return this.loadFromOutput<T>("..\\" + procType + "\\" + argName + "." + dataStructTypes.GetName<T>());
        }
        public void CallProcess(string procName)
        {
            this.callProcess("FRAProcess_" + procName + ".exe", "");
        }
        public void CallProcess(string procName,string procType)
        {
            this.callProcess("FRAProcess_" + procName + ".exe", "..\\" + procType);
        }
        #endregion
    }
    public class AlgorithmException : System.Exception
    {
        public AlgorithmException(string message)
            :base(message)
        {
        }
    }
}
