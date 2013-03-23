using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
namespace Tool_MachingPerformance
{
    public partial class Form1 : Form
    {
        public Form1(string[] args)
        {
            InitializeComponent();
            if (args.Length != 0)
            {
                this.dataIdent = args[0];
                this.Text += "-" + this.dataIdent.Substring(this.dataIdent.LastIndexOf('\\') + 1);
            }
        }
        string dataIdent = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            #region 宏选项列表
            string[] anlymacros = Directory.GetFiles("..\\data", "*.宏");
            foreach (string anlymacro in anlymacros)
            {
                cAnlyMacro.Items.Add(anlymacro.Substring(anlymacro.LastIndexOf('\\') + 1));
                cMatchMacro.Items.Add(anlymacro.Substring(anlymacro.LastIndexOf('\\') + 1));
            } 
            #endregion
            #region 载入FVCDB
            if (!Directory.Exists("fvcdb"))
            {
                Directory.CreateDirectory("fvcdb");
            }
            string[] pics = Directory.GetFiles("fvcdb", "*.tif");
            List<string> fingers = new List<string>();
            foreach (string pic in pics)
            {
                string f = pic.Substring(pic.LastIndexOf('\\') + 1);
                f = f.Remove(f.IndexOf('_'));
                if (!fingers.Contains(f))
                {
                    fingers.Add(f);
                    int i = 1;
                ss:
                    try
                    {
                        imageList1.Images.Add(f, Image.FromFile("fvcdb\\" + f + "_" + i + ".tif"));
                    }
                    catch
                    {
                        i++;
                        goto ss;
                    }
                    ListViewItem item = new ListViewItem();
                    item.Text = f;
                    item.ImageKey = f;
                    List<ListViewItem> fpicsitems = new List<ListViewItem>();
                    ImageList imglist = new ImageList() { ColorDepth = ColorDepth.Depth8Bit, ImageSize = new Size(128, 128) };
                    string[] fpics = Directory.GetFiles("fvcdb", f + "_*.tif");
                    foreach (string fpic in fpics)
                    {
                        string fpicfname = fpic.Substring(fpic.LastIndexOf('\\') + 1);
                        imglist.Images.Add(fpicfname, Image.FromFile(fpic));
                        fpicsitems.Add(new ListViewItem(fpicfname, fpicfname));
                    }
                    item.Tag = new KeyValuePair<List<ListViewItem>, ImageList>(fpicsitems, imglist);
                    listView1.Items.Add(item);
                }
            } 
            #endregion
            #region 载入MatchingSchema
            if (this.dataIdent != "")
            {
                BinaryReader reader = new BinaryReader(File.OpenRead(this.dataIdent));
                FRADataStructs.DataStructs.MatchingSchema ms = new FRADataStructs.DataStructs.MatchingSchema();
                ms.Deserialize(reader);
                reader.Close();
                try
                {
                    this.cAnlyMacro.SelectedItem = ms.AMacro;
                    this.cAnlyResult.SelectedItem = ms.AResult;
                    this.cMatchMacro.SelectedItem = ms.MMacro;
                    this.cTgtMinuetia.SelectedItem = ms.MTargetInput;
                    this.cTmpMinuetia.SelectedItem = ms.MTemplateInput;
                    this.cMatchResult.SelectedItem = ms.MResult;
                    this.tMatchThreshold.Text = ms.MThreshold;
                }
                catch { }
                tabControl1.SelectedTab = tabPage2;
            }
            #endregion
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 1)
            {
                return;
            }
            listView2.Items.Clear();
            listView2.LargeImageList = ((KeyValuePair<List<ListViewItem>, ImageList>)listView1.SelectedItems[0].Tag).Value;
            listView2.Items.AddRange(((KeyValuePair<List<ListViewItem>, ImageList>)listView1.SelectedItems[0].Tag).Key.ToArray());
        }

        private void cAnlyMacro_SelectedIndexChanged(object sender, EventArgs e)
        {
            cAnlyResult.Items.Clear();
            if (this.cAnlyMacro.SelectedItem == null) { return; }
            try
            {
                pQuick.StartInfo.FileName = File.ReadAllText("macroruntimepath.txt") + "\\framacroruntime.exe";
                pQuick.StartInfo.Arguments = (string)cAnlyMacro.SelectedItem;
                pQuick.Start();
                pQuick.WaitForExit();
                while (!pQuick.StandardOutput.EndOfStream)
                {
                    string line = pQuick.StandardOutput.ReadLine();
                    if (line.Contains("错误(第"))
                    {
                        throw (new Exception("编译时发生错误："+line));
                    }
                    if (line.StartsWith("输出结果："))
                    {
                        cAnlyResult.Items.Add(line.Substring("输出结果：".Length));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.cAnlyMacro.SelectedItem = null;
            }
        }

        private void cAnlyResult_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cMatchMacro_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cTgtMinuetia.Items.Clear();
            this.cTmpMinuetia.Items.Clear();
            this.cMatchResult.Items.Clear();
            if (this.cMatchMacro.SelectedItem == null) { return; }
            try
            {
                pQuick.StartInfo.FileName = File.ReadAllText("macroruntimepath.txt") + "\\framacroruntime.exe";
                pQuick.StartInfo.Arguments = (string)cMatchMacro.SelectedItem;
                pQuick.Start();
                pQuick.WaitForExit();
                while (!pQuick.StandardOutput.EndOfStream)
                {
                    string line = pQuick.StandardOutput.ReadLine();
                    if (line.Contains("错误(第"))
                    {
                        throw (new Exception("编译时发生错误：" + line));
                    }
                    if (line.StartsWith("需要全局数据："))
                    {
                        this.cTgtMinuetia.Items.Add(line.Substring("需要全局数据：".Length));
                        this.cTmpMinuetia.Items.Add(line.Substring("需要全局数据：".Length));
                    }
                    if (line.StartsWith("输出结果："))
                    {
                        if (line.EndsWith(".浮点数"))
                        {
                            this.cMatchResult.Items.Add(line.Substring("输出结果：".Length));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.cAnlyMacro.SelectedItem = null;
            }
        }

        private void cTmpMinuetia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cTgtMinuetia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cMatchResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tMatchThreshold_TextChanged(object sender, EventArgs e)
        {

        }

        List<Error> lerr = new List<Error>();
        struct Error
        {
            public string Tmp;
            public string Tgt;
            public bool Faild;
            public bool SameFinger;
            public override string ToString()
            {
                return Tmp + (Faild ? (SameFinger ? " 错误拒绝 " : " 错误匹配 ") : (SameFinger ? " 成功匹配 " : " 成功拒绝 ")) + Tgt;
            }
        }
        private void bFRR_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            this.progress = 0;
            tabControl1.Enabled = false;
            t= new Thread(threadFRR);
            t.Start();
        }
        Thread t = null;
        void threadFRR()
        {
            try
            {
                Dictionary<string, FRADataStructs.DataStructs.DoubleFloatArray> arrs = new Dictionary<string, FRADataStructs.DataStructs.DoubleFloatArray>();
                foreach (string scoreName in this.cMatchResult.Items)
                {
                    arrs.Add(scoreName, new FRADataStructs.DataStructs.DoubleFloatArray());
                }
                lerr.Clear();
                Random r = new Random();
                int numPerFinger = 5;
                foreach (ListViewItem fingeritem in listView1.Items)
                {
                    for (int i = 0; i < numPerFinger; i++)
                    {
                        string[] pics = Directory.GetFiles("fvcdb", fingeritem.Text + "_*.tif");
                        string tmp = pics[r.Next() % pics.Length];
                        string tgt;
                        do
                        {
                            tgt = pics[r.Next() % pics.Length];
                        } while (tgt == tmp);
                        tmp = tmp.Substring(tmp.LastIndexOf('\\') + 1);
                        tgt = tgt.Substring(tgt.LastIndexOf('\\') + 1);
                        string[] keyNames = arrs.Keys.ToArray();
                        List<double> scores = match(tmp, tgt, keyNames);
                        for (int k = 0; k < arrs.Keys.Count; k++)
                        {
                            arrs[keyNames[k]].Add(scores[k]);
                        }
                        double d = scores[keyNames.ToList().IndexOf((string)cMatchResult.SelectedItem)];
                        if (d > this.dMatchThreshold)
                        {
                            lerr.Add(new Error() { Tmp = tmp, Tgt = tgt, SameFinger = true, Faild = true });
                        }
                        else
                        {
                            lerr.Add(new Error() { Tmp = tmp, Tgt = tgt, SameFinger = true, Faild = false });
                        }
                        this.progress += 1 / (double)(listView1.Items.Count * numPerFinger);
                    }
                }
                {
                    string[] keyNames = arrs.Keys.ToArray();
                    for (int k = 0; k < arrs.Keys.Count; k++)
                    {
                        BinaryWriter writer = new BinaryWriter(File.Create("FR样本." + cAnlyResult.SelectedItem + "." + keyNames[k] + ".数组"));
                        arrs[keyNames[k]].Serialize(writer);
                        writer.Close();
                    }
                }
                MessageBox.Show("匹配完毕，共匹配" + numPerFinger * listView1.Items.Count + "次,FRR=" + ((double)lerr.Count(te=>te.Faild) / (numPerFinger * listView1.Items.Count)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("匹配时发生错误：" + ex.Message);
            }
        }
        private void bFAR_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            this.progress = 0;
            tabControl1.Enabled = false;
            t = new Thread(threadFAR);
            t.Start();
        }
        void threadFAR()
        {
            try
            {
                Dictionary<string, FRADataStructs.DataStructs.DoubleFloatArray> arrs = new Dictionary<string, FRADataStructs.DataStructs.DoubleFloatArray>();
                foreach (string scoreName in this.cMatchResult.Items)
                {
                    arrs.Add(scoreName, new FRADataStructs.DataStructs.DoubleFloatArray());
                }
                lerr.Clear();
                Random r = new Random();
                int num = 100;
                for (int i = 0; i < num; i++)
                {
                    string f1 = listView1.Items[r.Next() % listView1.Items.Count].Text;
                    string f2;
                    do
                    {
                        f2 = listView1.Items[r.Next() % listView1.Items.Count].Text;
                    } while (f1 == f2);
                    string[] pics1 = Directory.GetFiles("fvcdb", f1 + "_*.tif");
                    string tmp = pics1[r.Next() % pics1.Length];
                    string[] pics2 = Directory.GetFiles("fvcdb", f2 + "_*.tif");
                    string tgt = pics2[r.Next() % pics2.Length];
                    tmp = tmp.Substring(tmp.LastIndexOf('\\') + 1);
                    tgt = tgt.Substring(tgt.LastIndexOf('\\') + 1);
                    string[] keyNames = arrs.Keys.ToArray();
                    List<double> scores = match(tmp, tgt, keyNames);
                    for (int k = 0; k < arrs.Keys.Count; k++)
                    {
                        arrs[keyNames[k]].Add(scores[k]);
                    }
                    double d = scores[keyNames.ToList().IndexOf((string)cMatchResult.SelectedItem)];
                    if (d < this.dMatchThreshold)
                    {
                        lerr.Add(new Error() { Tmp = tmp, Tgt = tgt, SameFinger = false, Faild = true });
                    }
                    else
                    {
                        lerr.Add(new Error() { Tmp = tmp, Tgt = tgt, SameFinger = false, Faild = false });
                    }
                    this.progress += 1 / (double)(num);
                }
                {
                    string[] keyNames = arrs.Keys.ToArray();
                    for (int k = 0; k < arrs.Keys.Count; k++)
                    {
                        BinaryWriter writer = new BinaryWriter(File.Create("FA样本." + cAnlyResult.SelectedItem + "." + keyNames[k] + ".数组"));
                        arrs[keyNames[k]].Serialize(writer);
                        writer.Close();
                    }
                }
                MessageBox.Show("匹配完毕，共匹配" + num + "次,FAR=" + ((double)lerr.Count(te => te.Faild) / (num)));
            }
            catch (Exception ex)
            {
                MessageBox.Show("匹配时发生错误：" + ex.Message);
            }
        }
        List<double> match(string file1, string file2,params string[] scoreNames)
        {
            List<double> scores = new List<double>();
            File.Copy("fvcdb\\" + file1 + ".宏处理结果." + this.cAnlyResult.SelectedItem, "..\\data\\" + this.cTmpMinuetia.SelectedItem, true);
            File.Copy("fvcdb\\" + file2 + ".宏处理结果." + this.cAnlyResult.SelectedItem, "..\\data\\" + this.cTgtMinuetia.SelectedItem, true);
            pQuick.StartInfo.Arguments = this.cMatchMacro.SelectedItem + " ..\\tools\\fvcdb\\" + file1;
            pQuick.Start();
            pQuick.WaitForExit();
            if (pQuick.ExitCode == 0)
            {
                foreach (string scoreName in scoreNames)
                {
                    FRADataStructs.DataStructs.DoubleFloat d = new FRADataStructs.DataStructs.DoubleFloat();
                    BinaryReader reader = new BinaryReader(File.OpenRead("fvcdb\\" + file1 + ".宏处理结果." + scoreName));
                    d.Deserialize(reader);
                    reader.Close();
                    scores.Add(d.Value);
                }
                return scores;
            }
            throw (new Exception(pQuick.StandardOutput.ReadToEnd()));
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.SelectionStart + textBox1.SelectionLength == textBox1.Text.Length)
            {
                textBox1.SelectionLength = 0;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }
        string output = "";
        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            output = "";
        }

        double dMatchThreshold;
        bool bReadyToMatch = false;
        bool bAnalysising = false;
        string tmpFilename = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = output;
            bAnyAll.Enabled =
                cAnlyMacro.SelectedItem != null &&
                cAnlyResult.SelectedItem != null &&
                (!this.bAnalysising);
            bFAR.Enabled =
                bFRR.Enabled =
                this.singleToolStripMenuItem.Enabled =
                bFAScoreSampling.Enabled =
                bFRScoreSampling.Enabled =
                this.bReadyToMatch &&
                cMatchMacro.SelectedItem != null &&
                cTgtMinuetia.SelectedItem != null &&
                cTmpMinuetia.SelectedItem != null &&
                cMatchResult.SelectedItem != null &&
                double.TryParse(tMatchThreshold.Text, out this.dMatchThreshold);
            this.singleToolStripMenuItem.Text = (this.tmpFilename == "") ? "用这个图像匹配" : "将这个图像与"+this.tmpFilename+"进行匹配";
            if (t != null)
            {
                if (!t.IsAlive)
                {
                    this.tabControl1.Enabled = true;
                    foreach (Error err in lerr)
                    {
                        listBox1.Items.Add(err);
                    }
                    t = null;
                }
                else
                {
                    this.progressBar1.Visible = true;
                    this.progressBar1.Value = (int)(this.progressBar1.Maximum * this.progress);
                }
            }
            else
            {
                this.progressBar1.Visible = false;
            }
        }
        double progress;
        private void bAnyAll_Click(object sender, EventArgs e)
        {
            this.cAnlyMacro.Enabled = this.cAnlyResult.Enabled = false;
            string arg = "";
            string[] files = Directory.GetFiles("fvcdb", "*.tif");
            foreach (string file in files)
            {
                if (File.Exists(file + ".宏处理结果." + cAnlyResult.SelectedItem))
                {
                    continue;
                }
                arg += " \"" + new FileInfo(file).FullName + "\"";
            }

            if (arg == "")//没有未处理的
            {
                this.bReadyToMatch = true;
                MessageBox.Show("没有需要分析的指纹，可以直接进行匹配。");
                return;
            }
            pAnalysis.StartInfo.FileName = File.ReadAllText("macroruntimepath.txt") + "\\FRAMacroRuntime.exe";
            pAnalysis.StartInfo.Arguments = (string)cAnlyMacro.SelectedItem + arg;
            pAnalysis.Start();
            pAnalysis.BeginOutputReadLine();
            tabControl1.SelectedTab = tabPage3;
            this.bAnalysising = true;
            
        }

        private void pAnalysis_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            this.output = e.Data + "\r\n" + this.output;
        }

        private void pAnalysis_Exited(object sender, EventArgs e)
        {
            this.pAnalysis.CancelOutputRead();
            this.bAnalysising = false;
            if (pAnalysis.ExitCode == 0)
            {
                MessageBox.Show("分析成功");
                this.bReadyToMatch = true;
            }
            else
            {
                MessageBox.Show("分析未成功");
                this.bReadyToMatch = false;
                this.cAnlyMacro.Enabled = this.cAnlyResult.Enabled = true;
            }
        }

        private void 取消当前操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.pAnalysis.Kill();
            }
            catch { }
        }

        private void singleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count != 1)
            {
                return;
            }
            if (this.tmpFilename == "")
            {
                this.tmpFilename = listView2.SelectedItems[0].Text;
            }
            else
            {
                try
                {
                    MessageBox.Show("匹配分数：" + match(this.tmpFilename, listView2.SelectedItems[0].Text,(string)cMatchResult.SelectedItem)[0].ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误：" + ex.Message);
                }
                this.tmpFilename = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region 保存MatchingSchema
            if (this.dataIdent != "")
            {
                BinaryWriter writer = new BinaryWriter(File.Create(this.dataIdent));
                FRADataStructs.DataStructs.MatchingSchema ms = new FRADataStructs.DataStructs.MatchingSchema();
                try
                {
                    ms.AMacro = (string)this.cAnlyMacro.SelectedItem;
                    ms.AResult = (string)this.cAnlyResult.SelectedItem;
                    ms.MMacro = (string)this.cMatchMacro.SelectedItem;
                    ms.MTargetInput = (string)this.cTgtMinuetia.SelectedItem;
                    ms.MTemplateInput = (string)this.cTmpMinuetia.SelectedItem;
                    ms.MResult = (string)this.cMatchResult.SelectedItem;
                    ms.MThreshold = (string)this.tMatchThreshold.Text;
                    ms.Serialize(writer);
                }
                catch { }
                writer.Close();
            }
            #endregion
            try
            {
                this.t.Abort();
            }
            catch { }
        }

        private void butClearAll_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles("fvcdb", "*.tif");
            foreach (string file in files)
            {
                File.Delete(file + ".宏处理结果." + cAnlyResult.SelectedItem);
            }
            this.bReadyToMatch = false;
        }

        private void 查看模板ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string tmp=((Error)listBox1.SelectedItem).Tmp;
                foreach (ListViewItem fingeritem in listView1.Items)
                {
                    fingeritem.Selected = fingeritem.Text == tmp.Remove(tmp.IndexOf('_'));
                }
                foreach (ListViewItem item in listView2.Items)
                {
                    item.Selected = item.Text == tmp;
                }
                tabControl1.SelectedTab=tabPage1;
                listView2.Focus();
            }
        }

        private void 查看目标ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string tgt = ((Error)listBox1.SelectedItem).Tgt;
                foreach (ListViewItem fingeritem in listView1.Items)
                {
                    fingeritem.Selected = fingeritem.Text == tgt.Remove(tgt.IndexOf('_'));
                } 
                foreach (ListViewItem item in listView2.Items)
                {
                    item.Selected = item.Text == tgt;
                }
                tabControl1.SelectedTab = tabPage1;
                listView2.Focus();
            }
        }

        private void 全部清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void 全部保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach(Error err in listBox1.Items)
            {
                if (!err.Faild)
                {
                    continue;
                }
                if (!File.Exists(folderBrowserDialog1.SelectedPath.TrimEnd('\\') + "\\" + err.Tgt))
                {
                    File.Copy("fvcdb\\" + err.Tgt, folderBrowserDialog1.SelectedPath.TrimEnd('\\') + "\\" + err.Tgt);
                }
                if (!File.Exists(folderBrowserDialog1.SelectedPath.TrimEnd('\\') + "\\" + err.Tmp))
                {
                    File.Copy("fvcdb\\" + err.Tmp, folderBrowserDialog1.SelectedPath.TrimEnd('\\') + "\\" + err.Tmp);
                }
            }
            MessageBox.Show("保存完毕");
        }

        private void bFAScoreSampling_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            this.progress = 0;
            tabControl1.Enabled = false;
            t = new Thread(threadFASS);
            t.Start();
        }
        void threadFASS()
        {
            try
            {
                lerr.Clear();
                progress = 0;
                foreach(string s in this.cMatchResult.Items)
                {
                    Process.Start("..\\MainGui.exe", "FA样本." + cAnlyResult.SelectedItem + "." + s + ".数组");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }

        private void bFRScoreSampling_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            this.progress = 0;
            tabControl1.Enabled = false;
            t = new Thread(threadFRSS);
            t.Start();
        }
        void threadFRSS()
        {
            try
            {
                lerr.Clear();
                progress = 0;
                foreach (string s in this.cMatchResult.Items)
                {
                    Process.Start("..\\MainGui.exe", "FR样本." + cAnlyResult.SelectedItem + "." + s + ".数组");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex.Message);
            }
        }


    }
}
