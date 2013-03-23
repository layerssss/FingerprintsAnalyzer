namespace FRADataStructs.DataStructs.Controls
{
    partial class FormMacro
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.编译ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.运行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.单个图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.所有图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.原图像ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除已编译的缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除整个日志ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注释选定的语句ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.取消注释ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.部署ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.追加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清除部署缓存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.清除整个日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.结束宿主进程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成结果配置代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.process1 = new System.Diagnostics.Process();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.浏览目录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编译ToolStripMenuItem,
            this.运行ToolStripMenuItem,
            this.清除已编译的缓存ToolStripMenuItem,
            this.清除整个日志ToolStripMenuItem1,
            this.代码ToolStripMenuItem,
            this.部署ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(606, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // 编译ToolStripMenuItem
            // 
            this.编译ToolStripMenuItem.Name = "编译ToolStripMenuItem";
            this.编译ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编译ToolStripMenuItem.Text = "编译";
            this.编译ToolStripMenuItem.Click += new System.EventHandler(this.编译ToolStripMenuItem_Click);
            // 
            // 运行ToolStripMenuItem
            // 
            this.运行ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.单个图像ToolStripMenuItem,
            this.所有图像ToolStripMenuItem,
            this.原图像ToolStripMenuItem});
            this.运行ToolStripMenuItem.Name = "运行ToolStripMenuItem";
            this.运行ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.运行ToolStripMenuItem.Text = "运行";
            this.运行ToolStripMenuItem.Click += new System.EventHandler(this.运行ToolStripMenuItem_Click);
            // 
            // 单个图像ToolStripMenuItem
            // 
            this.单个图像ToolStripMenuItem.Name = "单个图像ToolStripMenuItem";
            this.单个图像ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.单个图像ToolStripMenuItem.Text = "选择文件";
            this.单个图像ToolStripMenuItem.Click += new System.EventHandler(this.单个图像ToolStripMenuItem_Click);
            // 
            // 所有图像ToolStripMenuItem
            // 
            this.所有图像ToolStripMenuItem.Name = "所有图像ToolStripMenuItem";
            this.所有图像ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.所有图像ToolStripMenuItem.Text = "选择目录";
            this.所有图像ToolStripMenuItem.Click += new System.EventHandler(this.所有图像ToolStripMenuItem_Click);
            // 
            // 原图像ToolStripMenuItem
            // 
            this.原图像ToolStripMenuItem.Name = "原图像ToolStripMenuItem";
            this.原图像ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.原图像ToolStripMenuItem.Text = "原图像";
            this.原图像ToolStripMenuItem.Click += new System.EventHandler(this.原图像ToolStripMenuItem_Click);
            // 
            // 清除已编译的缓存ToolStripMenuItem
            // 
            this.清除已编译的缓存ToolStripMenuItem.Name = "清除已编译的缓存ToolStripMenuItem";
            this.清除已编译的缓存ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.清除已编译的缓存ToolStripMenuItem.Text = "清除编译缓存";
            this.清除已编译的缓存ToolStripMenuItem.Click += new System.EventHandler(this.清除已编译的缓存ToolStripMenuItem_Click);
            // 
            // 清除整个日志ToolStripMenuItem1
            // 
            this.清除整个日志ToolStripMenuItem1.Name = "清除整个日志ToolStripMenuItem1";
            this.清除整个日志ToolStripMenuItem1.Size = new System.Drawing.Size(92, 21);
            this.清除整个日志ToolStripMenuItem1.Text = "清除整个日志";
            this.清除整个日志ToolStripMenuItem1.Click += new System.EventHandler(this.清除整个日志ToolStripMenuItem1_Click);
            // 
            // 代码ToolStripMenuItem
            // 
            this.代码ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.注释选定的语句ToolStripMenuItem,
            this.取消注释ToolStripMenuItem});
            this.代码ToolStripMenuItem.Name = "代码ToolStripMenuItem";
            this.代码ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.代码ToolStripMenuItem.Text = "代码";
            // 
            // 注释选定的语句ToolStripMenuItem
            // 
            this.注释选定的语句ToolStripMenuItem.Name = "注释选定的语句ToolStripMenuItem";
            this.注释选定的语句ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.注释选定的语句ToolStripMenuItem.Text = "注释选定的语句";
            this.注释选定的语句ToolStripMenuItem.Click += new System.EventHandler(this.注释选定的语句ToolStripMenuItem_Click);
            // 
            // 取消注释ToolStripMenuItem
            // 
            this.取消注释ToolStripMenuItem.Name = "取消注释ToolStripMenuItem";
            this.取消注释ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.取消注释ToolStripMenuItem.Text = "取消注释";
            this.取消注释ToolStripMenuItem.Click += new System.EventHandler(this.取消注释ToolStripMenuItem_Click);
            // 
            // 部署ToolStripMenuItem
            // 
            this.部署ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.追加ToolStripMenuItem,
            this.清除部署缓存ToolStripMenuItem,
            this.浏览目录ToolStripMenuItem});
            this.部署ToolStripMenuItem.Name = "部署ToolStripMenuItem";
            this.部署ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.部署ToolStripMenuItem.Text = "部署";
            // 
            // 追加ToolStripMenuItem
            // 
            this.追加ToolStripMenuItem.Name = "追加ToolStripMenuItem";
            this.追加ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.追加ToolStripMenuItem.Text = "追加";
            this.追加ToolStripMenuItem.Click += new System.EventHandler(this.追加ToolStripMenuItem_Click);
            // 
            // 清除部署缓存ToolStripMenuItem
            // 
            this.清除部署缓存ToolStripMenuItem.Name = "清除部署缓存ToolStripMenuItem";
            this.清除部署缓存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.清除部署缓存ToolStripMenuItem.Text = "清除部署缓存";
            this.清除部署缓存ToolStripMenuItem.Click += new System.EventHandler(this.清除部署缓存ToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.AcceptsTab = true;
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.HideSelection = false;
            this.textBox1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(311, 313);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.textBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.textBox2);
            this.splitContainer1.Size = new System.Drawing.Size(606, 313);
            this.splitContainer1.SplitterDistance = 311;
            this.splitContainer1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.ContextMenuStrip = this.contextMenuStrip1;
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.HideSelection = false;
            this.textBox2.ImeMode = System.Windows.Forms.ImeMode.On;
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(291, 313);
            this.textBox2.TabIndex = 0;
            this.textBox2.DoubleClick += new System.EventHandler(this.转到该错误所在行ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除整个日志ToolStripMenuItem,
            this.结束宿主进程ToolStripMenuItem,
            this.生成结果配置代码ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(173, 70);
            // 
            // 清除整个日志ToolStripMenuItem
            // 
            this.清除整个日志ToolStripMenuItem.Name = "清除整个日志ToolStripMenuItem";
            this.清除整个日志ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.清除整个日志ToolStripMenuItem.Text = "清除整个日志";
            this.清除整个日志ToolStripMenuItem.Click += new System.EventHandler(this.清除整个日志ToolStripMenuItem_Click);
            // 
            // 结束宿主进程ToolStripMenuItem
            // 
            this.结束宿主进程ToolStripMenuItem.Name = "结束宿主进程ToolStripMenuItem";
            this.结束宿主进程ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.结束宿主进程ToolStripMenuItem.Text = "结束宿主进程";
            this.结束宿主进程ToolStripMenuItem.Click += new System.EventHandler(this.结束宿主进程ToolStripMenuItem_Click);
            // 
            // 生成结果配置代码ToolStripMenuItem
            // 
            this.生成结果配置代码ToolStripMenuItem.Name = "生成结果配置代码ToolStripMenuItem";
            this.生成结果配置代码ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.生成结果配置代码ToolStripMenuItem.Text = "生成结果配置代码";
            this.生成结果配置代码ToolStripMenuItem.Click += new System.EventHandler(this.生成结果配置代码ToolStripMenuItem_Click);
            // 
            // process1
            // 
            this.process1.EnableRaisingEvents = true;
            this.process1.StartInfo.CreateNoWindow = true;
            this.process1.StartInfo.Domain = "";
            this.process1.StartInfo.LoadUserProfile = false;
            this.process1.StartInfo.Password = null;
            this.process1.StartInfo.RedirectStandardOutput = true;
            this.process1.StartInfo.StandardErrorEncoding = null;
            this.process1.StartInfo.StandardOutputEncoding = null;
            this.process1.StartInfo.UserName = "";
            this.process1.StartInfo.UseShellExecute = false;
            this.process1.SynchronizingObject = this;
            this.process1.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(this.process1_OutputDataReceived);
            this.process1.Exited += new System.EventHandler(this.process1_Exited);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "请选择包含原图像文件的目录";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "系统支持的位图图像文件|*.*";
            this.openFileDialog1.Multiselect = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "保存结果";
            // 
            // 浏览目录ToolStripMenuItem
            // 
            this.浏览目录ToolStripMenuItem.Name = "浏览目录ToolStripMenuItem";
            this.浏览目录ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.浏览目录ToolStripMenuItem.Text = "浏览目录";
            this.浏览目录ToolStripMenuItem.Click += new System.EventHandler(this.浏览目录ToolStripMenuItem_Click);
            // 
            // FormMacro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 338);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormMacro";
            this.Text = "FormMacro";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMacro_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Diagnostics.Process process1;
        private System.Windows.Forms.ToolStripMenuItem 运行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 单个图像ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 所有图像ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 编译ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 清除整个日志ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 原图像ToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem 清除已编译的缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除整个日志ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 结束宿主进程ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成结果配置代码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 代码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注释选定的语句ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 取消注释ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 部署ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 追加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清除部署缓存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 浏览目录ToolStripMenuItem;

    }
}