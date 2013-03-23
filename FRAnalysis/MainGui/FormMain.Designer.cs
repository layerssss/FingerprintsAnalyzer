namespace MainGui
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.pictureView = new System.Windows.Forms.PictureBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.pageLoad = new System.Windows.Forms.TabPage();
            this.splitLoad = new System.Windows.Forms.SplitContainer();
            this.listViewLoad = new System.Windows.Forms.ListView();
            this.imageListImg = new System.Windows.Forms.ImageList(this.components);
            this.flowLoadPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.butImgRefresh = new System.Windows.Forms.Button();
            this.butImgBrowse = new System.Windows.Forms.Button();
            this.pageData = new System.Windows.Forms.TabPage();
            this.listViewData = new System.Windows.Forms.ListView();
            this.contextMenuData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dataOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataSetIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageListTool = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.processOpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pageMacro = new System.Windows.Forms.TabPage();
            this.textMacro = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.butMacroNew = new System.Windows.Forms.Button();
            this.checkMacroRecording = new System.Windows.Forms.CheckBox();
            this.pageTool = new System.Windows.Forms.TabPage();
            this.listViewTool = new System.Windows.Forms.ListView();
            this.menuTool = new System.Windows.Forms.MenuStrip();
            this.menuItemToolBrowse = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserImg = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.notify = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timerTaskbar = new System.Windows.Forms.Timer(this.components);
            this.btnImgLast = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureView)).BeginInit();
            this.tabMain.SuspendLayout();
            this.pageLoad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitLoad)).BeginInit();
            this.splitLoad.Panel1.SuspendLayout();
            this.splitLoad.Panel2.SuspendLayout();
            this.splitLoad.SuspendLayout();
            this.flowLoadPanel.SuspendLayout();
            this.pageData.SuspendLayout();
            this.contextMenuData.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.pageMacro.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.pageTool.SuspendLayout();
            this.menuTool.SuspendLayout();
            this.contextMenuNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pictureView);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.tabMain);
            this.splitMain.Size = new System.Drawing.Size(713, 416);
            this.splitMain.SplitterDistance = 235;
            this.splitMain.TabIndex = 0;
            // 
            // pictureView
            // 
            this.pictureView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureView.Location = new System.Drawing.Point(0, 0);
            this.pictureView.Name = "pictureView";
            this.pictureView.Size = new System.Drawing.Size(235, 416);
            this.pictureView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureView.TabIndex = 0;
            this.pictureView.TabStop = false;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.pageLoad);
            this.tabMain.Controls.Add(this.pageData);
            this.tabMain.Controls.Add(this.pageMacro);
            this.tabMain.Controls.Add(this.pageTool);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(474, 416);
            this.tabMain.TabIndex = 0;
            // 
            // pageLoad
            // 
            this.pageLoad.Controls.Add(this.splitLoad);
            this.pageLoad.Location = new System.Drawing.Point(4, 22);
            this.pageLoad.Name = "pageLoad";
            this.pageLoad.Padding = new System.Windows.Forms.Padding(3);
            this.pageLoad.Size = new System.Drawing.Size(466, 390);
            this.pageLoad.TabIndex = 0;
            this.pageLoad.Text = "载入";
            this.pageLoad.UseVisualStyleBackColor = true;
            // 
            // splitLoad
            // 
            this.splitLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitLoad.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitLoad.IsSplitterFixed = true;
            this.splitLoad.Location = new System.Drawing.Point(3, 3);
            this.splitLoad.Name = "splitLoad";
            this.splitLoad.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitLoad.Panel1
            // 
            this.splitLoad.Panel1.Controls.Add(this.listViewLoad);
            // 
            // splitLoad.Panel2
            // 
            this.splitLoad.Panel2.Controls.Add(this.flowLoadPanel);
            this.splitLoad.Size = new System.Drawing.Size(460, 384);
            this.splitLoad.SplitterDistance = 355;
            this.splitLoad.TabIndex = 0;
            // 
            // listViewLoad
            // 
            this.listViewLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewLoad.LargeImageList = this.imageListImg;
            this.listViewLoad.Location = new System.Drawing.Point(0, 0);
            this.listViewLoad.MultiSelect = false;
            this.listViewLoad.Name = "listViewLoad";
            this.listViewLoad.Size = new System.Drawing.Size(460, 355);
            this.listViewLoad.TabIndex = 0;
            this.listViewLoad.UseCompatibleStateImageBehavior = false;
            this.listViewLoad.DoubleClick += new System.EventHandler(this.listViewLoad_DoubleClick);
            // 
            // imageListImg
            // 
            this.imageListImg.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageListImg.ImageSize = new System.Drawing.Size(96, 96);
            this.imageListImg.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // flowLoadPanel
            // 
            this.flowLoadPanel.Controls.Add(this.butImgRefresh);
            this.flowLoadPanel.Controls.Add(this.butImgBrowse);
            this.flowLoadPanel.Controls.Add(this.btnImgLast);
            this.flowLoadPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLoadPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLoadPanel.Name = "flowLoadPanel";
            this.flowLoadPanel.Size = new System.Drawing.Size(460, 25);
            this.flowLoadPanel.TabIndex = 0;
            // 
            // butImgRefresh
            // 
            this.butImgRefresh.AutoSize = true;
            this.butImgRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butImgRefresh.Location = new System.Drawing.Point(3, 3);
            this.butImgRefresh.Name = "butImgRefresh";
            this.butImgRefresh.Size = new System.Drawing.Size(39, 22);
            this.butImgRefresh.TabIndex = 1;
            this.butImgRefresh.Text = "刷新";
            this.butImgRefresh.UseVisualStyleBackColor = true;
            this.butImgRefresh.Click += new System.EventHandler(this.butImgRefresh_Click);
            // 
            // butImgBrowse
            // 
            this.butImgBrowse.Location = new System.Drawing.Point(48, 3);
            this.butImgBrowse.Name = "butImgBrowse";
            this.butImgBrowse.Size = new System.Drawing.Size(75, 23);
            this.butImgBrowse.TabIndex = 2;
            this.butImgBrowse.Text = "更改目录";
            this.butImgBrowse.UseVisualStyleBackColor = true;
            this.butImgBrowse.Click += new System.EventHandler(this.butImgBrowse_Click);
            // 
            // pageData
            // 
            this.pageData.Controls.Add(this.listViewData);
            this.pageData.Controls.Add(this.menuStrip1);
            this.pageData.Location = new System.Drawing.Point(4, 22);
            this.pageData.Name = "pageData";
            this.pageData.Padding = new System.Windows.Forms.Padding(3);
            this.pageData.Size = new System.Drawing.Size(466, 390);
            this.pageData.TabIndex = 2;
            this.pageData.Text = "数据";
            this.pageData.UseVisualStyleBackColor = true;
            // 
            // listViewData
            // 
            this.listViewData.ContextMenuStrip = this.contextMenuData;
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewData.LargeImageList = this.imageListTool;
            this.listViewData.Location = new System.Drawing.Point(3, 28);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(460, 359);
            this.listViewData.TabIndex = 0;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.DoubleClick += new System.EventHandler(this.dataOpenToolStripMenuItem_Click);
            // 
            // contextMenuData
            // 
            this.contextMenuData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataOpenToolStripMenuItem,
            this.dataSetIconToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.contextMenuData.Name = "contextMenuData";
            this.contextMenuData.Size = new System.Drawing.Size(125, 70);
            this.contextMenuData.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuData_Opening);
            // 
            // dataOpenToolStripMenuItem
            // 
            this.dataOpenToolStripMenuItem.Name = "dataOpenToolStripMenuItem";
            this.dataOpenToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.dataOpenToolStripMenuItem.Text = "打开";
            this.dataOpenToolStripMenuItem.Click += new System.EventHandler(this.dataOpenToolStripMenuItem_Click);
            // 
            // dataSetIconToolStripMenuItem
            // 
            this.dataSetIconToolStripMenuItem.Name = "dataSetIconToolStripMenuItem";
            this.dataSetIconToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.dataSetIconToolStripMenuItem.Text = "设置图标";
            this.dataSetIconToolStripMenuItem.Click += new System.EventHandler(this.dataSetIconToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.refreshToolStripMenuItem.Text = "刷新";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // imageListTool
            // 
            this.imageListTool.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTool.ImageStream")));
            this.imageListTool.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListTool.Images.SetKeyName(0, "vista_Pleasant_53.png");
            this.imageListTool.Images.SetKeyName(1, "vista_Pleasant_13.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.processOpenToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(460, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // processOpenToolStripMenuItem
            // 
            this.processOpenToolStripMenuItem.Name = "processOpenToolStripMenuItem";
            this.processOpenToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.processOpenToolStripMenuItem.Text = "处理";
            this.processOpenToolStripMenuItem.Click += new System.EventHandler(this.processOpenToolStripMenuItem_Click);
            // 
            // pageMacro
            // 
            this.pageMacro.Controls.Add(this.textMacro);
            this.pageMacro.Controls.Add(this.flowLayoutPanel1);
            this.pageMacro.Location = new System.Drawing.Point(4, 22);
            this.pageMacro.Name = "pageMacro";
            this.pageMacro.Padding = new System.Windows.Forms.Padding(3);
            this.pageMacro.Size = new System.Drawing.Size(466, 390);
            this.pageMacro.TabIndex = 3;
            this.pageMacro.Text = "宏录制";
            this.pageMacro.UseVisualStyleBackColor = true;
            // 
            // textMacro
            // 
            this.textMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textMacro.Location = new System.Drawing.Point(3, 31);
            this.textMacro.Multiline = true;
            this.textMacro.Name = "textMacro";
            this.textMacro.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textMacro.Size = new System.Drawing.Size(460, 356);
            this.textMacro.TabIndex = 1;
            this.textMacro.Text = resources.GetString("textMacro.Text");
            this.textMacro.WordWrap = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.butMacroNew);
            this.flowLayoutPanel1.Controls.Add(this.checkMacroRecording);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(460, 28);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // butMacroNew
            // 
            this.butMacroNew.AutoSize = true;
            this.butMacroNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butMacroNew.Location = new System.Drawing.Point(3, 3);
            this.butMacroNew.Name = "butMacroNew";
            this.butMacroNew.Size = new System.Drawing.Size(51, 22);
            this.butMacroNew.TabIndex = 0;
            this.butMacroNew.Text = "新建宏";
            this.butMacroNew.UseVisualStyleBackColor = true;
            this.butMacroNew.Click += new System.EventHandler(this.butMacroNew_Click);
            // 
            // checkMacroRecording
            // 
            this.checkMacroRecording.AutoSize = true;
            this.checkMacroRecording.Location = new System.Drawing.Point(60, 6);
            this.checkMacroRecording.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.checkMacroRecording.Name = "checkMacroRecording";
            this.checkMacroRecording.Size = new System.Drawing.Size(108, 16);
            this.checkMacroRecording.TabIndex = 2;
            this.checkMacroRecording.Text = "录制操作的处理";
            this.checkMacroRecording.UseVisualStyleBackColor = true;
            // 
            // pageTool
            // 
            this.pageTool.Controls.Add(this.listViewTool);
            this.pageTool.Controls.Add(this.menuTool);
            this.pageTool.Location = new System.Drawing.Point(4, 22);
            this.pageTool.Name = "pageTool";
            this.pageTool.Padding = new System.Windows.Forms.Padding(3);
            this.pageTool.Size = new System.Drawing.Size(466, 390);
            this.pageTool.TabIndex = 4;
            this.pageTool.Text = "工具";
            this.pageTool.UseVisualStyleBackColor = true;
            // 
            // listViewTool
            // 
            this.listViewTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewTool.LargeImageList = this.imageListTool;
            this.listViewTool.Location = new System.Drawing.Point(3, 28);
            this.listViewTool.Name = "listViewTool";
            this.listViewTool.Size = new System.Drawing.Size(460, 359);
            this.listViewTool.TabIndex = 0;
            this.listViewTool.UseCompatibleStateImageBehavior = false;
            this.listViewTool.DoubleClick += new System.EventHandler(this.listViewTool_DoubleClick);
            // 
            // menuTool
            // 
            this.menuTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemToolBrowse});
            this.menuTool.Location = new System.Drawing.Point(3, 3);
            this.menuTool.Name = "menuTool";
            this.menuTool.Size = new System.Drawing.Size(460, 25);
            this.menuTool.TabIndex = 1;
            this.menuTool.Text = "menuStrip1";
            // 
            // menuItemToolBrowse
            // 
            this.menuItemToolBrowse.Name = "menuItemToolBrowse";
            this.menuItemToolBrowse.Size = new System.Drawing.Size(68, 21);
            this.menuItemToolBrowse.Text = "打开目录";
            this.menuItemToolBrowse.Click += new System.EventHandler(this.menuItemToolBrowse_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // notify
            // 
            this.notify.ContextMenuStrip = this.contextMenuNotify;
            this.notify.Icon = ((System.Drawing.Icon)(resources.GetObject("notify.Icon")));
            this.notify.Text = "指纹全局特征分析实验";
            this.notify.Visible = true;
            this.notify.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseClick);
            // 
            // contextMenuNotify
            // 
            this.contextMenuNotify.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contextMenuNotify.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.processToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuNotify.Name = "contextMenuNotify";
            this.contextMenuNotify.Size = new System.Drawing.Size(187, 130);
            this.contextMenuNotify.Text = "指纹全局特征分析实验";
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(186, 26);
            this.showToolStripMenuItem.Text = "显示/隐藏主界面";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Enabled = false;
            this.dataToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("dataToolStripMenuItem.Image")));
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
            this.dataToolStripMenuItem.Text = "查看数据";
            // 
            // processToolStripMenuItem
            // 
            this.processToolStripMenuItem.Enabled = false;
            this.processToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("processToolStripMenuItem.Image")));
            this.processToolStripMenuItem.Name = "processToolStripMenuItem";
            this.processToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.processToolStripMenuItem.Text = "处理";
            this.processToolStripMenuItem.Click += new System.EventHandler(this.processToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 26);
            this.exitToolStripMenuItem.Text = "退出";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // btnImgLast
            // 
            this.btnImgLast.Location = new System.Drawing.Point(129, 3);
            this.btnImgLast.Name = "btnImgLast";
            this.btnImgLast.Size = new System.Drawing.Size(75, 23);
            this.btnImgLast.TabIndex = 3;
            this.btnImgLast.Text = "上次打开";
            this.btnImgLast.UseVisualStyleBackColor = true;
            this.btnImgLast.Click += new System.EventHandler(this.btnImgLast_Click);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 416);
            this.Controls.Add(this.splitMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "指纹全局特征分析实验";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureView)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.pageLoad.ResumeLayout(false);
            this.splitLoad.Panel1.ResumeLayout(false);
            this.splitLoad.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitLoad)).EndInit();
            this.splitLoad.ResumeLayout(false);
            this.flowLoadPanel.ResumeLayout(false);
            this.flowLoadPanel.PerformLayout();
            this.pageData.ResumeLayout(false);
            this.pageData.PerformLayout();
            this.contextMenuData.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pageMacro.ResumeLayout(false);
            this.pageMacro.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.pageTool.ResumeLayout(false);
            this.pageTool.PerformLayout();
            this.menuTool.ResumeLayout(false);
            this.menuTool.PerformLayout();
            this.contextMenuNotify.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.PictureBox pictureView;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage pageLoad;
        private System.Windows.Forms.SplitContainer splitLoad;
        private System.Windows.Forms.ListView listViewLoad;
        private System.Windows.Forms.FlowLayoutPanel flowLoadPanel;
        private System.Windows.Forms.Button butImgRefresh;
        private System.Windows.Forms.TabPage pageData;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserImg;
        private System.Windows.Forms.ImageList imageListImg;
        private System.Windows.Forms.Button butImgBrowse;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TabPage pageMacro;
        private System.Windows.Forms.TextBox textMacro;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button butMacroNew;
        private System.Windows.Forms.CheckBox checkMacroRecording;
        private System.Windows.Forms.TabPage pageTool;
        private System.Windows.Forms.ListView listViewTool;
        private System.Windows.Forms.ImageList imageListTool;
        private System.Windows.Forms.MenuStrip menuTool;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolBrowse;
        private System.Windows.Forms.ListView listViewData;
        private System.Windows.Forms.ContextMenuStrip contextMenuData;
        private System.Windows.Forms.ToolStripMenuItem dataOpenToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem processOpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataSetIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notify;
        private System.Windows.Forms.ContextMenuStrip contextMenuNotify;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem processToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Timer timerTaskbar;
        private System.Windows.Forms.Button btnImgLast;
    }
}