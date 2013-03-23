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
    public partial class FormDataList : Form
    {
        public FormDataList(string type)
        {
            InitializeComponent();
            this.Type = type;
        }
        public string Type;
        private void FormDataList_Load(object sender, EventArgs e)
        {
            this.Text = "数据->" + this.Type;
            this.RefreshList();
        }
        public void RefreshList()
        {
            try
            {
                listViewData.Items.Clear();
                string[] filenames = Directory.GetFiles("data", "*." + (Type));
                foreach (string filename in filenames)
                {
                    FileInfo fi = new FileInfo(filename);
                    ListViewItem item = new ListViewItem()
                    {
                        Text = fi.Name,
                        Tag = fi
                    };
                    if (!(this.Owner as FormMain).ListLocalData.Contains(fi.Name))
                    {
                        item.Text = "@" + item.Text;
                    }
                    listViewData.Items.Add(item);
                }
            }
            catch { }
        }

        private void toolMenuRefresh_Click(object sender, EventArgs e)
        {
            this.RefreshList();
        }

        private void toolMenuCopy_Click(object sender, EventArgs e)
        {
            var fi = listViewData.SelectedItems[0].Tag as FileInfo;
            string oldName=fi.Name.Remove(fi.Name.LastIndexOf('.'));
            string res;
            if (FRADataStructs.DataStructs.Dialogs.Prompt.Show("复制 " + fi.Name, "新名称", oldName, out res) == DialogResult.OK && res != oldName)
            {
                fi.CopyTo(fi.Directory.FullName + "\\" + res + "." + this.Type, true);
                this.RefreshList();
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            this.toolMenuCopy.Enabled
                = this.toolMenuRename.Enabled
                = (listViewData.SelectedItems.Count == 1);
            this.toolMenuSetGlobal.Enabled
                = this.toolMenuSetLocal.Enabled
                = this.delToolStripMenuItem.Enabled
                = (listViewData.SelectedItems.Count != 0);
        }

        private void toolMenuRename_Click(object sender, EventArgs e)
        {
            var fi = listViewData.SelectedItems[0].Tag as FileInfo;
            string oldName = fi.Name.Remove(fi.Name.LastIndexOf('.'));
            string res;
            if (FRADataStructs.DataStructs.Dialogs.Prompt.Show("重命名 " + fi.Name, "新名称", oldName, out res) == DialogResult.OK && res != oldName)
            {
                fi.MoveTo(fi.Directory.FullName + "\\" + res + "." + this.Type);
                this.RefreshList();
            }
        }

        private void toolMenuSetLocal_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewData.SelectedItems)
            {
                var fi = lvi.Tag as FileInfo;
                (this.Owner as FormMain).ListLocalData.Add(fi.Name);
                this.RefreshList();
            }
        }

        private void toolMenuSetGlobal_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewData.SelectedItems)
            {
                var fi = lvi.Tag as FileInfo;
                (this.Owner as FormMain).ListLocalData.Remove(fi.Name);
                this.RefreshList();
            }
        }

        private void listViewData_DoubleClick(object sender, EventArgs e)
        {
            if (listViewData.SelectedItems.Count == 1)
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                {
                    FileName = Application.ExecutablePath,
                    Arguments = "\"" + ((FileInfo)listViewData.SelectedItems[0].Tag).FullName + "\"",
                    WorkingDirectory = Application.StartupPath + "\\data"
                });
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dataStructTypes=(this.Owner as FormMain).DataStructTypes;
            var ListLocalData = (this.Owner as FormMain).ListLocalData;
            string res;
            if (FRADataStructs.DataStructs.Dialogs.Prompt.Show("创建一个 " + this.Type, "新名称：", "", out res) != DialogResult.OK)
            {
                return;
            }
            try
            {
                IDataStruct emptydata = dataStructTypes.GetNew(this.Type);
                IDataStruct data = emptydata.BuildInstance();
                BinaryWriter writer = new BinaryWriter(File.Create("data\\" + res + "." + this.Type));
                data.Serialize(writer);
                writer.Close();
                ListLocalData.Remove(res + "." + this.Type);
                this.RefreshList();
            }
            catch (NotImplementedException)
            {
                MessageBox.Show("数据类型“" + this.Type + "”不支持这种创建方式。", "无法创建", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("创建时发生错误：" + ex.Message + "。", "创建数据失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in listViewData.SelectedItems)
            {
                var fi = lvi.Tag as FileInfo;
                (this.Owner as FormMain).ListLocalData.Remove(fi.Name);
                fi.Delete();
                this.RefreshList();
            }
        }

        private void listViewData_ItemDrag(object sender, ItemDragEventArgs e)
        {
            string[] files = new String[this.listViewData.SelectedItems.Count];
            for (int i = 0; i < files.Length; i++)
            {
                files[i] = (this.listViewData.SelectedItems[i].Tag as FileInfo).FullName;
            }
            DataObject data = new DataObject(DataFormats.FileDrop, files);
            DoDragDrop(data, DragDropEffects.Copy);
            this.RefreshList();
        }

        private void FormDataList_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.AllowedEffect.HasFlag(DragDropEffects.Copy))
            {
                return;
            }
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                if ((e.KeyState & 8) == 8)
                {
                    fi.CopyTo(Application.StartupPath + "\\data\\" + fi.Name.Remove(fi.Name.Length - this.Type.Length) + this.Type, true);
                }
                else
                {
                    fi.MoveTo(Application.StartupPath + "\\data\\" + fi.Name.Remove(fi.Name.Length - this.Type.Length) + this.Type);
                }
                (this.Owner as FormMain).ListLocalData.Remove(fi.Name.Remove(fi.Name.Length - this.Type.Length) + this.Type);
            }
            this.RefreshList();
        }

        private void FormDataList_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            if(files.All(ts=>this.Type==ts.ToLower().Substring(ts.LastIndexOf('.')+1))){

                e.Effect = ((e.KeyState & 8) == 8) ? DragDropEffects.Copy : DragDropEffects.Move;
                return;
            }
            e.Effect = DragDropEffects.None;
        }
    }
}
