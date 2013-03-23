using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Microsoft.Build.BuildEngine;
using FRADataStructs.DataStructs.Dialogs;


namespace Tool_ProcessesCleaner
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
            if (MessageBox.Show("即将删除所有处理过程并重新部署！", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                var ds = Directory.GetDirectories("..\\process");
                var progressD = Progress.Show("正在删除所有处理过程", "操作中，请稍候", ds.Length);
                var count = 0;
                foreach (var d in ds)
                {
                    var fs = Directory.GetFiles(d);
                    foreach (var f in fs)
                    {
                        if (f.ToLower().EndsWith(".info"))
                        {
                            continue;
                        }
                        File.Delete(f);
                    }
                    count++;
                    progressD.Elapse(count);
                }
                if (MessageBox.Show("已删除所有处理过程，是否重新部署？", "信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var lines = File.ReadAllLines(File.ReadAllLines("..\\processDeployHelperPath.txt")[0] + "\\processDirs.txt");
                    var failedSolution = "";
                    var vspath=System.Environment.GetEnvironmentVariable("VS100COMNTOOLS")+"..\\IDE";
                    var logPath = Application.StartupPath + "\\buildLog.txt";
                    var projFiles = new List<string>();
                    foreach (var line in lines)
                    {
                        var projDirs = Directory.GetDirectories(line, "FRAProcess_*");
                        foreach (var projDir in projDirs)
                        {
                            var projFile = projDir + projDir.Substring(projDir.LastIndexOf('\\')) + ".csproj";
                            projFile = projFile.ToUpper();
                            if (!projFiles.Contains(projFile))
                            {
                                projFiles.Add(projFile);
                            }
                        }
                    }
                    progressD = Progress.Show("正在重新编译所有处理过程", "操作中，请稍候", projFiles.Count);
                    for(int i=0;i<projFiles.Count;i++)
                    {
                        Microsoft.Build.Evaluation.ProjectCollection pc = Microsoft.Build.Evaluation.ProjectCollection.GlobalProjectCollection;
                        var proj = pc.LoadProject(projFiles[i]);
                        proj.SetProperty("PostBuildEvent", "");
                        if (!proj.Build())
                        {
                            failedSolution = projFiles[i];
                            break;
                        }
                        progressD.Elapse(i);
                    }
                    progressD.Elapse(projFiles.Count);
                    if (failedSolution != "")
                    {
                        if (MessageBox.Show("生成\""+ failedSolution + "\"的时候发生错误。是否打开查看？", "错误", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(failedSolution);
                            progressD.Elapse(projFiles.Count);
                        }
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                        {
                            WorkingDirectory = File.ReadAllLines("..\\processDeployHelperPath.txt")[0],
                            FileName = "FRAProcessDeployHelper.exe"
                        }).WaitForExit();
                        MessageBox.Show("已重新部署所有处理过程！", "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
