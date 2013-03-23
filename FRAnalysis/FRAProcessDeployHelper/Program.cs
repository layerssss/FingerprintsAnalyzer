using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
namespace FRAProcessDeployHelper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string deployDir = (new DirectoryInfo(System.IO.File.ReadAllText(Application.StartupPath+"\\deployDir.txt"))).FullName+"\\";
            string[] proDirs = File.ReadAllLines(Application.StartupPath + "\\processDirs.txt");
            if (args.Length != 0)
            {
                Form1 f = new Form1(args[0]+"\\", args[1].Substring("FRAProcess_".Length), deployDir);
                f.ShowDialog();
                string solutionPath = (new DirectoryInfo(args[0])).Parent.Parent.Parent.FullName;
                if (!proDirs.Contains(solutionPath.ToUpper()))
                {
                    List<string> l = new List<string>();
                    l.AddRange(proDirs);
                    l.Add(solutionPath.ToUpper());
                    File.WriteAllLines(Application.StartupPath + "\\processDirs.txt", l.ToArray());
                }
                return;
            }
            List<string> notExist = new List<string>();
            foreach (string proDir in proDirs)
            {
                if (!Directory.Exists(proDir.ToUpper()))
                {
                    notExist.Add(proDir.ToUpper());
                    continue;
                }
                string[] projs = Directory.GetDirectories(proDir, "FRAProcess_*");
                Form1 f;
                foreach (string proj in projs)
                {
                    string processName = proj.Substring(proj.LastIndexOf('_') + 1);
                    string curv = File.GetLastWriteTime(proj + "\\bin\\Debug\\FRAProcess_" + processName + ".exe").ToString();
                    try
                    {
                        string deployedType = GetSolutionName(proDir);
                        string oldv = File.ReadAllText(deployDir + "process\\" + deployedType + "\\FRAProcess_" + processName + ".version");
                        if (oldv != curv)
                        {
                            throw (new Exception());
                        }
                    }
                    catch
                    {
                        f = new Form1(proj + "\\bin\\Debug\\", processName, deployDir);
                        f.ShowDialog();
                    }
                }
            }
            var newProDirs = Array.FindAll(proDirs, ts => !notExist.Contains(ts));
            var lnpd = new List<string>();
            foreach (var npd in newProDirs)
            {
                if (!lnpd.Contains(npd.ToUpper()))
                {
                    lnpd.Add(npd.ToUpper());
                }
            }

            File.WriteAllLines(Application.StartupPath + "\\processDirs.txt", lnpd);
        }
        public static string GetSolutionName(string path){
            var tdi=new DirectoryInfo(path);
            var di = tdi.Parent.GetDirectories();
            foreach (var sdi in di)
            {
                if (sdi.Name.ToUpper() == tdi.Name.ToUpper())
                {
                    return sdi.Name.Contains('_') ? sdi.Name.Substring(sdi.Name.IndexOf('_') + 1) : sdi.Name;
                }
            }
            throw (new Exception());
        }
    }
}
