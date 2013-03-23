using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
namespace FRADataStructs.DataStructs.Controls
{
    public partial class FormMacro : Form
    {
        public FormMacro()
        {
            InitializeComponent();
        }
        GrayLevelImage img;
        public void LoadData(Macro data, GrayLevelImage img, string dataIdent)
        {
            this.img = img;
            m = data;
            if (img == null)
            {
                this.原图像ToolStripMenuItem.Enabled = false;
            }
            this.textBox1.Text = m.Text;
            oldText = m.Text;
            this.Text = "宏-" + dataIdent;
            this.dataIdent = dataIdent;
            this.fmrPath = File.ReadAllText(Application.StartupPath + "\\macroRunTimePath.txt");
        }
        string oldText;
        string dataIdent;
        Macro m;

        private void FormMacro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (textBox1.Text == oldText)
            {
                return;
            }
            switch (MessageBox.Show("内容已发生改动，是否保存？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case System.Windows.Forms.DialogResult.Yes:
                    m.Text = textBox1.Text;
                    ShouldSave = true;
                    break;
                case System.Windows.Forms.DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }
        public bool ShouldSave = false;
        string stringConsole;
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool scroll = false;
            int sel = textBox2.SelectionStart;
            if (textBox2.SelectionStart == textBox2.Text.Length)
            {
                scroll = true;
            }
            textBox2.Text = stringConsole;
            if (scroll)
            {
                textBox2.SelectionLength = 0;
                textBox2.SelectionStart = textBox2.Text.Length;
            }
            else
            {
                textBox2.SelectionStart = sel;
            }
            textBox2.ScrollToCaret();
        }

