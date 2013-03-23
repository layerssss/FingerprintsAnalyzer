using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace FRADataStructs.DataStructs.Dialogs
{
    public partial class Progress : Form
    {
        public Progress()
        {
            InitializeComponent();
        }

        private void Progress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!closing)
            {
                e.Cancel = true;
            }
        }

        private void Progress_Load(object sender, EventArgs e)
        {
        }
        long total;
        long elapsed = 0;
        public static Progress Show(string text,string title,long size)
        {
            var p = new Progress();
            p.Text = title;
            p.label1.Text = text;
            p.total = size;
            (new Thread(p.ThreadStarting)).Start();
            return p;
        }
        void ThreadStarting()
        {
            this.timer1.Start();
            this.ShowDialog();
        }
        public void Elapse(long elapsed)
        {
            lock (this)
            {
                this.elapsed = elapsed;
            }
        }
        bool closing = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            lock (this)
            {
                progressBar1.Value = (int)(elapsed * 100 / total);
                label2.Text = elapsed + "/" + total+"("+progressBar1.Value + "%)";
                if (elapsed == total)
                {
                    this.closing = true;
                    this.Close();
                }
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
