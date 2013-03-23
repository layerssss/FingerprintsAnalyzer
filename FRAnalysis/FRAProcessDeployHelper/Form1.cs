using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FRAProcessDeployHelper
{
    public partial class Form1 : Form
    {
        string outputDir;
        string processName;
        string deployDir;
        public Form1(string outputDir,string processName,string deployDir)
        {
            InitializeComponent();
            this.outputDir = outputDir;
            this.processName = processName;
            this.deployDir = deployDir;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] filenames = Directory.GetFiles(outputDir);
            if (!Directory.Exists(deployDir + "process\\" + textBox3.Text))
            {
                Directory.CreateDirectory(deployDir + "process\\" + textBox3.Text);
            }
            File.WriteAllText(outputDir + "DeployedType.txt", textBox3.Text);
            File.WriteAllText(deployDir+"process\\"+textBox3.Text+"\\FRAProcess_"+processName+".version",File.GetLastWriteTime(outputDir+"FRAProcess_"+processName+".exe").ToString());
            foreach(string filename in filenames)//复制
            {
                FileInfo fi = new FileInfo(filename);
                if (fi.Name == "DeployedType.txt")
                {
                    continue;
                }
                fi.CopyTo(deployDir + "process\\" + textBox3.Text + "\\" + fi.Name, true);
            }
            File.WriteAllText(deployDir + "process\\" + textBox3.Text + "\\FRAProcess_" + this.processName + ".info", textBox1.Text);
            this.DialogResult = DialogResult.OK;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.Text = deployDir;
            this.Text += ":"+processName;
            try
            {
                string deployedType = Program.GetSolutionName((new DirectoryInfo(this.outputDir)).Parent.Parent.Parent.FullName);
                listBox1.Items.Add(deployedType);
                listBox1.SelectedIndex = 0;
                textBox1.Text = File.ReadAllText(deployDir + "process\\" + deployedType + "\\FRAProcess_" + processName + ".info");
            }
            catch {
                try
                {
                    textBox1.Text = File.ReadAllText(deployDir + "process\\" +
                        File.ReadAllLines(outputDir + "DeployedType.txt")[0]
                        + "\\FRAProcess_" + processName + ".info");
                }
                catch { }
            }


            string[] types=Directory.GetDirectories(deployDir+"process");
            foreach (string type in types)
            {
                string tstring = type.Substring(type.LastIndexOf('\\') + 1);
                if (!listBox1.Items.Contains(tstring))
                {
                    listBox1.Items.Add(tstring);
                }
            }
            if (textBox1.Text != "" && textBox3.Text != "")
            {
                button2_Click(null, null);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox3.Text = (string)listBox1.SelectedItem;
        }
    }
}
