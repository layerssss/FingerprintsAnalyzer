using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace MainGui
{
    public partial class FormPreInputs : Form
    {
        public FormPreInputs(StreamReader preinputs)
        {
            InitializeComponent();
            this.sr = preinputs;
        }
        StreamReader sr;
        List<ComboBox> listPreinputs = new List<ComboBox>();
        private void FormPreInputs_Load(object sender, EventArgs e)
        {
            while (!sr.EndOfStream)
            {
                switch (sr.ReadLine())
                {
                    case "input":
                        ProcessInputItem input = new ProcessInputItem(sr.ReadLine(), sr.ReadLine());
                        Label l = new Label() { Text = input.Name + "(" + input.Type + "):", AutoSize = true };
                        ComboBox c = new ComboBox();
                        c.Dock = DockStyle.Fill;
                        this.tableLayoutPanel1.RowCount += 1;
                        this.tableLayoutPanel1.Controls.Add(l);
                        this.tableLayoutPanel1.Controls.Add(c);
                        this.tableLayoutPanel1.SetRow(l, this.tableLayoutPanel1.RowCount - 2);
                        this.tableLayoutPanel1.SetRow(c, this.tableLayoutPanel1.RowCount - 2);
                        this.tableLayoutPanel1.SetColumn(l, 0);
                        this.tableLayoutPanel1.SetColumn(c, 1);
                        string[] datas = Directory.GetFiles("data", "*." + input.Type);
                        foreach (string data in datas)
                        {
                            c.Items.Add(new FileInfo(data).Name);
                        }
                        c.Tag = input;
                        this.listPreinputs.Add(c);
                        break;
                    default:
                        throw (new Exception());
                }
            }
        }

        private void FormPreInputs_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
        public List<KeyValuePair<ProcessInputItem, string>> PreInputs = new List<KeyValuePair<ProcessInputItem, string>>();
        private void FormPreInputs_FormClosing(object sender, FormClosingEventArgs e)
        {
            sr.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (ComboBox c in this.listPreinputs)
            {
                if(c.SelectedItem==null)
                {
                    MessageBox.Show(((ProcessInputItem)c.Tag).Name+"未选择");
                }
            }
            foreach (ComboBox c in this.listPreinputs)
            {
                this.PreInputs.Add(new KeyValuePair<ProcessInputItem, string>((ProcessInputItem)c.Tag, (string)c.SelectedItem));
            }
            DialogResult = DialogResult.OK;
        }
    }
}
