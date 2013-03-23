using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace FRADataStructs.DataStructs.Controls
{
    public partial class FormLocalStructure : Form
    {
        public FormLocalStructure()
        {
            InitializeComponent();
        }
        public void LoadData(LocalStructure data, GrayLevelImage originalImg, string dataIdent)
        {
            this.data = data;
            this.originalImg = originalImg;
            this.Text = "局部结构-" + dataIdent;
            this.refresh();
        }
        LocalStructure data;
        GrayLevelImage originalImg;
        bool changed = false;
        private void FormLocalStructure_Load(object sender, EventArgs e)
        {
            
        }
        void refresh()
        {
            DataStructTypes type=new DataStructTypes();
            this.listView1.Items.Clear();
            foreach (var kvp in this.data)
            {
                this.listView1.Items.Add(new ListViewItem(){
                    Tag=kvp.Key,
                    Text = kvp.Key + "." + type.GetName(kvp.Value) + (kvp.Key==this.data.PrimaryDataKey ? "(主数据)" : "")
                });
            }
            if (this.originalImg != null&&this.data.PrimaryData!=null)
            {
                this.pictureBox1.Image = new Bitmap(this.originalImg.Width, this.originalImg.Height);
                this.originalImg.DrawImage(this.pictureBox1.Image as Bitmap);
                try
                {
                    this.data.Draw(this.pictureBox1.Image,this.Font);
                }
                catch (Exception ex)
                {
                    this.toolStripStatusLabel1.Text = "主数据[" + this.data.PrimaryDataKey + "]无法显示:" + ex.Message;
                }

            }
        }
        private void FormLocalStructure_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (changed)
            {
                switch (MessageBox.Show("是否保存更改？", "数据已被修改", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                {
                    case System.Windows.Forms.DialogResult.Yes:
                        this.DialogResult = DialogResult.OK;
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    case System.Windows.Forms.DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }
            }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var key = listView1.SelectedItems[0].Tag as string;
                try
                {
                    if (this.data[key].Present(this.originalImg, "[" + this.Text + "]中的：" + key))
                    {
                        this.changed = true;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("该数据不支持这样的显示方式，或者该数据的显示需要载入原图像:\r\n"+ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                this.data.Remove(item.Tag as string);
                this.changed = true;
            }
            this.refresh();
        }

        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var types=new DataStructTypes();
            if (listView1.SelectedItems.Count == 1)
            {
                var key = listView1.SelectedItems[0].Tag as string;
                string newkey;
                if (Dialogs.Prompt.Show("重命名" + key + "." + types.GetName(this.data[key]), "新名称:", key, out newkey) == DialogResult.OK&&newkey!=key)
                {
                    if (this.data.ContainsKey(newkey))
                    {
                        MessageBox.Show("已经存在了名为"+newkey+"的数据。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var data = this.data[key];
                        this.data.Remove(key);
                        this.data.Add(newkey, data);
                        this.changed = true;
                        this.refresh();
                    }
                }
                
            }
        }

        private void 设置为主数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var key = listView1.SelectedItems[0].Tag as string;
                this.data.PrimaryDataKey = key;
                this.changed = true;
                this.refresh();
            }
        }

        private void 取消主数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.data.PrimaryDataKey = "";
            this.changed = true;
            this.refresh();
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var types = new DataStructTypes();
            string[] files = new String[this.listView1.SelectedItems.Count];
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\data.局部结构");
            if (!di.Exists)
            {
                di.Create(); ;
            }
            for (int i = 0; i < files.Length; i++)
            {
                string path = di.FullName + "\\" + this.listView1.SelectedItems[i].Tag + "." + types.GetName(this.data[(string)this.listView1.SelectedItems[i].Tag]);
                BinaryWriter writer = new BinaryWriter(File.Create(path));
                this.data[(string)this.listView1.SelectedItems[i].Tag].Serialize(writer);
                writer.Close();
                files[i] = path;
            }
            DataObject data = new DataObject(DataFormats.FileDrop, files);
            DoDragDrop(data, DragDropEffects.Copy);
            this.refresh();
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {

            var types = new DataStructTypes();
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            if (files.All(ts => types.ContainsKey(ts.ToLower().Substring(ts.LastIndexOf('.') + 1)) || ts.ToLower().Substring(ts.LastIndexOf('.') + 1) == "tif" || ts.ToLower().Substring(ts.LastIndexOf('.') + 1)=="bmp"))
            {
                e.Effect = DragDropEffects.Copy;
                return;
            }
            e.Effect = DragDropEffects.None;
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            var types = new DataStructTypes();
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                var key = fi.Name.Remove(fi.Name.LastIndexOf('.'));
                var type = fi.Name.Substring(fi.Name.LastIndexOf('.') + 1).ToLower();
                IDataStruct data;
                try
                {
                    BinaryReader reader = new BinaryReader(fi.OpenRead());
                    data = types.GetNew(type);
                    data.Deserialize(reader);
                    reader.Close();
                }
                catch
                {
                    var img = new Bitmap(Image.FromFile(fi.FullName));
                    data = GrayLevelImage.FromBitmap(img);
                    img.Dispose();
                }
                if (this.data.ContainsKey(key))
                {
                    this.data.Remove(key);
                }
                this.data.Add(key, data);
                this.changed = true;
            }
            this.refresh();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1&&this.originalImg!=null)
            {
                var newimg = new Bitmap(this.originalImg.Width, this.originalImg.Height);
                this.originalImg.DrawImage(newimg);
                var key = listView1.SelectedItems[0].Tag as string;
                try
                {
                    DataStructTypes.TryDraw(this.data[key], newimg, Color.Red,this.Font);
                    pictureBox1.Image = newimg;
                } 
                catch (Exception ex)
                {
                    this.toolStripStatusLabel1.Text = "数据[" + key + "]无法显示:" + ex.Message;
                }
            }
        }
    }
}