        private void process1_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data.StartsWith("缓存数据："))
                {
                    var str=e.Data.TrimEnd('\n');
                    str = str.TrimEnd('\r');
                    str = str.Substring(5);
                    this.tmpData.Add(str);
                    return;
                }
                stringConsole += DateTime.Now.ToLongTimeString() + ":" + e.Data + "\r\n";
            }
            catch { }
        }

        private void 编译ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteAndLaunch("");
        }
        string fmrPath;
        List<string> tmpData = new List<string>();
        void WriteAndLaunch(string moreargs)
        {
            BinaryWriter writer = new BinaryWriter(File.Create(dataIdent));
            this.m.Text = textBox1.Text;
            this.oldText = textBox1.Text;
            m.Serialize(writer);
            writer.Close();
            tmpData.Clear();
            this.stringConsole = DateTime.Now.ToLongTimeString() + ":" + "日志已清除。\r\n";
            process1.StartInfo.FileName = fmrPath + "\\FRAMacroRuntime.exe";
            process1.StartInfo.Arguments = "\"" + dataIdent + "\"" + moreargs;
            process1.Start();
            process1.BeginOutputReadLine();
            menuStrip1.Enabled = textBox1.Enabled = false;
        }
        private void process1_Exited(object sender, EventArgs e)
        {
            process1.CancelOutputRead();
            menuStrip1.Enabled = textBox1.Enabled = true;
            this.stringConsole += DateTime.Now.ToLongTimeString() + ":" + "FRAMacroRuntime.exe已退出，返回代码：" + process1.ExitCode + "。\r\n";

            #region 检查是否需要抽取全局数据
            var lines = this.stringConsole.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var globals = new List<string>();
            foreach (var line in lines)
            {
                try
                {
                    var str = line.Substring(9);
                    str = str.Substring(str.IndexOf(':') + 1);
                    if (str.StartsWith("没有生成") && str.EndsWith("，不能将它作为输入参数。"))
                    {
                        str = str.Substring(5);
                        str = str.Remove(str.LastIndexOf("”"));
                        globals.Add(str);
                    }
                }
                catch { }
            }
            if (globals.Count > 0)
            {
                var str = "";
                foreach (var global in globals)
                {
                    var i = global.LastIndexOf('.');
                    str += "MConfigGlobal(\"" + global.Substring(i + 1) + "\",\"" + global.Remove(i) + "\");\r\n";
                }
                if (MessageBox.Show("可能没有配置好足够的全局数据，是否将相关配置代码生成到剪切板？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    Clipboard.SetText(str, TextDataFormat.UnicodeText);
                }
            }
            #endregion
            if (needExport)
            {
                needExport = false;
                string[] results = Directory.GetFiles(Environment.CurrentDirectory, "原图像.灰度图.宏处理结果.*");
                saveFileDialog1.InitialDirectory = Environment.CurrentDirectory;
                foreach (string result in results)
                {
                    string fname = result.Substring(result.LastIndexOf('\\') + 1);
                    saveFileDialog1.FileName = fname.Remove(fname.LastIndexOf('.'));
                    saveFileDialog1.Filter = fname.Substring(fname.LastIndexOf('.') + 1) + "|*." + fname.Substring(fname.LastIndexOf('.') + 1);
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        if (result.ToLower() == saveFileDialog1.FileName.ToLower())
                        {
                            continue;
                        }
                        File.Copy(result, saveFileDialog1.FileName, true);
                    }
                    File.Delete(result);
                }
            }
        }

        private void 转到该错误所在行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] p1 = textBox2.Text.Remove(textBox2.SelectionStart).Split('\n');
            string[] p2 = textBox2.Text.Substring(textBox2.SelectionStart).Split('\r');
            string line = p1[p1.Length - 1] + p2[0];
            try
            {
                line = line.Substring(line.IndexOf("错误(第") + "错误(第".Length);
                line = line.Remove(line.IndexOf("行):"));
                int lineNum = int.Parse(line);
                int posStart=0;
                string temp = textBox1.Text;
                for (; lineNum != 1; lineNum--)
                {
                    posStart += temp.IndexOf('\r') + 2;
                    temp = textBox1.Text.Substring(posStart);
                }
                int posEnd = posStart + temp.IndexOf('\r');
                if (posEnd<posStart)
                {
                    posEnd = textBox1.Text.Length;
                }
                textBox1.Select(posStart, posEnd - posStart);
                textBox1.Focus();
                textBox1.ScrollToCaret();
            }
            catch { }
        }

        private void 清除整个日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stringConsole = DateTime.Now.ToLongTimeString() + ":" + "日志已清除。\r\n";
        }

        private void 单个图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
                if (openFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                string str = "";
                foreach (string s in openFileDialog1.FileNames)
                {
                    str += " \"" + s + "\"";
                }
                WriteAndLaunch(str);
        }
        FormMacroSearchPattern fff = new FormMacroSearchPattern();
        private void 所有图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (fff.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            WriteAndLaunch(" \"" + folderBrowserDialog1.SelectedPath + "\" \"" + fff.SearchPattern + "\"");
        }

        private void 运行ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 原图像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            needExport = true;
            WriteAndLaunch(" \"原图像.灰度图\"");
        }
        bool needExport = false;

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 清除已编译的缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Delete(fmrPath + "\\CompiledCache", true);
            }
            catch { }
            stringConsole += DateTime.Now.ToLongTimeString() + ":" + "已清除所有已编译内容的缓存\r\n";
        }

        private void 清除整个日志ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.清除整个日志ToolStripMenuItem_Click(null, null);
        }

        private void 结束宿主进程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.process1.Kill();
                stringConsole += DateTime.Now.ToLongTimeString() + ":" + "已强制杀死宏宿主进程\r\n";
            }
            catch(Exception ex) {

                stringConsole += DateTime.Now.ToLongTimeString() + ":" + "错误：" + ex.Message + "\r\n";
            }
        }

        private void 生成结果配置代码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var lines = this.stringConsole.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                var selected = new List<string>();
                foreach (var line in lines)
                {
                    var str = line.Substring(9);
                    str = str.Substring(str.IndexOf(':') + 1);
                    if (str.StartsWith("需要全局数据："))
                    {
                        str = str.Substring(7);
                        this.tmpData.RemoveAll(ttmp => ttmp == str);
                    }
                    if (str.StartsWith("输出结果："))
                    {
                        str = str.Substring(5);
                        selected.Add(str);
                    }
                }
                if (this.tmpData.Count > 0)
                {
                    var items = new List<string>();
                    foreach (var tmp in this.tmpData)
                    {
                        var i = tmp.LastIndexOf('.');
                        items.Add("MConfigResult(\"" + tmp.Substring(i + 1) + "\",\"" + tmp.Remove(i) + "\");");
                    }
                    var selectedItems = new List<string>();
                    foreach (var tmp in selected)
                    {
                        var i = tmp.LastIndexOf('.');
                        selectedItems.Add("MConfigResult(\"" + tmp.Substring(i + 1) + "\",\"" + tmp.Remove(i) + "\");");
                    }
                    Dialogs.MultiSelection f = new Dialogs.MultiSelection("生成结果配置代码", "请选择要作为结果的数据", items, selectedItems);
                    if (f.ShowDialog() == DialogResult.OK)
                    {
                        Clipboard.SetText(f.Result, TextDataFormat.UnicodeText);
                    }
                }
            }
            catch { }
        }

        private void 注释选定的语句ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "/*" + textBox1.SelectedText.Replace("\r\n","*/\r\n/*") + "*/";
        }

        private void 取消注释ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = textBox1.SelectedText.Replace("/*", "").Replace("*/", "");
            
        }

        private void 追加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteAndLaunch(" deploy");
        }

        private void 清除部署缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { FileName = "Tool_DeployCacheCleaner.exe", WorkingDirectory = Application.StartupPath + "\\Tools" }).WaitForExit();
            stringConsole += DateTime.Now.ToLongTimeString() + ":" + "部署缓存已清除\r\n";

        }

        private void 浏览目录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { FileName = fmrPath + "\\DeployCache" });
        }


    }
}
