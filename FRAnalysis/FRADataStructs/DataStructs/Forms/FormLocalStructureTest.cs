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
    public partial class FormLocalStructureTest : Form
    {
        public FormLocalStructureTest()
        {
            InitializeComponent();
        }
        void refresh()
        {
            try
            {
                this.pictureBox1.Image = null;
                this.listView1.Items.Clear();
                this.pictureBox1.Image = new Bitmap(this.pair.Img[0].Width, this.pair.Img[0].Height);
                this.listView1.Items.Add("IMG", "原图像.灰度图", -1);
                this.pair.Img[0].DrawImage(this.pictureBox1.Image as Bitmap);
                this.pair.Data[0].Draw(this.pictureBox1.Image,this.Font);
                this.listView1.Items.Add("DATA", "局部结构.局部结构", -1);
            }
            catch { }
            try
            {
                this.pictureBox2.Image = null;
                this.listView2.Items.Clear();
                this.pictureBox2.Image = new Bitmap(this.pair.Img[1].Width, this.pair.Img[1].Height);
                this.listView2.Items.Add("IMG", "原图像.灰度图", -1);
                this.pair.Img[1].DrawImage(this.pictureBox2.Image as Bitmap);
                this.pair.Data[1].Draw(this.pictureBox2.Image,this.Font);
                this.listView2.Items.Add("DATA","局部结构.局部结构",-1);
            }
            catch { }
        }
        LocalStructureTest pair;
        public static void Show(string title, LocalStructureTest pair)
        {
            FormLocalStructureTest f = new FormLocalStructureTest();
            f.Text = title;
            f.pair = pair;
            f.ShowDialog();
        }

        private void LocalSturctureTest_Load(object sender, EventArgs e)
        {
            this.refresh();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\data.局部结构测试集");
            if (!di.Exists)
            {
                di.Create(); ;
            }
            var listview = sender as ListView;
            int i = this.tableLayoutPanel1.GetColumn(sender as Control);
            var lstr = new List<string>();
            if (listview.SelectedItems.ContainsKey("IMG"))
            {
                var writer = new BinaryWriter(File.Create(di.FullName + "\\原图像.灰度图"));
                this.pair.Img[i].Serialize(writer);
                writer.Close();
                lstr.Add(di.FullName + "\\原图像.灰度图");
            }
            if (listview.SelectedItems.ContainsKey("DATA"))
            {
                var writer = new BinaryWriter(File.Create(di.FullName + "\\局部结构.局部结构"));
                this.pair.Data[i].Serialize(writer);
                writer.Close();
                lstr.Add(di.FullName + "\\局部结构.局部结构");
            }
            DataObject data = new DataObject(DataFormats.FileDrop,lstr.ToArray());
            this.DoDragDrop(data, DragDropEffects.Copy);
            this.refresh();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var i = tableLayoutPanel1.GetColumn(contextMenuStrip1.SourceControl);
            var listview = contextMenuStrip1.SourceControl as ListView;
            if (listview.SelectedItems.ContainsKey("IMG"))
            {
                this.pair.Img[i] = null;
            }
            if (listview.SelectedItems.ContainsKey("DATA"))
            {
                this.pair.Data[i] = null;
            }
            this.refresh();
        }

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            var types = new DataStructTypes();
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            String[] files = e.Data.GetData(DataFormats.FileDrop, false) as String[];
            if (files.Length == 1)
            {
                if (files[0].Substring(files[0].LastIndexOf('.')+1) == "灰度图")
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
                if (files[0].Substring(files[0].LastIndexOf('.') + 1) == "局部结构")
                {
                    e.Effect = DragDropEffects.Copy;
                    return;
                }
            }
            e.Effect = DragDropEffects.None;
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            var listview = sender as ListView;
            int i = this.tableLayoutPanel1.GetColumn(sender as Control);
            var file = (e.Data.GetData(DataFormats.FileDrop, false) as String[])[0];
            if (file.Substring(file.LastIndexOf('.') + 1) == "灰度图")
            {
                this.pair.Img[i] = new GrayLevelImage();
                var reader = new BinaryReader(File.OpenRead(file));
                this.pair.Img[i].Deserialize(reader);
                reader.Close();
                this.refresh();
                return;
            }
            {
                this.pair.Data[i] = new LocalStructure();
                var reader = new BinaryReader(File.OpenRead(file));
                this.pair.Data[i].Deserialize(reader);
                reader.Close();
                this.refresh();
                return;
            }

        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            var listview = sender as ListView;
            int i = this.tableLayoutPanel1.GetColumn(sender as Control);
            if (listview.SelectedItems.Count == 1)
            {
                DirectoryInfo di = new DirectoryInfo(Application.StartupPath + "\\data.局部结构测试集");
                if (!di.Exists)
                {
                    di.Create(); ;
                }
                {
                    var writer = new BinaryWriter(File.Create(di.FullName + "\\原图像.灰度图"));
                    this.pair.Img[i].Serialize(writer);
                    writer.Close();
                }
                if (listview.SelectedItems.ContainsKey("DATA"))
                {
                    var writer = new BinaryWriter(File.Create(di.FullName + "\\局部结构.局部结构"));
                    this.pair.Data[i].Serialize(writer);
                    writer.Close();
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        WorkingDirectory=di.FullName,
                        FileName="..\\MainGui.exe",
                        Arguments = "局部结构.局部结构"
                    });
                }
                else
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        WorkingDirectory = di.FullName,
                        FileName = "..\\MainGui.exe",
                        Arguments = "原图像.灰度图"
                    });
                }
                return;
            }
        }
    }
}
