using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MainGui
{
    public partial class Taskbar : Form
    {
        public Taskbar()
        {
            InitializeComponent();
        }

        private void Taskbar_Load(object sender, EventArgs e)
        {
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.flowLayoutPanel1.Width = this.flowLayoutPanel2.Width = this.Width;
            //this.timer1.Enabled = false;
            fold();
        }
        void fold()
        {
            targetOffset = 0;
        }
        void unfold()
        {
            targetOffset = this.Height-20;
            this.Activate();
        }
        int targetOffset = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Top = (this.Top*6+(this.targetOffset-(this.Height - 20)))/7;
            foreach (var form in (this.Owner as FormMain).OpenForms)
            {
                if (form.IsDisposed||form==this)
                {
                    continue;
                }
                bool found = false;
                Button foundBtn = null;
                foreach (Button b in this.flowLayoutPanel1.Controls)
                {
                    if (b.Tag == form)
                    {
                        foundBtn = b;
                        found = true;
                    }
                }
                foreach (Button b in this.flowLayoutPanel2.Controls)
                {
                    if (b.Tag == form)
                    {
                        foundBtn = b;
                        found = true;
                    }
                }
                Button formBtn;
                if (found)
                {
                    formBtn = foundBtn;
                }
                else
                {
                    formBtn = new Button() { Tag = form, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink, Image = form.Icon.ToBitmap(), TextImageRelation = TextImageRelation.ImageBeforeText };
                    formBtn.Click += new EventHandler(formBtn_Click);
                    if (form is FormProcess)
                    {
                        this.flowLayoutPanel1.Controls.Add(formBtn);
                    }
                    if (form is FormDataList)
                    {
                        this.flowLayoutPanel2.Controls.Add(formBtn);
                    }
                }
                var formBtnText = form.Text;
                if (formBtnText.StartsWith("处理->"))
                {
                    formBtnText = formBtnText.Substring(4);
                } 
                if (formBtnText.StartsWith("数据->"))
                {
                    formBtnText = formBtnText.Substring(4);
                }
                formBtn.Text = formBtnText;
            }
            foreach (Button b in this.flowLayoutPanel1.Controls)
            {
                if ((b.Tag as Form).IsDisposed)
                {
                    this.flowLayoutPanel1.Controls.Remove(b);
                    break;
                }
            }
            foreach (Button b in this.flowLayoutPanel2.Controls)
            {
                if ((b.Tag as Form).IsDisposed) {
                    this.flowLayoutPanel2.Controls.Remove(b);
                    break;
                }
            }
            var p = new Point(Control.MousePosition.X,Control.MousePosition.Y);
            p.Offset(-Screen.PrimaryScreen.Bounds.Location.X,+20);
            if (!this.Bounds.Contains(p))
            {
                fold();
            }
        }

        void formBtn_Click(object sender, EventArgs e)
        {
            ((sender as Button).Tag as Form).Activate();
        }

        private void Taskbar_MouseEnter(object sender, EventArgs e)
        {
            this.unfold();
        }

    }
}
