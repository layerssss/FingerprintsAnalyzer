using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FRADataStructs;

namespace MainGui
{
    public partial class FormProcess : Form
    {
        public FormProcess()
        {
            InitializeComponent();
            this.backgroundProcess.StartInfo.UseShellExecute = false;
            this.backgroundProcess.StartInfo.CreateNoWindow = true;
            this.backgroundProcess.StartInfo.RedirectStandardOutput = true;
            string[] dirnames = Directory.GetDirectories("process");
            foreach (string dirname in dirnames)
            {
                toolComboProcessType.Items.Add(dirname.Substring(dirname.LastIndexOf('\\') + 1));
            }
            this.backgroundProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(backgroundProcess_OutputDataReceived);
            this.backgroundProcess.Exited += new EventHandler(backgroundProcess_Exited);
        }
        public void OpenTitle(string title)
        {
            if (title.Length > 4)
            {
                title = title.Substring(4);
                var i=title.IndexOf("->");
                if (i != -1)
                {
                    this.toolComboProcessType.SelectedIndex = this.toolComboProcessType.Items.IndexOf(title.Remove(i));
                    for(int j=0;j<this.toolComboProcess.Items.Count;j++){
                        if (((processItem)this.toolComboProcess.Items[j]).ToString() == title.Substring(i + 2))
                        {
                            this.toolComboProcess.SelectedIndex = j;
                            break;
                        }
                    }
                }
                else
                {
                    this.toolComboProcessType.SelectedIndex = this.toolComboProcessType.Items.IndexOf(title);
                }
            }
        }
        LocalDataList ListLocalData;
        
        System.Diagnostics.Process backgroundProcess = new System.Diagnostics.Process();
        List<ComboBox> processInputData = new List<ComboBox>();
        List<TextBox> processOutputData = new List<TextBox>();
        processItem process;

        bool isProcessing;
        void writeProcessInfo(string str)
        {
            this.stringProcessInfo += DateTime.Now.ToLongTimeString() + ":" + str + "\r\n";
        }
        void backgroundProcess_Exited(object sender, EventArgs e)
        {
            if (!isProcessing)
            {
                return;
            }
            try
            {
                backgroundProcess.CancelOutputRead();
            }
            catch { }
            #region 返回代码分析
            writeProcessInfo("已退出，返回代码：" + backgroundProcess.ExitCode.ToString());
            switch (this.backgroundProcess.ExitCode)
            {
                case -1:
                    writeProcessInfo("不可用");
                    break;
                case 0:
                    writeProcessInfo("成功");
                    break;
                case 1:
                    writeProcessInfo("错误的调用");
                    break;
                case 2:
                    writeProcessInfo("获取参数列表出错");
                    break;
                case 3:
                    writeProcessInfo("读取输入参数时出错");
                    break;
                case 4:
                    writeProcessInfo("运行处理时出错");
                    break;
                case 5:
                    writeProcessInfo("导出结果时出错");
                    break;
                default:
                    writeProcessInfo("未知");
                    break;
            }
            #endregion
            isProcessing = false;
            this.backgroundProcess.EnableRaisingEvents = false;
            #region 导出和清理数据
            #region 移出输出数据
            writeProcessInfo("生成结果：");
            #region 判断是否是全局过程
            bool isGlobalProcess = true;
            foreach (ComboBox c in this.processInputData)
            {
                if (ListLocalData.Contains((string)c.SelectedItem))
                {
                    isGlobalProcess = false;
                }
            }
            #endregion
            foreach (TextBox t in this.processOutputData)
            {
                processOutputItem output = (processOutputItem)t.Tag;
                if (File.Exists("process\\" + process.Type + "\\" + output.Name + "." + output.Type))
                {
                    File.Copy("process\\" + process.Type + "\\" + output.Name + "." + output.Type,
                        "data\\" + t.Text + "." + output.Type, true);//Move可能出错（重名）
                    File.Delete("process\\" + process.Type + "\\" + output.Name + "." + output.Type);
                    writeProcessInfo(t.Text + "." + output.Type + "(" + output.Name + ")");
                    if (!isGlobalProcess)
                    {
                        this.ListLocalData.Add(t.Text + "." + output.Type);
                    }
                }
            }
            #endregion
            #region 删除输入数据(如果还存在的话)
            foreach (ComboBox c in this.processInputData)
            {
                ProcessInputItem input = (ProcessInputItem)c.Tag;
                if (File.Exists("process\\" + process.Type + "\\" + input.Name + "." + input.Type))
                {
                    File.Delete("process\\" + process.Type + "\\" + input.Name + "." + input.Type);
                }
            }
            #endregion
            #endregion
        }
        string stringProcessInfo = "";
        bool isProcessHolding = false;

