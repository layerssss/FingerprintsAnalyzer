using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
namespace FRAMacroRuntime
{
    public class FRAMacro : FRADataStructs.IDataStruct
    {
        public List<FRAGlobalCfg> ListGlobalData;
        public List<string> ListTempFile;
        public List<FRAProcessCfg> ListProcess;
        public List<FRAResultCfg> ListResult;
        public string Infomation;
        public void SemanticCheck(string procPath, string dataPath)
        {
            this.argListCheck(procPath, dataPath);
            this.routeCheck();
        }
        void argListCheck(string procPath, string dataPath)
        {
            foreach (FRAProcessCfg proc in this.ListProcess)
            {
                #region 索取preinputs.txt
                Process p;
                try
                {
                    p = Process.Start(proc.GetProcessStartInfo(procPath, "preinputs"));
                    p.WaitForExit();
                }
                catch
                {
                    #region 尝试寻找正确的分类
                    var rightTypes = new LinkedList<string>();
                    var typePaths = Directory.GetDirectories(procPath);
                    foreach (var typePath in typePaths)
                    {
                        if (File.Exists(typePath + "\\FRAProcess_" + proc.Name + ".exe"))
                        {
                            rightTypes.AddLast(typePath.Substring(typePath.LastIndexOf('\\') + 1));
                        }
                    }
                    #endregion
                    throw (new MException(proc.ContextOffset, "过程", "找不到指定的处理过程,可能的正确分类有：" + rightTypes.Aggregate<string, string>("", (str1, str2) => str1 + "\"" + str2 + "\";")));
                }
                StreamReader sr;
                try
                {
                    sr = new StreamReader(procPath + "\\" + proc.Type + "\\preinputs.txt");
                }
                catch
                {
                    throw (new MException(proc.ContextOffset, "参数", "无法获取" + proc.Name + "的预输入参数列表，该处理可能不可用。"));
                }
                #endregion while (!sr.EndOfStream)
                {
                    #region 预输入数据
                    switch (sr.ReadLine())
                    {
                        case "input":
                            string itype = sr.ReadLine();
                            string iname = sr.ReadLine();
                            if (!proc.CfgInputs.Any(tc => tc.Type == itype && tc.Ident == iname))
                            {
                                throw (new MException(proc.ContextOffset, "参数", proc.Name + "的预输入参数“" + iname + "(" + itype + ")”未设置"));
                            }
                            #region 获取预输入数据
                            FRAProcessIOCfg input = proc.CfgInputs.Find(tc => tc.Type == itype && tc.Ident == iname);
                            try
                            {
                                File.Copy(input.ToTargetString(), p.StartInfo.WorkingDirectory + "\\" + input.ToArgString(), true);
                            }
                            catch
                            {
                                throw (new MException(input.ContextOffset, "参数", "找不到预输入数据" + input.ToTargetString()));
                            }
                            break;
                            #endregion
                        default:
                            break;
                    }
                    #endregion
                }
                sr.Close();
                #region 索取arglist.txt
                p = Process.Start(proc.GetProcessStartInfo(procPath, "arglist"));
                p.WaitForExit();
                try
                {
                    sr = new StreamReader(procPath + "\\" + proc.Type + "\\arglist.txt");
                }
                catch
                {
                    throw (new MException(proc.ContextOffset, "参数", "无法获取" + proc.Name + "的参数列表，该处理可能不可用。"));
                }
                #endregion
                while (!sr.EndOfStream)
                {
                    #region 检查参数是否够了，并做标记
                    switch (sr.ReadLine())
                    {
                        case "input":
                            string itype = sr.ReadLine();
                            string iname = sr.ReadLine();
                            try
                            {
                                proc.CfgInputs.Find(tc => tc.Type == itype && tc.Ident == iname).Tag = true;
                            }
                            catch
                            {
                                throw (new MException(proc.ContextOffset, "参数", proc.Name + "的输入参数“" + iname + "(" + itype + ")”未设置"));
                            }
                            break;
                        case "output":
                            string otype = sr.ReadLine();
                            string oname = sr.ReadLine();
                            try
                            {
                                proc.CfgOutputs.Find(tc => tc.Type == otype && tc.Ident == oname).Tag = true;
                            }
                            catch
                            {
                                throw (new MException(proc.ContextOffset, "参数", proc.Name + "的输出参数“" + oname + "(" + otype + ")”未设置"));
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                sr.Close();
                #region 检查是否有没有标记的参数
                FRAProcessIOCfg untagedIO;
                untagedIO = proc.CfgInputs.Find(tc => !tc.Tag);
                if (untagedIO != null)
                {
                    throw (new MException(untagedIO.ContextOffset, "参数", "过程“" + proc.Name + "”不需要输入参数“" + untagedIO.ToString() + "”"));
                }
                untagedIO = proc.CfgOutputs.Find(tc => !tc.Tag);
                if (untagedIO != null)
                {
                    throw (new MException(untagedIO.ContextOffset, "参数", "过程“" + proc.Name + "”不需要输出参数“" + untagedIO.ToString() + "”"));
                }
                #endregion
            }
        }
        void routeCheck()
        {
            this.ListTempFile = new List<string>();
            foreach (var global in this.ListGlobalData)
            {
                if (this.ListTempFile.Any(ttemp => ttemp == global.ToTargetString()))
                {
                    throw (new MException(
                        global.ContextOffset,
                        "参数",
                        "重复的全局数据配置。"));
                }
                this.ListTempFile.Add(global.ToTargetString());
            }
            var undefineExceptions = new List<MException>();
            foreach (FRAProcessCfg proc in this.ListProcess)
            {
                foreach (FRAProcessIOCfg input in proc.CfgInputs)
                {
                    if (!ListTempFile.Contains(input.ToTargetString()))
                    {
                        undefineExceptions.Add(new MException(
                            input.ContextOffset,
                            "参数",
                            "没有生成“" + input.ToTargetString() + "”，不能将它作为输入参数。"));
                    }
                    FRAGlobalCfg g = null;
                    if ((g = this.ListGlobalData.FirstOrDefault(tg => tg.ToTargetString() == input.ToTargetString())) != null)
                    {
                        g.Tag = true;
                    }
                }
                foreach (FRAProcessIOCfg output in proc.CfgOutputs)
                {
                    if (this.ListGlobalData.Any(tg => tg.ToTargetString() == output.ToTargetString()))
                    {
                        throw (new MException(
                            output.ContextOffset,
                            "参数",
                            "输出数据不能和全局数据重名，这样会修改全局数据。"));
                    }
                    ListTempFile.Add(output.ToTargetString());
                }
            }
            if (undefineExceptions.Any())
            {
                var ex = new MException(0, "参数", "使用了不存在的数据作为输入参数，是否忘记配置全局数据？");
                ex.Data["Children"] = undefineExceptions;
                throw (ex);
            }
            foreach (FRAResultCfg result in this.ListResult)
            {
                if (!ListTempFile.Contains(result.ToTargetString()))
                {
                    throw (new MException(result.ContextOffset, "参数", "没有生成“" + result.ToTargetString() + "”，不能将它配置为结果。"));
                }
                if (this.ListGlobalData.Any(tg => tg.ToTargetString() == result.ToTargetString()))
                {
                    throw (new MException(result.ContextOffset, "参数", "“" + result.ToTargetString() + "”为全局数据，不能被配置为结果。"));
                }
            }
            foreach (var global in this.ListGlobalData)
            {
                if (!global.Tag)
                {
                    throw (new MException(global.ContextOffset, "参数", "全局数据“" + global.ToTargetString() + "”重来没有被使用过。"));
                }
            }
        }
        public void Run(string procPath, string dataPath, string fileName)
        {
            #region 提取 原图像.灰度图
            try
            {
                if (fileName.EndsWith(".灰度图"))
                {
                    File.Copy(fileName, dataPath + "\\原图像.灰度图");
                }
                else
                {
                    FRADataStructs.DataStructs.GrayLevelImage img = FRADataStructs.DataStructs.GrayLevelImage.FromBitmap(new System.Drawing.Bitmap(fileName));
                    BinaryWriter writer = new BinaryWriter(File.Create(dataPath + "\\原图像.灰度图"));
                    img.Serialize(writer);
                    writer.Close();
                }
            }
            catch
            {
                throw (new MException(0, "运行时", fileName.Substring(fileName.LastIndexOf('\\') + 1) + " 无法读取。"));
            }
            #endregion
            foreach (FRAProcessCfg proc in this.ListProcess)
            {
                ProcessStartInfo processStartInfo = proc.GetProcessStartInfo(procPath, "process");
                try
                {
                    #region 输入参数
                    foreach (FRAProcessIOCfg input in proc.CfgInputs)
                    {
                        try
                        {
                            File.Copy(dataPath + "\\" + input.ToTargetString(), processStartInfo.WorkingDirectory + "\\" + input.ToArgString(), true);
                        }
                        catch
                        {
                            throw (new MException(input.ContextOffset, "运行时", "找不到输入数据" + input.ToTargetString()));
                        }
                    }
                    File.WriteAllText(processStartInfo.WorkingDirectory + "\\processingFilename.txt", fileName.Substring(fileName.LastIndexOf('\\') + 1));
                    File.WriteAllText(processStartInfo.WorkingDirectory + "\\processingFilepath.txt", fileName);
                    #endregion
                    #region 运行
                    Console.WriteLine(proc.Name + "...");
                    Process process = Process.Start(processStartInfo);
                    process.WaitForExit();
                    Console.Write(process.StandardOutput.ReadToEnd());
                    string exitCode = "不可用";
                    try
                    {
                        if (process.ExitCode != 0)
                        {
                            exitCode = process.ExitCode.ToString();
                            throw (new Exception());//因为可能有NotSupportException所以用双层try-catch
                        }
                    }
                    catch
                    {
                        throw (new MException(proc.ContextOffset, "运行时", "过程" + proc.Name + "运行时发生错误，错误代码（" + exitCode + "）。"));
                    }
                    Console.WriteLine(proc.Name + "成功。");
                    #endregion
                    #region 输出参数
                    foreach (FRAProcessIOCfg output in proc.CfgOutputs)
                    {
                        try
                        {
                            File.Copy(processStartInfo.WorkingDirectory + "\\" + output.ToArgString(), dataPath + "\\" + output.ToTargetString(), true);
                        }
                        catch
                        {
                            throw (new MException(output.ContextOffset, "运行时", "找不到输出数据" + output.ToTargetString()));
                        }
                    }
                    #endregion
                    #region 清理：不需要异常处理
                    foreach (FRAProcessIOCfg input in proc.CfgInputs)
                    {
                        File.Delete(processStartInfo.WorkingDirectory + "\\" + input.ToString());
                    }
                    foreach (FRAProcessIOCfg output in proc.CfgOutputs)
                    {
                        File.Delete(processStartInfo.WorkingDirectory + "\\" + output.ToString());
                    }
                    File.Delete(processStartInfo.WorkingDirectory + "\\processingFilename.txt");
                    #endregion
                }
                catch (MException ex)
                {
                    File.Delete(processStartInfo.WorkingDirectory + "\\processingFilename.txt");
                    File.Delete(processStartInfo.WorkingDirectory + "\\processingFilepath.txt");
                    throw (ex);
                }

            }
            #region 导出结果
            foreach (FRAResultCfg result in this.ListResult)
            {
                try
                {
                    File.Copy(dataPath + "\\" + result.ToTargetString(), fileName + ".宏处理结果." + result.ToTargetString(), true);
                }
                catch (FileNotFoundException)
                {
                    throw (new MException(result.ContextOffset, "运行时", "没有找到欲导出的结果“" + result.ToTargetString() + "”"));
                }
            }
            #endregion
        }
        public void Deploy(string macroFilename, string targetPath, string processPath, FRADataStructs.DataStructs.Macro data, string[] procSourcePath)
        {
            #region 数据
            foreach (var global in this.ListGlobalData)
            {
                if (global.ToTargetString() == "原图像.灰度图")
                {
                    continue;
                }
                File.Copy(global.ToTargetString(), targetPath + "\\bin\\Data\\" + global.ToTargetString(), true);
                File.SetAttributes(targetPath + "\\bin\\Data\\" + global.ToTargetString(), FileAttributes.Normal);
            }
            #endregion
            #region 宏
            #region dummyMacro
            {
                var writer = new BinaryWriter(File.Create(targetPath + "\\Macros\\" + macroFilename));
                data.Text = "";
                data.CalculateMD5 = false;
                data.Serialize(writer);
                writer.Close();
            }
            #endregion
            #region 填充缓存
            {
                var writer = new BinaryWriter(File.Create(targetPath + "\\Runtime\\CompiledCache\\Macro" + data.MD5PART));
                this.Serialize(writer);
                writer.Close();
            }
            #endregion
            #endregion
            #region 处理过程
            foreach (var proc in this.ListProcess)
            {
                var tProcPath = targetPath + "\\bin\\Process\\" + proc.Type;
                if (!Directory.Exists(tProcPath))
                {
                    Directory.CreateDirectory(tProcPath);
                    File.Copy(processPath + "\\" + proc.Type + "\\FRADataStructs.dll", tProcPath + "\\FRADataStructs.dll", true);
                    File.Copy(processPath + "\\" + proc.Type + "\\FRAProcess.dll", tProcPath + "\\FRAProcess.dll", true);
                }
                File.Copy(processPath + "\\" + proc.Type + "\\FRAProcess_" + proc.Name + ".exe", tProcPath + "\\FRAProcess_" + proc.Name + ".exe", true);
                File.Copy(processPath + "\\" + proc.Type + "\\FRAProcess_" + proc.Name + ".pdb", tProcPath + "\\FRAProcess_" + proc.Name + ".pdb", true);
                var souceSPath = procSourcePath[0];
                foreach (var line in procSourcePath)
                {
                    if (line.EndsWith("_" + proc.Type, StringComparison.OrdinalIgnoreCase))
                    {
                        souceSPath = line;
                    }
                }
                var source = new FileInfo(souceSPath + "\\FRAProcess_" + proc.Name + "\\FRAProcess_" + proc.Name + ".cs");
                if (source.Exists)
                {
                    source.CopyTo(tProcPath + "\\FRAProcess_" + proc.Name + ".cs", true);
                }
            }
            #endregion
        }
        #region IDataStruct 成员

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(this.Infomation);
            writer.Write(this.ListGlobalData.Count);
            foreach (var item in this.ListGlobalData)
            {
                item.Serialize(writer);
            }
            writer.Write(this.ListProcess.Count);
            foreach (var item in this.ListProcess)
            {
                item.Serialize(writer);
            }
            writer.Write(this.ListResult.Count);
            foreach (var item in this.ListResult)
            {
                item.Serialize(writer);
            }
            writer.Write(this.ListTempFile.Count);
            foreach (var item in this.ListTempFile)
            {
                writer.Write(item);
            }
        }

        public void Deserialize(BinaryReader reader)
        {
            int count;
            this.Infomation = reader.ReadString();

            count = reader.ReadInt32();
            this.ListGlobalData = new List<FRAGlobalCfg>();
            for (int i = 0; i < count; i++)
            {
                var g = new FRAGlobalCfg();
                g.Deserialize(reader);
                this.ListGlobalData.Add(g);
            }


            count = reader.ReadInt32();
            this.ListProcess = new List<FRAProcessCfg>();
            for (int i = 0; i < count; i++)
            {
                var item = new FRAProcessCfg();
                item.Deserialize(reader);
                this.ListProcess.Add(item);
            }

            count = reader.ReadInt32();
            this.ListResult = new List<FRAResultCfg>();
            for (int i = 0; i < count; i++)
            {
                var item = new FRAResultCfg();
                item.Deserialize(reader);
                this.ListResult.Add(item);
            }

            count = reader.ReadInt32();
            this.ListTempFile = new List<string>();
            for (int i = 0; i < count; i++)
            {
                this.ListTempFile.Add(reader.ReadString());
            }
        }

        public bool Present(FRADataStructs.DataStructs.GrayLevelImage originalImg, string dataIdent)
        {
            throw new NotImplementedException();
        }

        public FRADataStructs.IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }


    public class FRAProcessCfg : FRADataStructs.IDataStruct
    {
        public string Type;
        public string Name;
        public List<FRAProcessIOCfg> CfgInputs;
        public List<FRAProcessIOCfg> CfgOutputs;
        public int ContextOffset;
        public override string ToString()
        {
            return this.Name;
        }
        public ProcessStartInfo GetProcessStartInfo(string procPath, string cmd)
        {
            return new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = procPath + "\\" + Type + "\\FRAProcess_" + Name + ".exe",
                Arguments = "-" + cmd,
                WorkingDirectory = procPath + "\\" + Type,
                RedirectStandardOutput = true
            };
        }

        #region IDataStruct 成员

        public void Serialize(BinaryWriter writer)
        {

            writer.Write(this.ContextOffset);
            writer.Write(this.Name);
            writer.Write(this.Type);

            writer.Write(this.CfgInputs.Count);
            foreach (var item in this.CfgInputs)
            {
                item.Serialize(writer);
            }

            writer.Write(this.CfgOutputs.Count);
            foreach (var item in this.CfgOutputs)
            {
                item.Serialize(writer);
            }
        }

        public void Deserialize(BinaryReader reader)
        {
            this.ContextOffset = reader.ReadInt32();
            this.Name = reader.ReadString();
            this.Type = reader.ReadString();
            int count;

            count = reader.ReadInt32();
            this.CfgInputs = new List<FRAProcessIOCfg>();
            for (int i = 0; i < count; i++)
            {
                var item = new FRAProcessIOCfg();
                item.Deserialize(reader);
                this.CfgInputs.Add(item);
            }

            count = reader.ReadInt32();
            this.CfgOutputs = new List<FRAProcessIOCfg>();
            for (int i = 0; i < count; i++)
            {
                var item = new FRAProcessIOCfg();
                item.Deserialize(reader);
                this.CfgOutputs.Add(item);
            }
        }

        public bool Present(FRADataStructs.DataStructs.GrayLevelImage originalImg, string dataIdent)
        {
            throw new NotImplementedException();
        }

        public FRADataStructs.IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class FRAProcessIOCfg : FRADataStructs.IDataStruct
    {
        public string Ident;
        public string Type;
        public string Target;
        public int ContextOffset;
        public bool Tag = false;
        public override string ToString()
        {
            return this.Ident + "(" + this.Type + ")";
        }
        public string ToArgString()
        {
            return this.Ident + "." + this.Type;
        }
        public string ToTargetString()
        {
            return this.Target + "." + this.Type;
        }

        #region IDataStruct 成员

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(this.ContextOffset);
            writer.Write(this.Ident);
            writer.Write(this.Target);
            writer.Write(this.Type);
        }

        public void Deserialize(BinaryReader reader)
        {
            this.ContextOffset = reader.ReadInt32();
            this.Ident = reader.ReadString();
            this.Target = reader.ReadString();
            this.Type = reader.ReadString();
        }

        public bool Present(FRADataStructs.DataStructs.GrayLevelImage originalImg, string dataIdent)
        {
            throw new NotImplementedException();
        }

        public FRADataStructs.IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class FRAResultCfg : FRADataStructs.IDataStruct
    {
        public string Type;
        public string Name;
        public int ContextOffset;
        public string ToTargetString()
        {
            return Name + "." + Type;
        }

        #region IDataStruct 成员

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(this.ContextOffset);
            writer.Write(this.Name);
            writer.Write(this.Type);
        }

        public void Deserialize(BinaryReader reader)
        {
            this.ContextOffset = reader.ReadInt32();
            this.Name = reader.ReadString();
            this.Type = reader.ReadString();
        }

        public bool Present(FRADataStructs.DataStructs.GrayLevelImage originalImg, string dataIdent)
        {
            throw new NotImplementedException();
        }

        public FRADataStructs.IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
    public class FRAGlobalCfg : FRADataStructs.IDataStruct
    {
        public string Type;
        public string Name;
        public int ContextOffset;
        public bool Tag = false;
        public string ToTargetString()
        {
            return this.Name + "." + this.Type;
        }
        #region IDataStruct 成员

        public void Serialize(BinaryWriter writer)
        {
            writer.Write(this.Type);
            writer.Write(this.Name);
            writer.Write(this.ContextOffset);
        }

        public void Deserialize(BinaryReader reader)
        {
            this.Type = reader.ReadString();
            this.Name = reader.ReadString();
            this.ContextOffset = reader.ReadInt32();
        }

        public bool Present(FRADataStructs.DataStructs.GrayLevelImage originalImg, string dataIdent)
        {
            throw new NotImplementedException();
        }

        public FRADataStructs.IDataStruct BuildInstance()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
