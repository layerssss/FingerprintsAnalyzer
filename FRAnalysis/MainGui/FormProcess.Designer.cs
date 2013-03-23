namespace MainGui
{
    partial class FormProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormProcess));
            this.tabProcess = new System.Windows.Forms.TabControl();
            this.pageProcessInfo = new System.Windows.Forms.TabPage();
            this.textProcessInfo = new System.Windows.Forms.TextBox();
            this.pageProcessInput = new System.Windows.Forms.TabPage();
            this.tableProcessInput = new System.Windows.Forms.TableLayoutPanel();
            this.pageProcessOutput = new System.Windows.Forms.TabPage();
            this.tableProcessOutput = new System.Windows.Forms.TableLayoutPanel();
            this.pageProcessOp = new System.Windows.Forms.TabPage();
            this.textProcessOp = new System.Windows.Forms.TextBox();
            this.progressProcessOp = new System.Windows.Forms.ProgressBar();
            this.menuProcessOp = new System.Windows.Forms.MenuStrip();
            this.menuItemProcessOpStart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProcessOpCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemProcessOpClearlog = new System.Windows.Forms.ToolStripMenuItem();
            this.toolComboProcessOpConfig = new System.Windows.Forms.ToolStripComboBox();
            this.menuProcess = new System.Windows.Forms.MenuStrip();
            this.toolComboProcessType = new System.Windows.Forms.ToolStripComboBox();
            this.toolComboProcess = new System.Windows.Forms.ToolStripComboBox();
            this.menuItemProcessResetargs = new System.Windows.Forms.ToolStripMenuItem();
            this.timerProcessOutput = new System.Windows.Forms.Timer(this.components);
            this.tabProcess.SuspendLayout();
            this.pageProcessInfo.SuspendLayout();
            this.pageProcessInput.SuspendLayout();
            this.pageProcessOutput.SuspendLayout();
            this.pageProcessOp.SuspendLayout();
            this.menuProcessOp.SuspendLayout();
            this.menuProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabProcess
            // 
            this.tabProcess.Controls.Add(this.pageProcessInfo);
            this.tabProcess.Controls.Add(this.pageProcessInput);
            this.tabProcess.Controls.Add(this.pageProcessOutput);
            this.tabProcess.Controls.Add(this.pageProcessOp);
            this.tabProcess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProcess.Location = new System.Drawing.Point(0, 29);
            this.tabProcess.Name = "tabProcess";
            this.tabProcess.SelectedIndex = 0;
            this.tabProcess.Size = new System.Drawing.Size(574, 381);
            this.tabProcess.TabIndex = 2;
            this.tabProcess.Visible = false;
            // 
            // pageProcessInfo
            // 
            this.pageProcessInfo.Controls.Add(this.textProcessInfo);
            this.pageProcessInfo.Location = new System.Drawing.Point(4, 22);
            this.pageProcessInfo.Name = "pageProcessInfo";
            this.pageProcessInfo.Padding = new System.Windows.Forms.Padding(3);
            this.pageProcessInfo.Size = new System.Drawing.Size(566, 355);
            this.pageProcessInfo.TabIndex = 2;
            this.pageProcessInfo.Text = "说明";
            this.pageProcessInfo.UseVisualStyleBackColor = true;
            // 
            // textProcessInfo
            // 
            this.textProcessInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textProcessInfo.Location = new System.Drawing.Point(3, 3);
            this.textProcessInfo.Multiline = true;
            this.textProcessInfo.Name = "textProcessInfo";
            this.textProcessInfo.ReadOnly = true;
            this.textProcessInfo.Size = new System.Drawing.Size(560, 349);
            this.textProcessInfo.TabIndex = 0;
            // 
            // pageProcessInput
            // 
            this.pageProcessInput.Controls.Add(this.tableProcessInput);
            this.pageProcessInput.Location = new System.Drawing.Point(4, 22);
            this.pageProcessInput.Name = "pageProcessInput";
            this.pageProcessInput.Padding = new System.Windows.Forms.Padding(3);
            this.pageProcessInput.Size = new System.Drawing.Size(566, 355);
            this.pageProcessInput.TabIndex = 0;
            this.pageProcessInput.Text = "输入";
            this.pageProcessInput.UseVisualStyleBackColor = true;
            // 
            // tableProcessInput
            // 
            this.tableProcessInput.ColumnCount = 2;
            this.tableProcessInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableProcessInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableProcessInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableProcessInput.Location = new System.Drawing.Point(3, 3);
            this.tableProcessInput.Name = "tableProcessInput";
            this.tableProcessInput.RowCount = 1;
            this.tableProcessInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableProcessInput.Size = new System.Drawing.Size(560, 349);
            this.tableProcessInput.TabIndex = 0;
            // 
            // pageProcessOutput
            // 
            this.pageProcessOutput.Controls.Add(this.tableProcessOutput);
            this.pageProcessOutput.Location = new System.Drawing.Point(4, 22);
            this.pageProcessOutput.Name = "pageProcessOutput";
            this.pageProcessOutput.Padding = new System.Windows.Forms.Padding(3);
            this.pageProcessOutput.Size = new System.Drawing.Size(566, 355);
            this.pageProcessOutput.TabIndex = 1;
            this.pageProcessOutput.Text = "输出";
            this.pageProcessOutput.UseVisualStyleBackColor = true;
            // 
            // tableProcessOutput
            // 
            this.tableProcessOutput.ColumnCount = 2;
            this.tableProcessOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableProcessOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableProcessOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableProcessOutput.Location = new System.Drawing.Point(3, 3);
            this.tableProcessOutput.Name = "tableProcessOutput";
            this.tableProcessOutput.RowCount = 1;
            this.tableProcessOutput.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableProcessOutput.Size = new System.Drawing.Size(560, 349);
            this.tableProcessOutput.TabIndex = 1;
            // 
            // pageProcessOp
            // 
            this.pageProcessOp.Controls.Add(this.textProcessOp);
            this.pageProcessOp.Controls.Add(this.progressProcessOp);
            this.pageProcessOp.Controls.Add(this.menuProcessOp);
            this.pageProcessOp.Location = new System.Drawing.Point(4, 22);
            this.pageProcessOp.Name = "pageProcessOp";
            this.pageProcessOp.Padding = new System.Windows.Forms.Padding(3);
            this.pageProcessOp.Size = new System.Drawing.Size(566, 355);
            this.pageProcessOp.TabIndex = 3;
            this.pageProcessOp.Text = "操作";
            this.pageProcessOp.UseVisualStyleBackColor = true;
            this.pageProcessOp.Enter += new System.EventHandler(this.pageProcessOp_Enter);
            // 
            // textProcessOp
            // 
            this.textProcessOp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textProcessOp.Location = new System.Drawing.Point(3, 28);
            this.textProcessOp.Multiline = true;
            this.textProcessOp.Name = "textProcessOp";
            this.textProcessOp.ReadOnly = true;
            this.textProcessOp.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textProcessOp.Size = new System.Drawing.Size(560, 301);
            this.textProcessOp.TabIndex = 2;
            this.textProcessOp.TextChanged += new System.EventHandler(this.textProcessOp_TextChanged);
            // 
            // progressProcessOp
            // 
            this.progressProcessOp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressProcessOp.Location = new System.Drawing.Point(3, 329);
            this.progressProcessOp.Name = "progressProcessOp";
            this.progressProcessOp.Size = new System.Drawing.Size(560, 23);
            this.progressProcessOp.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressProcessOp.TabIndex = 0;
            this.progressProcessOp.Visible = false;
            // 
            // menuProcessOp
            // 
            this.menuProcessOp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemProcessOpStart,
            this.menuItemProcessOpCancel,
            this.menuItemProcessOpClearlog,
            this.toolComboProcessOpConfig});
            this.menuProcessOp.Location = new System.Drawing.Point(3, 3);
            this.menuProcessOp.Name = "menuProcessOp";
            this.menuProcessOp.Size = new System.Drawing.Size(560, 25);
            this.menuProcessOp.TabIndex = 0;
            this.menuProcessOp.Text = "menuStrip1";
            // 
            // menuItemProcessOpStart
            // 
            this.menuItemProcessOpStart.Name = "menuItemProcessOpStart";
            this.menuItemProcessOpStart.Size = new System.Drawing.Size(73, 21);
            this.menuItemProcessOpStart.Text = "开始/继续";
            this.menuItemProcessOpStart.Click += new System.EventHandler(this.menuItemProcessOpStart_Click);
            // 
            // menuItemProcessOpCancel
            // 
            this.menuItemProcessOpCancel.Name = "menuItemProcessOpCancel";
            this.menuItemProcessOpCancel.Size = new System.Drawing.Size(44, 21);
            this.menuItemProcessOpCancel.Text = "取消";
            this.menuItemProcessOpCancel.Click += new System.EventHandler(this.menuItemProcessOpCancel_Click);
            // 
            // menuItemProcessOpClearlog
            // 
            this.menuItemProcessOpClearlog.Name = "menuItemProcessOpClearlog";
            this.menuItemProcessOpClearlog.Size = new System.Drawing.Size(68, 21);
            this.menuItemProcessOpClearlog.Text = "清除日志";
            this.menuItemProcessOpClearlog.Click += new System.EventHandler(this.menuItemProcessOpClearlog_Click);
            // 
            // toolComboProcessOpConfig
            // 
            this.toolComboProcessOpConfig.Items.AddRange(new object[] {
            "直接运行",
            "启动后等待",
            "测试"});
            this.toolComboProcessOpConfig.Name = "toolComboProcessOpConfig";
            this.toolComboProcessOpConfig.Size = new System.Drawing.Size(121, 25);
            // 
            // menuProcess
            // 
            this.menuProcess.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolComboProcessType,
            this.toolComboProcess,
            this.menuItemProcessResetargs});
            this.menuProcess.Location = new System.Drawing.Point(0, 0);
            this.menuProcess.Name = "menuProcess";
            this.menuProcess.Size = new System.Drawing.Size(574, 29);
            this.menuProcess.TabIndex = 3;
            this.menuProcess.Text = "menuStrip1";
            // 
            // toolComboProcessType
            // 
            this.toolComboProcessType.Name = "toolComboProcessType";
            this.toolComboProcessType.Size = new System.Drawing.Size(150, 25);
            this.toolComboProcessType.SelectedIndexChanged += new System.EventHandler(this.toolComboProcessType_SelectedIndexChanged);
            // 
            // toolComboProcess
            // 
            this.toolComboProcess.Name = "toolComboProcess";
            this.toolComboProcess.Size = new System.Drawing.Size(150, 25);
            this.toolComboProcess.SelectedIndexChanged += new System.EventHandler(this.toolComboProcess_SelectedIndexChanged);
            // 
            // menuItemProcessResetargs
            // 
            this.menuItemProcessResetargs.Name = "menuItemProcessResetargs";
            this.menuItemProcessResetargs.Size = new System.Drawing.Size(68, 25);
            this.menuItemProcessResetargs.Text = "重置参数";
            this.menuItemProcessResetargs.Click += new System.EventHandler(this.menuItemProcessResetargs_Click);
            // 
            // timerProcessOutput
            // 
            this.timerProcessOutput.Enabled = true;
            this.timerProcessOutput.Tick += new System.EventHandler(this.timerProcessOutput_Tick);
            // 
            // FormProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 410);
            this.Controls.Add(this.tabProcess);
            this.Controls.Add(this.menuProcess);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormProcess";
            this.ShowInTaskbar = false;
            this.Text = "处理";
            this.Load += new System.EventHandler(this.FormProcess_Load);
            this.tabProcess.ResumeLayout(false);
            this.pageProcessInfo.ResumeLayout(false);
            this.pageProcessInfo.PerformLayout();
            this.pageProcessInput.ResumeLayout(false);
            this.pageProcessOutput.ResumeLayout(false);
            this.pageProcessOp.ResumeLayout(false);
            this.pageProcessOp.PerformLayout();
            this.menuProcessOp.ResumeLayout(false);
            this.menuProcessOp.PerformLayout();
            this.menuProcess.ResumeLayout(false);
            this.menuProcess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabProcess;
        private System.Windows.Forms.TabPage pageProcessInfo;
        private System.Windows.Forms.TextBox textProcessInfo;
        private System.Windows.Forms.TabPage pageProcessInput;
        private System.Windows.Forms.TableLayoutPanel tableProcessInput;
        private System.Windows.Forms.TabPage pageProcessOutput;
        private System.Windows.Forms.TableLayoutPanel tableProcessOutput;
        private System.Windows.Forms.TabPage pageProcessOp;
        private System.Windows.Forms.TextBox textProcessOp;
        private System.Windows.Forms.ProgressBar progressProcessOp;
        private System.Windows.Forms.MenuStrip menuProcessOp;
        private System.Windows.Forms.ToolStripMenuItem menuItemProcessOpStart;
        private System.Windows.Forms.ToolStripMenuItem menuItemProcessOpCancel;
        private System.Windows.Forms.ToolStripMenuItem menuItemProcessOpClearlog;
        private System.Windows.Forms.ToolStripComboBox toolComboProcessOpConfig;
        private System.Windows.Forms.MenuStrip menuProcess;
        private System.Windows.Forms.ToolStripComboBox toolComboProcessType;
        private System.Windows.Forms.ToolStripComboBox toolComboProcess;
        private System.Windows.Forms.ToolStripMenuItem menuItemProcessResetargs;
        private System.Windows.Forms.Timer timerProcessOutput;
    }
}