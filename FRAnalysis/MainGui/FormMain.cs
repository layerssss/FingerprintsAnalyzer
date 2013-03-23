using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FRADataStructs;
using System.IO;
using System.Runtime.InteropServices;
namespace MainGui
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.strMacroTemplate = textMacro.Text;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 9999)
            {
                this.showToolStripMenuItem_Click(null, null);
            }
            base.WndProc(ref m);
        }
        string strMacroTemplate;
        public DataStructTypes DataStructTypes = new DataStructTypes();
        public void DataTypesRefresh()
        {
            this.listViewData.Items.Clear();
            this.dataToolStripMenuItem.DropDownItems.Clear();
            while (this.imageListTool.Images.Count > 2)
            {
                this.imageListTool.Images.RemoveAt(this.imageListTool.Images.Count - 1);
            }
            foreach (KeyValuePair<string, DataStructInitialHandler> type in this.DataStructTypes)
            {
                var count = Directory.GetFiles(Application.StartupPath + "\\data", "*." + type.Key).Count(ts => this.ListLocalData.Contains(ts.Substring(ts.LastIndexOf('\\') + 1)));
                var lvi = new ListViewItem() { 
                    Text = type.Key+"("+count+")", 
                    ImageIndex = 1 ,
                    Tag=type.Key,
                    ForeColor=count==0?Color.Gray:Color.Black
                };
                this.listViewData.Items.Add(lvi);
                var ddm = new ToolStripMenuItem()
                {
                    Text = type.Key + "(" + count + ")",
                    Image=this.imageListTool.Images[1],
                    ForeColor = count == 0 ? Color.Gray : Color.Black,
                    Tag=type.Key
                };
                ddm.Click += new EventHandler(menuNotifyDataItem_Click);
                this.dataToolStripMenuItem.DropDownItems.Add(ddm);
                var dis = Directory.GetFiles(Application.StartupPath + "\\dataIcons", type.Key + ".*");
                if (dis.Length != 0)
                {
                    try
                    {
                        var img = Image.FromFile(dis[0]);
                        this.imageListTool.Images.Add(type.Key, new Bitmap(img));
                        img.Dispose();
                        lvi.ImageKey = type.Key;
                        ddm.Image = this.imageListTool.Images[type.Key];
                    }
                    catch
                    {
                    }
                }
            }
        }

        void menuNotifyDataItem_Click(object sender, EventArgs e)
        {
            var type = ((sender as ToolStripItem).Tag as string);
            var f = new FormDataList(type);
            f.Show(this);
            this.OpenForms.Add(f);
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            this.DataTypesRefresh();
            butImgLast_Click(null, null);
            string[] tools = Directory.GetFiles("tools", "*.exe");
            foreach (string tool in tools)
            {
                if (tool.ToLower().EndsWith(".vshost.exe"))
                {
                    continue;
                }
                this.listViewTool.Items.Add(tool.Substring(tool.LastIndexOf('\\') + 1), 0);
            }
            this.pageData.Enabled = false;
            Taskbar t = new Taskbar();
            t.Show(this);
            this.OpenForms.Add(t);
        }
        private void butImgBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserImg.ShowDialog() == DialogResult.OK)
            {
                imgRefresh();
            }
        }
        private void butImgLast_Click(object sender, EventArgs e)
        {
            try
            {
                folderBrowserImg.SelectedPath = File.ReadAllText("lastopenimgpath.txt");
                imgRefresh();
            }
            catch { }
        }
        void imgRefresh()
        {

            listViewLoad.Items.Clear();
            string[] filenames = Directory.GetFiles(folderBrowserImg.SelectedPath);
            foreach (string filename in filenames)
            {
                try
                {
                    imageListImg.Images.Add(filename, Image.FromFile(filename));
                    listViewLoad.Items.Add(filename.Substring(filename.LastIndexOf('\\') + 1) + (Directory.Exists(filename + ".localdata") ? "(*)" : ""), filename);
                }
                catch { }
            }
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            var openData = new List<string>();
            var openProc = new List<string>();
            foreach (var f in this.OpenForms)
            {
                try
                {
                    if (!f.IsDisposed)
                    {
                        if (f is FormDataList)
                        {
                            var tf=(f as FormDataList);
                            openData.Add(string.Format("{0},{1},{2},{3},{4}", tf.Type, tf.Left, tf.Top, tf.Width, tf.Height));
                        }
                        if (f is FormProcess)
                        {
                            var tf = (f as FormProcess);
                            openProc.Add(string.Format("{0},{1},{2},{3},{4}", tf.Text, tf.Left, tf.Top, tf.Width, tf.Height));
                        }
                        f.Close();
                    }
                }
                catch {
                    MessageBox.Show("无法关闭子窗口，请关闭子窗口后重试！\r\n" , "请先关闭所有子窗口", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (folderBrowserImg.SelectedPath.Length != 0)
            {
                File.WriteAllText("lastOpenImgPath.txt", folderBrowserImg.SelectedPath);
            }
            if (pictureView.Image != null)
            {
                File.WriteAllLines("lastOpenData.txt", openData.ToArray());
                File.WriteAllLines("lastOpenProc.txt", openProc.ToArray());
                localDataSave();
            }
        }

        private void butImgRefresh_Click(object sender, EventArgs e)
        {
            imgRefresh();
        }

        private void listViewLoad_DoubleClick(object sender, EventArgs e)
        {
            if (listViewLoad.SelectedItems.Count == 1)
            {
                this.butImgBrowse.Enabled = false;
                if (pictureView.Image == null)
                {
                    try
                    {
                        var openData = File.ReadAllLines("lastOpenData.txt");
                        var openProc = File.ReadAllLines("lastOpenProc.txt");
                        foreach (var data in openData)
                        {
                            var td = data.Split(',');
                            var f = new FormDataList(td[0]);
                            f.Show(this);
                            f.Left = Convert.ToInt32(td[1]);
                            f.Top = Convert.ToInt32(td[2]);
                            f.Width = Convert.ToInt32(td[3]);
                            f.Height = Convert.ToInt32(td[4]);
                            this.OpenForms.Add(f);
                        }
                        foreach (var data in openProc)
                        {
                            var td = data.Split(',');
                            var f = new FormProcess();
                            f.OpenTitle(td[0]);
                            f.Show(this);
                            f.Left = Convert.ToInt32(td[1]);
                            f.Top = Convert.ToInt32(td[2]);
                            f.Width = Convert.ToInt32(td[3]);
                            f.Height = Convert.ToInt32(td[4]);
                            this.OpenForms.Add(f);
                        }
                    }
                    catch { }
                }
                else
                {
                    localDataSave();
                }
                string filename = listViewLoad.SelectedItems[0].ImageKey;
                pictureView.Image = Image.FromFile(filename);
                pictureView.Tag = filename.Substring(filename.LastIndexOf('\\') + 1) + ".localdata";//数据集名称
                localDataLoad();
                menuItemDataLoad_Click(null, null);
                File.WriteAllLines("lastOpenImgFilename.txt", new[] { filename });
                foreach (var f in this.OpenForms)
                {
                    if (!f.IsDisposed && f is FormDataList)
                    {
                        (f as FormDataList).RefreshList();
                    }
                }

                this.pageData.Enabled  = true;
                this.dataToolStripMenuItem.Enabled = true;
                this.processToolStripMenuItem.Enabled = true;
                this.DataTypesRefresh();
                this.tabMain.SelectedTab = pageData;
            }
        }
        /// <summary>
        /// 打开数据集
        /// </summary>
        void localDataLoad()
        {

            DirectoryInfo di = new DirectoryInfo(folderBrowserImg.SelectedPath + "\\" + (string)pictureView.Tag);
            if (!di.Exists)
            {
                di.Create();
            }
            foreach (FileInfo fi in di.GetFiles())
            {
                fi.CopyTo("data\\" + fi.Name, true);//全局数据中可能会有重名的文件，直接Move可能会出错
                fi.Delete();
                ListLocalData.Add(fi.Name);
            }
        }
        /// <summary>
        /// 保存数据集
        /// </summary>
        void localDataSave()
        {
            DirectoryInfo di = new DirectoryInfo(folderBrowserImg.SelectedPath + "\\" + (string)pictureView.Tag);
            if (!di.Exists)
            {
                di.Create();
            }
            foreach (string localData in ListLocalData)
            {
                try
                {
                    File.Move("data\\" + localData, di.FullName + "\\" + localData);
                }
                catch { }
            }
            ListLocalData.Clear();
        }
        public LocalDataList ListLocalData = new LocalDataList();

        public void MacroAppend(string text)
        {
            if (this.textMacro.Text.Contains("/*FRARecordingPosition*/"))
            {
                int i = this.textMacro.Text.IndexOf("/*FRARecordingPosition*/");
                this.textMacro.Text =
                    this.textMacro.Text.Remove(i) +
                    text +
                    this.textMacro.Text.Substring(i);
            }
            else
            {
                this.MacroAppendError = true;
            }
        }
        public bool MacroAppendError = false;

        private void menuItemDataLoad_Click(object sender, EventArgs e)
        {
            try
            {
                FRADataStructs.DataStructs.GrayLevelImage img = FRADataStructs.DataStructs.GrayLevelImage.FromBitmap((Bitmap)pictureView.Image);
                BinaryWriter writer = new BinaryWriter(File.Create("data\\原图像.灰度图"));
                img.Serialize(writer);
                writer.Close();
                //MessageBox.Show("载入成功，新数据名称为：原图像.灰度图", "载入成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                ListLocalData.Add("原图像.灰度图");
            }

            catch (Exception ex)
            {
                MessageBox.Show("读取原图像时发生以下错误，请正常载入原图像后再尝试：\r\n" + ex.Message, "读取原图像时发生错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void butMacroNew_Click(object sender, EventArgs e)
        {
            this.textMacro.Text = this.strMacroTemplate;
        }
        public bool MacroRecording
        {
            get
            {
                return this.checkMacroRecording.Checked;
            }
        }

        private void listViewTool_DoubleClick(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewTool.SelectedItems)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    WorkingDirectory="tools",
                    FileName=item.Text
                });
            }
        }

        private void menuItemToolBrowse_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "tools");
        }
        public List<Form> OpenForms = new List<Form>();




        private void dataOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem i in this.listViewData.SelectedItems)
            {
                var f = new FormDataList(i.Tag as string);
                f.Show(this);
                this.OpenForms.Add(f);
            }
        }

        private void processOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormProcess f = new FormProcess();
            f.Show(this);
            this.OpenForms.Add(f);
        }

        private void dataSetIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog f = new OpenFileDialog() { Multiselect = false, Filter = "图片文件|*.jpg;*.jpeg;*.gif;*.bmp;*.png;*.tif;*.ico|所有文件(*.*)|*.*" };
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.imageListTool.Images.RemoveByKey(listViewData.SelectedItems[0].Tag as string);
                listViewData.SelectedItems[0].ImageIndex = 1;
                var dis = Directory.GetFiles(Application.StartupPath + "\\dataIcons", listViewData.SelectedItems[0].Tag as string + ".*");
                foreach (var file in dis)
                {
                    File.Delete(file);
                }
                var target = Application.StartupPath + "\\dataIcons\\" + listViewData.SelectedItems[0].Tag as string + f.FileName.Substring(f.FileName.LastIndexOf('.'));
                File.Copy(f.FileName,target);
                this.DataTypesRefresh();
            }
        }

        private void contextMenuData_Opening(object sender, CancelEventArgs e)
        {
            dataSetIconToolStripMenuItem.Enabled = listViewData.SelectedItems.Count == 1;
            dataOpenToolStripMenuItem.Enabled = listViewData.SelectedItems.Count != 0;
        }

        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = ((e.KeyState & 8) == 8) ? DragDropEffects.Copy : DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.AllowedEffect.HasFlag(DragDropEffects.Copy))
            {
                return;
            }
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            foreach (var file in files)
            {
                var ext = file.ToLower().Substring(file.LastIndexOf('.') + 1);
                try
                {
                    var data = this.DataStructTypes.GetNew(ext);
                    var fi = new FileInfo(file);
                    if ((e.KeyState & 8) == 8)
                    {
                        fi.CopyTo(Application.StartupPath + "\\data\\" + fi.Name.Remove(fi.Name.Length - ext.Length) + ext, true);
                    }
                    else
                    {
                        fi.MoveTo(Application.StartupPath + "\\data\\" + fi.Name.Remove(fi.Name.Length - ext.Length) + ext);
                    }
                    this.ListLocalData.Remove(fi.Name.Remove(fi.Name.Length - ext.Length) + ext);
                }
                catch { }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DataTypesRefresh();
        }
        public FormWindowState OldWindowState;

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notify_MouseClick(null, new MouseEventArgs(System.Windows.Forms.MouseButtons.Left, 0, 0, 0, 0));
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.processOpenToolStripMenuItem_Click(null, null);
            
        }

        private void notify_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.ShowInTaskbar = true;
                    this.WindowState = this.OldWindowState;
                }
                else
                {
                    this.OldWindowState = this.WindowState;
                    this.WindowState = FormWindowState.Minimized;
                }
            }
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                (new System.Threading.Thread(() =>
                {
                    System.Threading.Thread.Sleep(200);
                    this.Invoke(new MethodInvoker(() => { this.ShowInTaskbar = false; }));
                })).Start();
            }
            else
            {
            }
        }

        private void btnImgLast_Click(object sender, EventArgs e)
        {
            try
            {
                var lst = File.ReadAllLines("lastOpenImgFilename.txt")[0];
                foreach (ListViewItem item in this.listViewLoad.Items)
                {
                    item.Selected = item.ImageKey == lst;
                }
                this.listViewLoad_DoubleClick(null, null);
            }
            catch { }
        }




    }
    struct processItem
    {
        public string Name;
        public override string ToString()
        {
            return this.Name;
        }
        public string Info;
        public string Type;
    }
    /// <summary>
    /// 描述一个输出参数
    /// </summary>
    class processOutputItem : ProcessIOItem
    {
        public processOutputItem(string type, string name)
            : base(type, name)
        {
        }
    }
    /// <summary>
    /// 描述一个输入参数
    /// </summary>
    public class ProcessInputItem : ProcessIOItem
    {
        public ProcessInputItem(string type, string name)
            : base(type, name)
        {
        }
    }
    public class ProcessIOItem
    {
        /// <summary>
        /// 参数类型
        /// </summary>
        public string Type;

        /// <summary>
        /// 参数名称
        /// </summary>
        public string Name;
        public ProcessIOItem(string type, string name)
        {
            this.Type = type;
            this.Name = name;
        }
    }
    public class LocalDataList : List<string>
    {
        /// <summary>
        /// 增加（覆盖）一项新的本地数据
        /// </summary>
        /// <param name="item">The item.</param>
        public new void Add(string item)
        {
            if (!this.Contains(item))
            {
                base.Add(item);
            }
        }
        public new void Remove(string item)
        {
            if (this.Contains(item))
            {
                base.Remove(item);
            }
        }
    }
}