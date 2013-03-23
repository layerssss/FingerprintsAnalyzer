using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace DeployedMainGui
{
    public partial class FormRun : Form
    {
        public FormRun()
        {
            InitializeComponent();
        }
        public string Macro;
        private void FormRun_Load(object sender, EventArgs e)
        {
            timer1.Start();
            process1.StartInfo.Arguments = "\"..\\..\\Macros\\" + this.Macro + ".宏\"";
            foreach (var file in Directory.GetFiles("..\\DataCache"))
            {
                File.Delete(file);
            }
            this.process1.Start();
            this.process1.BeginOutputReadLine();
            tabControl1.SelectTab(tabPage2);
            this.listView1.Enabled = false;

            #region 源代码
            var reader = new BinaryReader(File.OpenRead("..\\Macros\\" + this.Macro + ".宏"));
            FRADataStructs.DataStructs.Macro m = new FRADataStructs.DataStructs.Macro();
            m.Deserialize(reader);
            reader.Close();
            reader = new BinaryReader(File.OpenRead("..\\Runtime\\CompiledCache\\Macro" + m.MD5PART));
            FRAMacroRuntime.FRAMacro mdata = new FRAMacroRuntime.FRAMacro();
            mdata.Deserialize(reader);
            reader.Close();
            foreach (var process in mdata.ListProcess)
            {
                var btn = new Button();
                btn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                btn.AutoSize = true;
                btn.TextAlign = ContentAlignment.BottomLeft;
                btn.Tag = process.Type + "\\FRAProcess_" + process.Name + ".cs";
                btn.Text = process.Type + '\\' + process.Name + ":\r\n" +
                    "    Inputs:\r\n";
                foreach (var io in process.CfgInputs)
                {
                    btn.Text += "        " + io.Ident + ':' + io.Target + '.' + io.Type + "\r\n";
                }
                btn.Text += "    Outputs:\r\n";
                foreach (var io in process.CfgOutputs)
                {
                    btn.Text += "        " + io.Ident + ':' + io.Target + '.' + io.Type + "\r\n";
                }
                btn.Click += new EventHandler(btn_Click);
                this.flowLayoutPanel1.Controls.Add(btn);
                btn.Dock = DockStyle.Top;
            }
            this.Text = "宏："+mdata.Infomation;
            #endregion
        }

        void btn_Click(object sender, EventArgs e)
        {
            var path = (sender as Control).Tag as string;
            try{
            System.Diagnostics.Process.Start("Process\\" + path);
            }
            catch{
                MessageBox.Show("该处理过程的源代码未发布。","无法查看源代码",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        string stringProcessInfo = "";

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool scroll = false;
            if (textBox1.SelectionStart == textBox1.Text.Length)
            {
                scroll = true;
            }
            textBox1.Text = stringProcessInfo;
            if (scroll)
            {
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }
        void writeProcessInfo(string str)
        {
            this.stringProcessInfo += DateTime.Now.ToLongTimeString() + ":" + str + "\r\n";
        }

        private void process1_Exited(object sender, EventArgs e)
        {
            process1.CancelOutputRead();
            this.writeProcessInfo("FRAMacroRuntime.exe已退出，返回代码：" + process1.ExitCode + "。\r\n");

            string[] results = Directory.GetFiles("data", "原图像.灰度图.宏处理结果.*");
            foreach (string result in results)
            {
                string fname = result.Substring(result.LastIndexOf('\\') + 1);
                File.Copy(result, "..\\DataCache\\" + fname.Substring("原图像.灰度图.宏处理结果.".Length));
                File.Delete(result);
            }
            tabControl1.SelectTab(tabPage1);
            this.refresh();
            this.listView1.Enabled = true;
        }
        void refresh()
        {
            this.listView1.Groups.Clear();
            FRADataStructs.DataStructTypes d = new FRADataStructs.DataStructTypes();
            foreach (var type in d.Keys)
            {
                var group = new ListViewGroup(type);
                this.listView1.Groups.Add(group);
                foreach (var file in Directory.GetFiles("..\\DataCache", "*." + type))
                {
                    var fname = file.Substring(file.LastIndexOf('\\') + 1);
                    var lvi = new ListViewItem()
                    {
                        Text = fname,
                        ImageIndex = 0,
                        Tag = new FileInfo(file)
                    };
                    this.listView1.Items.Add(lvi);
                    group.Items.Add(lvi);
                }

            }
        }
        private void process1_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                this.writeProcessInfo(e.Data);
            }
        }


        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            if (files.Length != 1)
            {

                e.Effect = DragDropEffects.None;
                return;
            }
            e.Effect = DragDropEffects.Copy;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
                var img = Image.FromFile(files[0]);
                var bmp = new Bitmap(img);
                img.Dispose();
                var gimg = FRADataStructs.DataStructs.GrayLevelImage.FromBitmap(bmp);
                bmp.Dispose();
                var writer = new BinaryWriter(File.Create("data\\原图像.灰度图"));
                gimg.Serialize(writer);
                writer.Close();
            }
            catch
            {
                MessageBox.Show("不能识别该图像文件的格式。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (var file in Directory.GetFiles("..\\DataCache"))
            {
                File.Delete(file);
            }
            File.Copy("data\\原图像.灰度图", "..\\DataCache\\原图像.灰度图", true);
            process1.StartInfo.Arguments = "\"..\\..\\Macros\\" + this.Macro + ".宏\" \"原图像.灰度图\"";
            process1.Start();
            process1.BeginOutputReadLine();
            tabControl1.SelectTab(tabPage2);
            this.listView1.Enabled = false;
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] files = new String[this.listView1.SelectedItems.Count];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = (this.listView1.SelectedItems[i].Tag as FileInfo).FullName;
            }
            DataObject data = new DataObject(DataFormats.FileDrop, files);
            DoDragDrop(data, DragDropEffects.Copy);
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 1)
            {
                var fi = this.listView1.SelectedItems[0].Tag as FileInfo;
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo() { FileName = "..\\bin\\MainGui.exe", Arguments = "\"" + (new FileInfo("..\\DataCache\\"+fi.Name)).FullName + "\"", WorkingDirectory = "..\\DataCache" });
            }
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            this.stringProcessInfo = "";
        }
    }
}
