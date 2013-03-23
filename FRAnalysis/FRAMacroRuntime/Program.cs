using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace FRAMacroRuntime
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.WriteLine("FRA 宏运行时 版本0.2");
            Console.WriteLine("版权所有 (c) 2011 尹志翔。");
            Console.WriteLine("");
            SCS.Parser parser;
            string allText = "";
            try
            {
                FRAMacro macro;
                #region 读取代码
                FRADataStructs.DataStructs.Macro macroData;
                try
                {
                    macroData = new FRADataStructs.DataStructs.Macro();
                    BinaryReader reader = new BinaryReader(File.OpenRead(args[0]));
                    macroData.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    throw (new MException(0, "文件", "读取宏代码时发生错误“" + ex.Message + "”"));
                }
                #endregion 
                #region 检查目录
                var procPath = System.IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\processPath.txt");
                var dataPath = System.Windows.Forms.Application.StartupPath + "\\RuntimeDataCache\\Macro" + macroData.MD5PART + ".data";
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\CompiledCache"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\CompiledCache");
                } 
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\RuntimeDataCache"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\RuntimeDataCache");
                }
                if (!Directory.Exists(dataPath))
                {
                    Directory.CreateDirectory(dataPath);
                }
                #endregion
                if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\CompiledCache\\Macro" + macroData.MD5PART))
                {
                    #region 读取已编译的宏
                    BinaryReader br = new BinaryReader(File.OpenRead(System.Windows.Forms.Application.StartupPath + "\\CompiledCache\\Macro" + macroData.MD5PART));
                    macro = new FRAMacro();
                    macro.Deserialize(br);
                    br.Close(); 
                    #endregion
                    Console.WriteLine();
                    Console.WriteLine("在缓存中找到了该宏。");
                }
                else
                {
                    #region 编译宏
                    #region 装载编译器
                    try
                    {
                        parser = new SCS.Parser(new MacroGramma.FRAMGramma().GetData());
                    }
                    catch (Exception ex)
                    {
                        throw (new MException(0, "内部", "装载编译器时发生类型为“" + ex.ToString() + "(" + ex.Message + ")”的错误。"));
                    }
                    #endregion
                    Console.WriteLine("正在编译" + args[0] + "(Macro" + macroData.MD5PART + ")...");
                    #region Parse，并捕获各种异常
                    allText = macroData.Text;
                    parser.InitContext(allText);
                    try
                    {
                        parser.Parse();
                    }
                    catch (SCS.ScannerEndOfFileException ex)
                    {
                        throw (new MException(ex.Offset, "语法", "意外的文件结尾"));
                    }
                    catch (SCS.ScannerCannotRecognizeException ex)
                    {
                        throw (new MException(ex.Offset, "语法", "不能识别的符号"));
                    }
                    catch (SCS.ParserException ex)
                    {
                        string ss = "";
                        foreach (string s in ex.Expecting)
                        {
                            ss += s + ",";
                        }
                        ss = ss.TrimEnd(',');
                        if (ex.Cur == "")
                        {
                            throw (new MException(ex.Offset, "语法", "期待以下值" + ss));
                        }
                        else
                        {
                            throw (new MException(ex.Offset, "语法", "期待" + ss + "，而不是" + ex.Cur));
                        }
                    }
                    #endregion
                    macro = (FRAMacro)parser.SemanticRecordsStack.Pop().InterminalObject;
                    if (!Directory.Exists(dataPath))
                    {
                        Directory.CreateDirectory(dataPath);
                    }
                    macro.SemanticCheck(procPath, dataPath);
                    #endregion
                    Console.WriteLine();
                    Console.WriteLine("编译成功。");
                    #region 保存已编译的宏
                    BinaryWriter bw=new BinaryWriter(File.Create(System.Windows.Forms.Application.StartupPath + "\\CompiledCache\\Macro" + macroData.MD5PART));
                    macro.Serialize(bw);
                    bw.Close();
                    #endregion
                }

                #region 输出宏信息
                Console.WriteLine();
                Console.WriteLine("宏说明：");
                Console.WriteLine(macro.Infomation);
                Console.WriteLine();
                foreach (var globleData in macro.ListGlobalData)
                {
                    Console.WriteLine("需要全局数据：" + globleData.ToTargetString());
                }
                foreach (var tmp in macro.ListTempFile)
                {
                    Console.WriteLine("缓存数据：" + tmp);
                }
                foreach (FRAResultCfg result in macro.ListResult)
                {
                    Console.WriteLine("输出结果：" + result.ToTargetString());
                } 
                #endregion
                Console.WriteLine();
                if (args.Length == 1)
                {
                    return 0;//如果只是编译，则到此为止
                }
                if (args.Length == 2&&args[1]=="deploy")
                {
                    #region 部署
                    var procSourcePath = System.IO.File.ReadAllLines(System.IO.File.ReadAllText(System.Windows.Forms.Application.StartupPath + "\\helperPath.txt").TrimEnd('\\') + "\\processDirs.txt");
                    var target = System.Windows.Forms.Application.StartupPath.TrimEnd('\\') +"\\DeployCache";
                    macro.Deploy(args[0], target, procPath, macroData, procSourcePath);
                    #endregion
                    Console.WriteLine();
                    Console.WriteLine("宏\"{0}\"部署完毕。",args[0]);
                    return 0;
                }
                #region 运行宏
                #region 载入全局数据
                try
                {
                    string[] curfiles = Directory.GetFiles(dataPath);
                    foreach (string curfile in curfiles)
                    {
                        File.Delete(curfile);
                    }
                    foreach (var globalData in macro.ListGlobalData)
                    {
                        if (globalData.ToTargetString() == "原图像.灰度图")
                        {
                            continue;
                        }
                        File.Copy( globalData.ToTargetString(), dataPath + "\\" + globalData.ToTargetString(), true);
                    }
                }
                catch (FileNotFoundException ex)
                {
                    string gName = ex.FileName.Substring(ex.FileName.LastIndexOf('\\') + 1);
                    throw (new MException(
                        macro.ListGlobalData.First(tg=>tg.ToTargetString()==ex.FileName).ContextOffset,
                        "加载",
                        "找不到所需要的全局数据“" + gName + "”"));
                }
                #endregion
                #region 获得待处理图像列表
                string[] fileNames;
                try
                {
                    if (File.Exists(args[1]))
                    {
                        fileNames = new string[args.Length - 1];
                        Array.Copy(args, 1, fileNames, 0, fileNames.Length);
                    }
                    else
                    {
                        List<string> fNames = new List<string>();
                        string[] sps = args[2].Split(';');
                        foreach (string sp in sps)
                        {
                            fNames.AddRange(Directory.GetFiles(args[1], sp));
                        }
                        fileNames = fNames.Distinct().ToArray();
                    }
                }
                catch (Exception ex)
                {
                    throw (new MException(0, "加载", "读取待处理图片列表时发生错误:" + ex.Message));
                }
                #endregion
                foreach (string fileName in fileNames)
                {
                    Console.WriteLine("尝试处理"+fileName+"...");
                    macro.Run(procPath,dataPath, fileName);
                    Console.WriteLine( fileName + "处理成功");
                    Console.WriteLine();
                }
                #endregion
                Console.WriteLine("宏运行结束。");
            }
            catch (MException ex)
            {
                #region 异常处理
                Console.WriteLine();
                Console.WriteLine(ex.ToString(allText));
                if (ex.Data["Children"] != null)
                {
                    var children = ex.Data["Children"] as List<MException>;
                    foreach (var child in children)
                    {
                        Console.WriteLine(child.ToString(allText));
                    }
                }
                return 1; 
                #endregion
            }
            return 0;
        }
    }
    class MException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MException"/> class.
        /// </summary>
        /// <param name="offset">错误坐标.</param>
        /// <param name="errtype">错误类型.</param>
        /// <param name="errdes">错误提示.</param>
        public MException(int offset, string errtype, string errdes)
        {
            this.offset = offset;
            this.errdes = errdes;
            this.errtype = errtype;
        }
        int offset;
        string errtype;
        string errdes;
        public string ToString(string context)
        {
            try
            {
                context = context.Remove(offset);
            }
            catch { }
            return errtype + "错误(第" + (context.Count<char>(tc => tc == '\n') + 1) + "行):" + errdes;
        }
    }
}
