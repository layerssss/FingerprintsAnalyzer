using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tool_BinaryFileStream
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        System.IO.BinaryWriter w;
        System.IO.BinaryReader r;
        void refreshW()
        {
            toolStripStatusLabel1.Text = "BinaryWriter:";
            if (w != null)
            {
                toolStripStatusLabel1.Text += "Open";
                toolStripStatusLabel2.Text = "CurPos:" + w.BaseStream.Position.ToString();
                flowLayoutPanel1.Enabled = true;
            }
            else
            {

                toolStripStatusLabel1.Text += "Closed";
                toolStripStatusLabel2.Text = "CurPos:NA";
                flowLayoutPanel1.Enabled = false;
            }
        }
        void refreshR()
        {
            toolStripStatusLabel3.Text = "BinaryReader:";
            if (r != null)
            {
                toolStripStatusLabel3.Text += "Open";
                toolStripStatusLabel4.Text = "CurPos:" + r.BaseStream.Position.ToString();
                toolStripStatusLabel5.Text = "Len:" + r.BaseStream.Length.ToString();
                flowLayoutPanel2.Enabled = true;
            }
            else
            {

                toolStripStatusLabel3.Text += "Closed";
                toolStripStatusLabel4.Text = "CurPos:NA";
                toolStripStatusLabel5.Text = "Len:NA";
                flowLayoutPanel2.Enabled = false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(byte.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadByte().ToString();
                refreshR();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(short.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadInt16().ToString();
                refreshR();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(int.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadInt32().ToString();
                refreshR();
            }
        }
        public void LoadFile(string filename)
        {
            this.r = new System.IO.BinaryReader(System.IO.File.OpenRead(filename));
            refreshR();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(long.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadInt64().ToString();
                refreshR();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(bool.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadBoolean().ToString();
                refreshR();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(float.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadSingle().ToString();
                refreshR();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(double.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadDouble().ToString();
                refreshR();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(char.Parse(textBox1.Text));
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadChar().ToString();
                refreshR();
            }
        }

        private void binaryWriterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (w == null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    w = new System.IO.BinaryWriter(System.IO.File.Create(saveFileDialog1.FileName));
                }
            }
            else
            {
                w.Flush();
                w.Close();
                w = null;
            }
            refreshW();
        }


        private void binaryReaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (r == null)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    r = new System.IO.BinaryReader(System.IO.File.OpenRead(openFileDialog1.FileName));
                }
            }
            else
            {
                r.Close();
                r = null;
            }
            refreshR();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            refreshW();
            refreshR();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (w != null)
            {
                w.Close();
                w = null;
            }
            if (r != null)
            {
                r.Close();
                r = null;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Controls.Contains((Control)sender))
            {
                w.Write(textBox1.Text);
                refreshW();
            }
            else
            {
                textBox2.Text = r.ReadString();
                refreshR();
            }
        }
    }
}
