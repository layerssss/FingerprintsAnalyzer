namespace Tool_MachingPerformance
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listView1 = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listView2 = new System.Windows.Forms.ListView();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.singleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cMatchResult = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cAnlyMacro = new System.Windows.Forms.ComboBox();
            this.cAnlyResult = new System.Windows.Forms.ComboBox();
            this.cMatchMacro = new System.Windows.Forms.ComboBox();
            this.cTmpMinuetia = new System.Windows.Forms.ComboBox();
            this.cTgtMinuetia = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.butClearAll = new System.Windows.Forms.Button();
            this.bAnyAll = new System.Windows.Forms.Button();
            this.bFAR = new System.Windows.Forms.Button();
            this.bFRR = new System.Windows.Forms.Button();
            this.bFAScoreSampling = new System.Windows.Forms.Button();
            this.bFRScoreSampling = new System.Windows.Forms.Button();
            this.tMatchThreshold = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.取消当前操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.查看模板ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看目标ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部清空ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pAnalysis = new System.Diagnostics.Process();
            this.pQuick = new System.Diagnostics.Process();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(573, 277);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(565, 251);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "库";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView2);
            this.splitContainer1.Size = new System.Drawing.Size(559, 245);
            this.splitContainer1.SplitterDistance = 304;
            this.splitContainer1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.HideSelection = false;
            this.listView1.LargeImageList = this.imageList1;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(304, 245);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(128, 128);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listView2
            // 
            this.listView2.ContextMenuStrip = this.contextMenuStrip2;
            this.listView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(0, 0);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(251, 245);
            this.listView2.TabIndex = 0;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.singleToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(111, 26);
            // 
            // singleToolStripMenuItem
            // 
            this.singleToolStripMenuItem.Name = "singleToolStripMenuItem";
            this.singleToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.singleToolStripMenuItem.Text = "single";
            this.singleToolStripMenuItem.Click += new System.EventHandler(this.singleToolStripMenuItem_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(565, 251);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.cMatchResult, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cAnlyMacro, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cAnlyResult, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.cMatchMacro, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.cTmpMinuetia, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cTgtMinuetia, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.tMatchThreshold, 1, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(559, 245);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // cMatchResult
            // 
            this.cMatchResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cMatchResult.FormattingEnabled = true;
            this.cMatchResult.Location = new System.Drawing.Point(78, 133);
            this.cMatchResult.Name = "cMatchResult";
            this.cMatchResult.Size = new System.Drawing.Size(482, 20);
            this.cMatchResult.TabIndex = 12;
            this.cMatchResult.SelectedIndexChanged += new System.EventHandler(this.cMatchResult_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(0, 156);
            this.label7.Margin = new System.Windows.Forms.Padding(0);
            this.label7.Name = "label7";
            this.label7.Padding = new System.Windows.Forms.Padding(5);
            this.label7.Size = new System.Drawing.Size(75, 27);
            this.label7.TabIndex = 11;
            this.label7.Text = "分数阀值";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(0, 130);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(5);
            this.label6.Size = new System.Drawing.Size(75, 26);
            this.label6.TabIndex = 9;
            this.label6.Text = "提取分数";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(0, 104);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(5);
            this.label5.Size = new System.Drawing.Size(75, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "目标特征";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(0, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(5);
            this.label4.Size = new System.Drawing.Size(75, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "模板特征";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(0, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(5);
            this.label3.Size = new System.Drawing.Size(75, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "匹配宏";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(5);
            this.label2.Size = new System.Drawing.Size(75, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "特征";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(5);
            this.label1.Size = new System.Drawing.Size(75, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "特征分析宏";
            // 
            // cAnlyMacro
            // 
            this.cAnlyMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cAnlyMacro.FormattingEnabled = true;
            this.cAnlyMacro.Location = new System.Drawing.Point(78, 3);
            this.cAnlyMacro.Name = "cAnlyMacro";
            this.cAnlyMacro.Size = new System.Drawing.Size(482, 20);
            this.cAnlyMacro.TabIndex = 1;
            this.cAnlyMacro.SelectedIndexChanged += new System.EventHandler(this.cAnlyMacro_SelectedIndexChanged);
            // 
            // cAnlyResult
            // 
            this.cAnlyResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cAnlyResult.FormattingEnabled = true;
            this.cAnlyResult.Location = new System.Drawing.Point(78, 29);
            this.cAnlyResult.Name = "cAnlyResult";
            this.cAnlyResult.Size = new System.Drawing.Size(482, 20);
            this.cAnlyResult.TabIndex = 1;
            this.cAnlyResult.SelectedIndexChanged += new System.EventHandler(this.cAnlyResult_SelectedIndexChanged);
            // 
            // cMatchMacro
            // 
            this.cMatchMacro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cMatchMacro.FormattingEnabled = true;
            this.cMatchMacro.Location = new System.Drawing.Point(78, 55);
            this.cMatchMacro.Name = "cMatchMacro";
            this.cMatchMacro.Size = new System.Drawing.Size(482, 20);
            this.cMatchMacro.TabIndex = 1;
            this.cMatchMacro.SelectedIndexChanged += new System.EventHandler(this.cMatchMacro_SelectedIndexChanged);
            // 
            // cTmpMinuetia
            // 
            this.cTmpMinuetia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTmpMinuetia.FormattingEnabled = true;
            this.cTmpMinuetia.Location = new System.Drawing.Point(78, 81);
            this.cTmpMinuetia.Name = "cTmpMinuetia";
            this.cTmpMinuetia.Size = new System.Drawing.Size(482, 20);
            this.cTmpMinuetia.TabIndex = 1;
            this.cTmpMinuetia.SelectedIndexChanged += new System.EventHandler(this.cTmpMinuetia_SelectedIndexChanged);
            // 
            // cTgtMinuetia
            // 
            this.cTgtMinuetia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTgtMinuetia.FormattingEnabled = true;
            this.cTgtMinuetia.Location = new System.Drawing.Point(78, 107);
            this.cTgtMinuetia.Name = "cTgtMinuetia";
            this.cTgtMinuetia.Size = new System.Drawing.Size(482, 20);
            this.cTgtMinuetia.TabIndex = 1;
            this.cTgtMinuetia.SelectedIndexChanged += new System.EventHandler(this.cTgtMinuetia_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.butClearAll);
            this.flowLayoutPanel1.Controls.Add(this.bAnyAll);
            this.flowLayoutPanel1.Controls.Add(this.bFAR);
            this.flowLayoutPanel1.Controls.Add(this.bFRR);
            this.flowLayoutPanel1.Controls.Add(this.bFAScoreSampling);
            this.flowLayoutPanel1.Controls.Add(this.bFRScoreSampling);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 186);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(557, 56);
            this.flowLayoutPanel1.TabIndex = 10;
            // 
            // butClearAll
            // 
            this.butClearAll.AutoSize = true;
            this.butClearAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butClearAll.Location = new System.Drawing.Point(3, 3);
            this.butClearAll.Name = "butClearAll";
            this.butClearAll.Size = new System.Drawing.Size(111, 22);
            this.butClearAll.TabIndex = 6;
            this.butClearAll.Text = "清理之前分析结果";
            this.butClearAll.UseVisualStyleBackColor = true;
            this.butClearAll.Click += new System.EventHandler(this.butClearAll_Click);
            // 
            // bAnyAll
            // 
            this.bAnyAll.AutoSize = true;
            this.bAnyAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bAnyAll.Location = new System.Drawing.Point(120, 3);
            this.bAnyAll.Name = "bAnyAll";
            this.bAnyAll.Size = new System.Drawing.Size(63, 22);
            this.bAnyAll.TabIndex = 5;
            this.bAnyAll.Text = "分析所有";
            this.bAnyAll.UseVisualStyleBackColor = true;
            this.bAnyAll.Click += new System.EventHandler(this.bAnyAll_Click);
            // 
            // bFAR
            // 
            this.bFAR.AutoSize = true;
            this.bFAR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bFAR.Enabled = false;
            this.bFAR.Location = new System.Drawing.Point(189, 3);
            this.bFAR.Name = "bFAR";
            this.bFAR.Size = new System.Drawing.Size(33, 22);
            this.bFAR.TabIndex = 4;
            this.bFAR.Text = "FAR";
            this.bFAR.UseVisualStyleBackColor = true;
            this.bFAR.Click += new System.EventHandler(this.bFAR_Click);
            // 
            // bFRR
            // 
            this.bFRR.AutoSize = true;
            this.bFRR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bFRR.Enabled = false;
            this.bFRR.Location = new System.Drawing.Point(228, 3);
            this.bFRR.Name = "bFRR";
            this.bFRR.Size = new System.Drawing.Size(33, 22);
            this.bFRR.TabIndex = 4;
            this.bFRR.Text = "FRR";
            this.bFRR.UseVisualStyleBackColor = true;
            this.bFRR.Click += new System.EventHandler(this.bFRR_Click);
            // 
            // bFAScoreSampling
            // 
            this.bFAScoreSampling.Location = new System.Drawing.Point(267, 3);
            this.bFAScoreSampling.Name = "bFAScoreSampling";
            this.bFAScoreSampling.Size = new System.Drawing.Size(75, 23);
            this.bFAScoreSampling.TabIndex = 7;
            this.bFAScoreSampling.Text = "FA分数采样";
            this.bFAScoreSampling.UseVisualStyleBackColor = true;
            this.bFAScoreSampling.Click += new System.EventHandler(this.bFAScoreSampling_Click);
            // 
            // bFRScoreSampling
            // 
            this.bFRScoreSampling.Location = new System.Drawing.Point(348, 3);
            this.bFRScoreSampling.Name = "bFRScoreSampling";
            this.bFRScoreSampling.Size = new System.Drawing.Size(75, 23);
            this.bFRScoreSampling.TabIndex = 7;
            this.bFRScoreSampling.Text = "FR分数采样";
            this.bFRScoreSampling.UseVisualStyleBackColor = true;
            this.bFRScoreSampling.Click += new System.EventHandler(this.bFRScoreSampling_Click);
            // 
            // tMatchThreshold
            // 
            this.tMatchThreshold.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tMatchThreshold.Location = new System.Drawing.Point(78, 159);
            this.tMatchThreshold.Name = "tMatchThreshold";
            this.tMatchThreshold.Size = new System.Drawing.Size(482, 21);
            this.tMatchThreshold.TabIndex = 2;
            this.tMatchThreshold.TextChanged += new System.EventHandler(this.tMatchThreshold_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.menuStrip1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(565, 251);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "分析";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(3, 28);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(559, 220);
            this.textBox1.TabIndex = 0;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.取消当前操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 3);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(559, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 取消当前操作ToolStripMenuItem
            // 
            this.取消当前操作ToolStripMenuItem.Name = "取消当前操作ToolStripMenuItem";
            this.取消当前操作ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.取消当前操作ToolStripMenuItem.Text = "取消当前操作";
            this.取消当前操作ToolStripMenuItem.Click += new System.EventHandler(this.取消当前操作ToolStripMenuItem_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.listBox1);
            this.tabPage4.Controls.Add(this.progressBar1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(565, 251);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "匹配";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(3, 30);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(559, 218);
            this.listBox1.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.查看模板ToolStripMenuItem,
            this.查看目标ToolStripMenuItem,
            this.全部清空ToolStripMenuItem,
            this.全部保存ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 114);
            // 
            // 查看模板ToolStripMenuItem
            // 
            this.查看模板ToolStripMenuItem.Name = "查看模板ToolStripMenuItem";
            this.查看模板ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.查看模板ToolStripMenuItem.Text = "查看模板";
            this.查看模板ToolStripMenuItem.Click += new System.EventHandler(this.查看模板ToolStripMenuItem_Click);
            // 
            // 查看目标ToolStripMenuItem
            // 
            this.查看目标ToolStripMenuItem.Name = "查看目标ToolStripMenuItem";
            this.查看目标ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.查看目标ToolStripMenuItem.Text = "查看目标";
            this.查看目标ToolStripMenuItem.Click += new System.EventHandler(this.查看目标ToolStripMenuItem_Click);
            // 
            // 全部清空ToolStripMenuItem
            // 
            this.全部清空ToolStripMenuItem.Name = "全部清空ToolStripMenuItem";
            this.全部清空ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.全部清空ToolStripMenuItem.Text = "全部清空";
            this.全部清空ToolStripMenuItem.Click += new System.EventHandler(this.全部清空ToolStripMenuItem_Click);
            // 
            // 全部保存ToolStripMenuItem
            // 
            this.全部保存ToolStripMenuItem.Name = "全部保存ToolStripMenuItem";
            this.全部保存ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.全部保存ToolStripMenuItem.Text = "保存全部错误项";
            this.全部保存ToolStripMenuItem.Click += new System.EventHandler(this.全部保存ToolStripMenuItem_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.progressBar1.Location = new System.Drawing.Point(3, 3);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(559, 27);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 0;
            // 
            // pAnalysis
            // 
            this.pAnalysis.EnableRaisingEvents = true;
            this.pAnalysis.StartInfo.CreateNoWindow = true;
            this.pAnalysis.StartInfo.Domain = "";
            this.pAnalysis.StartInfo.LoadUserProfile = false;
            this.pAnalysis.StartInfo.Password = null;
            this.pAnalysis.StartInfo.RedirectStandardOutput = true;
            this.pAnalysis.StartInfo.StandardErrorEncoding = null;
            this.pAnalysis.StartInfo.StandardOutputEncoding = null;
            this.pAnalysis.StartInfo.UserName = "";
            this.pAnalysis.StartInfo.UseShellExecute = false;
            this.pAnalysis.StartInfo.WorkingDirectory = "..\\data";
            this.pAnalysis.SynchronizingObject = this;
            this.pAnalysis.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(this.pAnalysis_OutputDataReceived);
            this.pAnalysis.Exited += new System.EventHandler(this.pAnalysis_Exited);
            // 
            // pQuick
            // 
            this.pQuick.StartInfo.CreateNoWindow = true;
            this.pQuick.StartInfo.Domain = "";
            this.pQuick.StartInfo.LoadUserProfile = false;
            this.pQuick.StartInfo.Password = null;
            this.pQuick.StartInfo.RedirectStandardOutput = true;
            this.pQuick.StartInfo.StandardErrorEncoding = null;
            this.pQuick.StartInfo.StandardOutputEncoding = null;
            this.pQuick.StartInfo.UserName = "";
            this.pQuick.StartInfo.UseShellExecute = false;
            this.pQuick.StartInfo.WorkingDirectory = "..\\data";
            this.pQuick.SynchronizingObject = this;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 277);
            this.Controls.Add(this.tabControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "FVC数据库匹配性能测试工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cAnlyMacro;
        private System.Windows.Forms.ComboBox cAnlyResult;
        private System.Windows.Forms.ComboBox cMatchMacro;
        private System.Windows.Forms.ComboBox cTmpMinuetia;
        private System.Windows.Forms.ComboBox cTgtMinuetia;
        private System.Windows.Forms.TextBox tMatchThreshold;
        private System.Windows.Forms.Button bFAR;
        private System.Windows.Forms.Button bFRR;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cMatchResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Diagnostics.Process pAnalysis;
        private System.Windows.Forms.Button bAnyAll;
        private System.Diagnostics.Process pQuick;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 取消当前操作ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 查看模板ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看目标ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem singleToolStripMenuItem;
        private System.Windows.Forms.Button butClearAll;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem 全部清空ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部保存ToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button bFAScoreSampling;
        private System.Windows.Forms.Button bFRScoreSampling;
    }
}

