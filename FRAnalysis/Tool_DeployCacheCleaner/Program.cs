using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace Tool_DeployCacheCleaner
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var filenameDataStructs = "FRADataStructs.dll";

            var path = System.Windows.Forms.Application.StartupPath.TrimEnd('\\') ;
            var pathFmr = File.ReadAllLines(path + "\\..\\MacroRuntimePath.txt")[0].TrimEnd('\\');
            var pathMainGui = Directory.GetParent(path).FullName ;
            var pathProcess = pathMainGui + "\\process";
            var pathTools = pathMainGui + "\\tools";
            var pathPdh = File.ReadAllLines(path + "\\..\\ProcessDeployHelperPath.txt")[0].TrimEnd('\\') ;
            var pathDeployedMainGui = File.ReadAllLines(path + "\\..\\DeployedMainGuiPath.txt")[0].TrimEnd('\\');
            var pathDeployedGui = File.ReadAllLines(path + "\\..\\DeployedGuiPath.txt")[0].TrimEnd('\\');

            var tpath = pathFmr + "\\DeployCache";
            var tpathBin = tpath + "\\bin";
            var tpathBinTools = tpath + "\\bin\\Tools";
            var tpathBinProcess = tpath + "\\bin\\Process";
            var tpathBinData = tpath + "\\bin\\Data";
            var tpathRuntime = tpath + "\\Runtime";
            var tpathRuntimeCompiledCache = tpath + "\\Runtime\\CompiledCache";
            var tpathMacros = tpath + "\\Macros";
            var tpathDatacache = tpath + "\\DataCache";

            if (Directory.Exists(tpath))
            {
                Directory.Delete(tpath, true);
            }
            Directory.CreateDirectory(tpath);
            Directory.CreateDirectory(tpathBin);
            Directory.CreateDirectory(tpathBinTools);
            Directory.CreateDirectory(tpathBinProcess);
            Directory.CreateDirectory(tpathRuntime);
            Directory.CreateDirectory(tpathRuntimeCompiledCache);
            Directory.CreateDirectory(tpathMacros);
            Directory.CreateDirectory(tpathBinData);
            Directory.CreateDirectory(tpathDatacache);

            File.Copy(pathDeployedMainGui + "\\DeployedMainGui.exe", tpathBin + "\\MainGui.exe");
            File.Copy(pathDeployedMainGui + "\\FRAMacroRuntime.exe", tpathBin + "\\FRAMacroRuntime.exe");
            File.Copy(pathDeployedMainGui + "\\" + filenameDataStructs, tpathBin + "\\" + filenameDataStructs);
            File.WriteAllText(tpathBin + "\\MacroRuntimePath.txt", "..\\Runtime");

            FileCopyAll(pathTools, tpathBinTools,"*.exe","*.dll","*.config");
            File.Copy(pathDeployedGui + "\\FRADeployedGUI.exe", tpath + "\\FRADeployedGUI.exe");
            File.WriteAllText(tpathBin + "\\macroRuntimePath.txt", "..\\Runtime");

            FileCopyAll(pathFmr, tpathRuntime, "*.exe", "*.dll", "*.config");
            File.WriteAllText(tpathRuntime + "\\processPath.txt", "..\\Process");
        }
        static void FileCopyAll(string path1, string path2,params string[] patterns)
        {
            foreach (var pattern in patterns)
            {
                var files = new DirectoryInfo(path1).GetFiles(pattern);
                foreach (var file in files)
                {
                    file.CopyTo(path2 + "\\" + file.Name, true);
                }
            }
        }
        static void FileCopyAll(string path1, string path2)
        {
            FileCopyAll(path1, path2, "*.*");
        }
    }
}
