namespace FRADataStructs.DataStructs.Controls
{
    partial class FormStringData
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.选择一个文件路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择一个目录路径ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择一个文件路径ToolStripMenuItem,
            this.选择一个目录路径ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(426, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 选择一个文件路径ToolStripMenuItem
            // 
            this.选择一个文件路径ToolStripMenuItem.Name = "选择一个文件路径ToolStripMenuItem";
            this.选择一个文件路径ToolStripMenuItem.Size = new System.Drawing.Size(116, 21);
            this.选择一个文件路径ToolStripMenuItem.Text = "选择一个文件路径";
            this.选择一个文件路径ToolStripMenuItem.Click += new System.EventHandler(this.选择一个文件路径ToolStripMenuItem_Click);
            // 
            // 选择一个目录路径ToolStripMenuItem
            // 
            this.选择一个目录路径ToolStripMenuItem.Name = "选择一个目录路径ToolStripMenuItem";
            this.选择一个目录路径ToolStripMenuItem.Size = new System.Drawing.Size(116, 21);
            this.选择一个目录路径ToolStripMenuItem.Text = "选择一个目录路径";
            this.选择一个目录路径ToolStripMenuItem.Click += new System.EventHandler(this.选择一个目录路径ToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox1.Location = new System.Drawing.Point(0, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(426, 246);
            this.textBox1.TabIndex = 1;
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormStringData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 271);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormStringData";
            this.Text = "FormStringData";
            this.Load += new System.EventHandler(this.FormStringData_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 选择一个文件路径ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择一个目录路径ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}