        int intProgressMax = 0;
        int intProgress = 0;
        void backgroundProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data == null)//空行的数据可能会是null
            {
                return;
            }
            if (e.Data.StartsWith("DEBUG:HOLD:"))
            {
                writeProcessInfo("过程已暂停：" + e.Data.Substring("DEBUG:HOLD:".Length) + "");
                this.isProcessHolding = true;
                return;
            }
            try
            {
                if (e.Data.StartsWith("DEBUG:PROGRESS:"))
                {
                    string str = e.Data.Substring("DEBUG:PROGRESS:".Length);
                    string[] strs = str.Split('/');
                    this.intProgress = int.Parse(strs[0]);
                    this.intProgressMax = int.Parse(strs[1]);
                    return;
                }
            }
            catch
            {
            }

            writeProcessInfo(":" + e.Data);

        }
        private void toolComboProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {

            toolComboProcess.Items.Clear();
            string[] filenames = Directory.GetFiles("process\\" + (string)toolComboProcessType.SelectedItem, "FRAProcess_*.exe");
            toolComboProcess.Text = "";
            toolComboProcess.Items.Clear();
            foreach (string filename in filenames)
            {
                try
                {
                    processItem process;

                    process.Name = filename.Substring(filename.LastIndexOf('\\') + 1);
                    process.Name = process.Name.Substring(process.Name.IndexOf('_') + 1);
                    process.Name = process.Name.Remove(process.Name.LastIndexOf('.'));
                    process.Info = File.ReadAllText("process\\" + (string)toolComboProcessType.SelectedItem + "\\FRAProcess_" + process.Name + ".info");
                    process.Type = (string)this.toolComboProcessType.SelectedItem;
                    toolComboProcess.Items.Add(process);
                }
                catch { }
            }
            toolComboProcess_SelectedIndexChanged(null, null);
            
        }

        void t_Enter(object sender, EventArgs e)
        {
            //((TextBox)sender).SelectAll();
        }

        FormMain FormMain
        {
            get
            {
                return this.Owner as FormMain;
            }
        }
        private void menuItemProcessOpStart_Click(object sender, EventArgs e)
        {
            this.intProgressMax = 0;
            if (isProcessHolding)
            {
                isProcessHolding = false;
                this.backgroundProcess.StandardInput.WriteLine("NEXT");
                writeProcessInfo("过程继续");
                return;
            }
            if (!isProcessing)
            {
                if (this.FormMain.MacroRecording)
                {
                    this.macroAppend("MConfigProcess(\"" + ((processItem)toolComboProcess.SelectedItem).Type + "\",\"");
                    this.macroAppend(((processItem)toolComboProcess.SelectedItem).Name + "\"){\r\n");
                    this.macroAppend("  input{\r\n");
                }
                foreach (ComboBox c in this.processInputData)
                {
                    ProcessInputItem input = (ProcessInputItem)c.Tag;
                    File.Copy(
                        "data\\" + (string)c.SelectedItem,
                        "process\\" + this.process.Type + "\\" + input.Name + "." + input.Type, true);
                    string str = (string)c.SelectedItem;
                    if (this.FormMain.MacroRecording)
                    {
                        this.macroAppend("    MConfigArgument(\"" + input.Type + "\",\"" + input.Name + "\",\"" + str.Remove(str.Length - input.Type.Length - 1) + "\");\r\n");
                    }
                }
                if (this.FormMain.MacroRecording)
                {
                    this.macroAppend("  }\r\n");
                    this.macroAppend("  output{\r\n");
                }
                foreach (TextBox t in this.processOutputData)
                {
                    processOutputItem output = (processOutputItem)t.Tag;
                    if (this.FormMain.MacroRecording)
                    {
                        this.macroAppend("    MConfigArgument(\"" + output.Type + "\",\"" + output.Name + "\",\"" + t.Text + "\");\r\n");
                    }
                }
                if (this.FormMain.MacroRecording)
                {
                    this.macroAppend("  }\r\n");
                    this.macroAppend("}\r\n");
                    if (this.macroAppendError)
                    {
                        this.macroAppendError = false;
                        MessageBox.Show("找不到宏指令录制点，请在录制的宏中第一条MConfigResult语句前添加注释“/*FRARecordingPosition*/”（不含双引号）后重试", "未录制任何宏指令", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                writeProcessInfo(((processItem)toolComboProcess.SelectedItem).Name + "开始");
                this.backgroundProcess.StartInfo.Arguments = "-process";
                switch ((string)this.toolComboProcessOpConfig.SelectedItem)
                {
                    case "直接运行":
                        this.backgroundProcess.StartInfo.Arguments += " -release";
                        break;
                    case "启动后等待":
                        this.backgroundProcess.StartInfo.Arguments += " -debug";
                        break;
                    case "测试":
                        this.backgroundProcess.StartInfo.Arguments += " -test";
                        break;
                    default:
                        writeProcessInfo("未选择任何有效启动配置");
                        return;
                }
                this.backgroundProcess.StartInfo.RedirectStandardInput = true;
                this.backgroundProcess.Start();
                this.isProcessing = true;
                this.backgroundProcess.BeginOutputReadLine();
                this.backgroundProcess.EnableRaisingEvents = true;
            }
        }

        private void menuItemProcessOpCancel_Click(object sender, EventArgs e)
        {

            if (isProcessing)
            {
                this.isProcessHolding = false;
                this.backgroundProcess.Kill();
                writeProcessInfo(((processItem)toolComboProcess.SelectedItem).Name + "已取消");
            }
        }

        private void menuItemProcessOpClearlog_Click(object sender, EventArgs e)
        {

            this.stringProcessInfo = "";
            writeProcessInfo("日志已清除");
        }

        private void timerProcessOutput_Tick(object sender, EventArgs e)
        {
            if (this.stringProcessInfo.Length > 1000000)
            {
                menuItemProcessOpCancel_Click(null, null);
                MessageBox.Show("命令行字符超过1000000，已强行终止处理过程", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.textProcessOp.Text = this.stringProcessInfo;
            this.progressProcessOp.Visible = this.isProcessing;
            this.progressProcessOp.Style = this.intProgressMax == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Continuous;
            if (this.intProgressMax != 0)
            {
                this.progressProcessOp.Maximum = intProgressMax;
                this.progressProcessOp.Value = intProgress;
            }
        }

        private void pageProcessOp_Enter(object sender, EventArgs e)
        {
            menuItemProcessOpStart.Enabled = false;
            foreach (ComboBox c in this.processInputData)
            {
                if (c.SelectedItem == null)
                {
                    return;
                }
            }
            foreach (TextBox t in this.processOutputData)
            {
                if (t.Text == "")
                {
                    return;
                }
            }
            menuItemProcessOpStart.Enabled = true;
        }
        void macroAppend(string text)
        {
            (this.Owner as FormMain).MacroAppend(text);
        }
        bool macroAppendError{
            get{
                return (this.Owner as FormMain).MacroAppendError;
            }
            set{
                (this.Owner as FormMain).MacroAppendError=value;
            }
        }

        private void textProcessOp_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            t.SelectionLength = t.Text.Length;
            t.ScrollToCaret();
        }

        private void menuItemProcessResetargs_Click(object sender, EventArgs e)
        {
            toolComboProcess_SelectedIndexChanged(null, null);
            
        }

        private void toolComboProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Text = "处理";
                if (toolComboProcessType.SelectedItem != null)
                {
                    this.Text += "->" + (string)toolComboProcessType.SelectedItem;
                }
                if (toolComboProcess.SelectedItem == null)
                {
                    tabProcess.Visible = false;
                    return;
                }
                this.process = ((processItem)toolComboProcess.SelectedItem);
                this.Text += "->"+ this.process.Name;
                textProcessInfo.Text = this.process.Info;
                File.Delete("process\\" + (string)toolComboProcessType.SelectedItem + "\\arglist.txt");
                File.Delete("process\\" + (string)toolComboProcessType.SelectedItem + "\\preinputs.txt");
                this.backgroundProcess.StartInfo.WorkingDirectory = "process\\" + (string)toolComboProcessType.SelectedItem;
                this.backgroundProcess.StartInfo.FileName = this.backgroundProcess.StartInfo.WorkingDirectory + "\\FRAProcess_" + ((processItem)toolComboProcess.SelectedItem).Name + ".exe";
                this.backgroundProcess.StartInfo.Arguments = "-preinputs";
                this.backgroundProcess.Start();
                this.backgroundProcess.WaitForExit();
                StreamReader preinputsr = File.OpenText("process\\" + process.Type + "\\preinputs.txt");
                List<KeyValuePair<ProcessInputItem, string>> preInputList = new List<KeyValuePair<ProcessInputItem, string>>();
                if (!preinputsr.EndOfStream)
                {
                    FormPreInputs preInputForm = new FormPreInputs(preinputsr);
                    if (preInputForm.ShowDialog() == DialogResult.Cancel)
                    {
                        return;
                    }
                    foreach (KeyValuePair<ProcessInputItem, string> preinput in preInputForm.PreInputs)
                    {
                        preInputList.Add(preinput);
                        new FileInfo("data\\" + preinput.Value).CopyTo(
                            "process\\" + process.Type +
                            "\\" + preinput.Key.Name + "." + preinput.Key.Type, true);
                    }
                }
                else
                {
                    preinputsr.Close();
                }
                this.backgroundProcess.StartInfo.Arguments = "-arglist";
                this.backgroundProcess.Start();
                this.backgroundProcess.WaitForExit();
                if (this.backgroundProcess.ExitCode != 0)
                {
                    throw (new Exception(this.backgroundProcess.StandardOutput.ReadToEnd()));
                }
                StreamReader sr = File.OpenText("process\\" + (string)toolComboProcessType.SelectedItem + "\\arglist.txt");

                processInputData.Clear();
                processOutputData.Clear();

                tableProcessInput.Controls.Clear();
                tableProcessInput.RowCount = 1;

                tableProcessOutput.Controls.Clear();
                tableProcessOutput.RowCount = 1;

                while (!sr.EndOfStream)
                {
                    switch (sr.ReadLine())
                    {
                        case "input":
                            ProcessInputItem input = new ProcessInputItem(sr.ReadLine(), sr.ReadLine());
                            Label l = new Label() { Text = input.Name + "(" + input.Type + "):", AutoSize = true };
                            ComboBox c = new ComboBox();
                            c.Dock = DockStyle.Fill;
                            tableProcessInput.RowCount += 1;
                            tableProcessInput.Controls.Add(l);
                            tableProcessInput.Controls.Add(c);
                            tableProcessInput.SetRow(l, tableProcessInput.RowCount - 2);
                            tableProcessInput.SetRow(c, tableProcessInput.RowCount - 2);
                            tableProcessInput.SetColumn(l, 0);
                            tableProcessInput.SetColumn(c, 1);
                            string[] datas = Directory.GetFiles("data", "*." + input.Type);
                            foreach (string data in datas)
                            {
                                c.Items.Add(new FileInfo(data).Name);
                                if ((string)c.Items[c.Items.Count - 1] == input.Name + "." + input.Type)
                                {
                                    c.SelectedIndex = c.Items.Count - 1;
                                }
                            }
                            if (preInputList.Any(ti => ti.Key.Name == input.Name && ti.Key.Type == input.Type))
                            {
                                c.SelectedItem = preInputList.Find(ti => ti.Key.Name == input.Name && ti.Key.Type == input.Type).Value;
                                c.Enabled = false;
                            }
                            c.Tag = input;
                            this.processInputData.Add(c);
                            break;
                        case "output":
                            processOutputItem output = new processOutputItem(sr.ReadLine(), sr.ReadLine());
                            Label l2 = new Label() { Text = output.Name + "(" + output.Type + "):", AutoSize = true };
                            TextBox t = new TextBox();
                            t.Dock = DockStyle.Fill;
                            tableProcessOutput.RowCount += 1;
                            tableProcessOutput.Controls.Add(l2);
                            tableProcessOutput.Controls.Add(t);
                            tableProcessOutput.SetRow(l2, tableProcessOutput.RowCount - 2);
                            tableProcessOutput.SetRow(t, tableProcessOutput.RowCount - 2);
                            tableProcessOutput.SetColumn(l2, 0);
                            tableProcessOutput.SetColumn(t, 1);
                            t.Text = output.Name;
                            t.Tag = output;
                            t.Click += new EventHandler(t_Enter);
                            this.processOutputData.Add(t);

                            break;
                        default:
                            sr.Close();
                            throw (new Exception());
                    }
                }
                //tableProcessInput.RowCount -= 1;
                sr.Close();
                tabProcess.Visible = true;
                tabProcess.SelectedTab = pageProcessInfo;
                toolComboProcessOpConfig.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                tabProcess.Visible = false;
                MessageBox.Show("无法获取该处理过程的参数列表:\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormProcess_Load(object sender, EventArgs e)
        {

            this.ListLocalData = (this.Owner as FormMain).ListLocalData;
        }


    }
}
