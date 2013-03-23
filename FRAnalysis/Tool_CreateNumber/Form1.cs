using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Tool_CreateNumber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("data")) { Directory.CreateDirectory("data"); }
            int j = int.Parse(textBox1.Text);
            int k = int.Parse(textBox2.Text);
            FRADataStructs.DataStructs.Integer I = new FRADataStructs.DataStructs.Integer();
            for (int i = j; i <= k; i++)
            {
                BinaryWriter writer = new BinaryWriter(File.Create("data\\" + i + ".整数"));
                I.Value = i;
                I.Serialize(writer);
                writer.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("data")) { Directory.CreateDirectory("data"); }
            double j = double.Parse(textBox1.Text);
            double k = double.Parse(textBox2.Text);
            double delta=double.Parse(textBox3.Text);
            FRADataStructs.DataStructs.DoubleFloat I = new FRADataStructs.DataStructs.DoubleFloat();
            for (double i = j; i <= k; i+=delta)
            {
                BinaryWriter writer = new BinaryWriter(File.Create("data\\" + i + ".浮点数"));
                I.Value = i;
                I.Serialize(writer);
                writer.Close();
            }
        }
    }
}
