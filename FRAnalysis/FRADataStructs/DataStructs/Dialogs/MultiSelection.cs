using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FRADataStructs.DataStructs.Dialogs
{
    public partial class MultiSelection : Form
    {
        public MultiSelection(string title,string text,List<string> items,List<string> selected)
        {
            InitializeComponent();
            this.Text = title;
            this.label1.Text = text;
            foreach (var item in items)
            {
                listBox1.Items.Add(item);
            }
            foreach (var item in selected)
            {
                listBox1.SelectedItems.Add(item);
            }

        }

        private void MultiSelectionClipboard_Load(object sender, EventArgs e)
        {
            
        }
        public bool IsMultiSelection
        {
            get
            {

                return this.listBox1.SelectionMode != SelectionMode.One;
            }
            set
            {
                if (!value && this.IsMultiSelection)
                {
                    this.listBox1.DoubleClick += this.button2_Click;
                }
                if (value && !this.IsMultiSelection)
                {
                    this.listBox1.DoubleClick -= button2_Click;
                }
                this.listBox1.SelectionMode = value ? SelectionMode.MultiSimple : SelectionMode.One;
            }
        }
        public string Result
        {
            get
            {
                var str = "";
                foreach (var item in listBox1.SelectedItems)
                {
                    str += item + "\r\n";
                }
                return str;
            }
        }
        public List<string> Items
        {
            get
            {
                var l=new List<string>();
                foreach (string str in this.listBox1.Items)
                {
                    l.Add(str);
                }
                return l;
            }
        }
        public List<string> SelectedItems
        {
            get
            {
                var l = new List<string>();
                foreach (string str in this.listBox1.SelectedItems)
                {
                    l.Add(str);
                }
                return l;
            }
        } 
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
