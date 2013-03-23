namespace MainGui
{
    partial class FormDataList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataList));
            this.listViewData = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.delToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuSetLocal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuSetGlobal = new System.Windows.Forms.ToolStripMenuItem();
            this.toolMenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewData
            // 
            this.listViewData.ContextMenuStrip = this.contextMenuStrip1;
            this.listViewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewData.Location = new System.Drawing.Point(0, 0);
            this.listViewData.Name = "listViewData";
            this.listViewData.Size = new System.Drawing.Size(453, 367);
            this.listViewData.TabIndex = 2;
            this.listViewData.UseCompatibleStateImageBehavior = false;
            this.listViewData.View = System.Windows.Forms.View.List;
            this.listViewData.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listViewData_ItemDrag);
            this.listViewData.DoubleClick += new System.EventHandler(this.listViewData_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolMenuCopy,
            this.toolMenuRename,
            this.delToolStripMenuItem,
            this.toolMenuSetLocal,
            this.toolMenuSetGlobal,
            this.toolMenuRefresh,
            this.newToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 180);
            this.contextMenuStrip1.Opened += new System.EventHandler(this.contextMenuStrip1_Opened);
            // 
            // toolMenuCopy
            // 
            this.toolMenuCopy.Name = "toolMenuCopy";
            this.toolMenuCopy.Size = new System.Drawing.Size(160, 22);
            this.toolMenuCopy.Text = "复制";
            this.toolMenuCopy.Click += new System.EventHandler(this.toolMenuCopy_Click);
            // 
            // toolMenuRename
            // 
            this.toolMenuRename.Name = "toolMenuRename";
            this.toolMenuRename.Size = new System.Drawing.Size(160, 22);
            this.toolMenuRename.Text = "重命名";
            this.toolMenuRename.Click += new System.EventHandler(this.toolMenuRename_Click);
            // 
            // delToolStripMenuItem
            // 
            this.delToolStripMenuItem.Name = "delToolStripMenuItem";
            this.delToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.delToolStripMenuItem.Text = "删除";
            this.delToolStripMenuItem.Click += new System.EventHandler(this.delToolStripMenuItem_Click);
            // 
            // toolMenuSetLocal
            // 
            this.toolMenuSetLocal.Name = "toolMenuSetLocal";
            this.toolMenuSetLocal.Size = new System.Drawing.Size(160, 22);
            this.toolMenuSetLocal.Text = "设置为本地数据";
            this.toolMenuSetLocal.Click += new System.EventHandler(this.toolMenuSetLocal_Click);
            // 
            // toolMenuSetGlobal
            // 
            this.toolMenuSetGlobal.Name = "toolMenuSetGlobal";
            this.toolMenuSetGlobal.Size = new System.Drawing.Size(160, 22);
            this.toolMenuSetGlobal.Text = "设置为全局数据";
            this.toolMenuSetGlobal.Click += new System.EventHandler(this.toolMenuSetGlobal_Click);
            // 
            // toolMenuRefresh
            // 
            this.toolMenuRefresh.Name = "toolMenuRefresh";
            this.toolMenuRefresh.Size = new System.Drawing.Size(160, 22);
            this.toolMenuRefresh.Text = "刷新";
            this.toolMenuRefresh.Click += new System.EventHandler(this.toolMenuRefresh_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.newToolStripMenuItem.Text = "创建";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // FormDataList
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(453, 367);
            this.Controls.Add(this.listViewData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDataList";
            this.ShowInTaskbar = false;
            this.Text = "FormDataList";
            this.Load += new System.EventHandler(this.FormDataList_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormDataList_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormDataList_DragEnter);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewData;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolMenuCopy;
        private System.Windows.Forms.ToolStripMenuItem toolMenuRename;
        private System.Windows.Forms.ToolStripMenuItem toolMenuRefresh;
        private System.Windows.Forms.ToolStripMenuItem toolMenuSetLocal;
        private System.Windows.Forms.ToolStripMenuItem toolMenuSetGlobal;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem delToolStripMenuItem;
    }
}