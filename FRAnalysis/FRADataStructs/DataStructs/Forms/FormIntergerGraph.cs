using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FRADataStructs.DataStructs.Controls
{
    public partial class FormIntergerGraph : Form
    {
        public FormIntergerGraph()
        {
            InitializeComponent();
            this.comboBox1.Items.Add(new itemColorSet()
            {
                Name = "选择配色显示方案",
                Colors = new Dictionary<int, Color>()
            });
            itemColorSet ics;
            ics = new itemColorSet()
            {
                Name = "选择配色显示方案",
                Colors = new Dictionary<int, Color>()
            };
            ics.Colors.Add(1, Color.White);
            ics.Colors.Add(2, Color.Yellow);
            ics.Colors.Add(3, Color.Blue);
            ics.Colors.Add(4, Color.Red);
            this.comboBox1.Items.Add(ics);
        }
        public virtual void LoadData(IntergerGraph pi, GrayLevelImage img, string dataIdent)
        {
            this.pi = pi;
            this.img = img;
            this.Text = "整数图-" + dataIdent;
            pictureBox1.Image = bitmap = new Bitmap(pi.Width, pi.Height);
            if (img == null)
            {
                checkBox1.Checked = false;
                checkBox1.Enabled = false;
            }
            checkBox_CheckedChanged(null, null);
        }
        IntergerGraph pi;
        GrayLevelImage img;
        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                img.DrawImage(bitmap);
            }
            else
            {
                bitmap = new Bitmap(pi.Width, pi.Height);
            }
            for (int i = 0; i < pi.Height; i++)
            {
                for (int j = 0; j < pi.Width; j++)
                {
                    foreach (CheckBox c in flowLayoutPanel1.Controls)
                    {
                        if (!c.Checked)
                        {
                            continue;
                        }
                        if (pi.Value[i][j] == (int)c.Tag)
                        {
                            bitmap.SetPixel(j, i, c.BackColor);
                            break;
                        }
                    }
                }
            }
            pictureBox1.Image = null;
            pictureBox1.Image = bitmap;
        }
        Bitmap bitmap;
        public void AddCheckBox(int value, Color color)
        {
            CheckBox c = new CheckBox();
            c.Tag = value;
            c.Text = value.ToString();
            c.ForeColor = Color.Red;
            c.Font = new System.Drawing.Font(c.Font, FontStyle.Bold);
            c.BackColor = color;
            c.Checked = true;
            c.CheckedChanged += new EventHandler(checkBox_CheckedChanged);
            flowLayoutPanel1.Controls.Add(c);
            flowLayoutPanel1.ResumeLayout();
            checkBox_CheckedChanged(null, null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.AddCheckBox((int)numericUpDown1.Value, colorDialog1.Color);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.Clear();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_CheckedChanged(null, null);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null) { return; }
            itemColorSet cSet = (itemColorSet)comboBox1.SelectedItem;
            if (cSet.Colors.Count != 0)
            {
                button2_Click(null, null);
                foreach (int i in cSet.Colors.Keys)
                {
                    AddCheckBox(i, cSet.Colors[i]);
                }
            }
        }
        class itemColorSet
        {
            public string Name;
            public Dictionary<int, Color> Colors;
            public override string ToString()
            {
                return Name + "(" + Colors.Keys.Count + ")";
            }
        }
    }
}
