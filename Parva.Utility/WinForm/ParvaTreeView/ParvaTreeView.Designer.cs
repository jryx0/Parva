namespace Parva.Utility.WinForm
{
    partial class ParvaTreeView  
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParvaTreeView));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ptreeView = new System.Windows.Forms.TreeView();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.cmsTreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewNode_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelAccount_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cmsTreeMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ptreeView);
            this.splitContainer1.Size = new System.Drawing.Size(633, 446);
            this.splitContainer1.SplitterDistance = 221;
            this.splitContainer1.TabIndex = 0;
            // 
            // ptreeView
            // 
            this.ptreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptreeView.ImageIndex = 0;
            this.ptreeView.ImageList = this.imgList;
            this.ptreeView.Location = new System.Drawing.Point(0, 0);
            this.ptreeView.Name = "ptreeView";
            this.ptreeView.SelectedImageIndex = 0;
            this.ptreeView.Size = new System.Drawing.Size(221, 446);
            this.ptreeView.TabIndex = 0;
            this.ptreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.ptreeView_AfterLabelEdit);
            this.ptreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.ptreeView_BeforeSelect);
            this.ptreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ptreeView_AfterSelect);
            this.ptreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.ptreeView_NodeMouseDoubleClick);
            this.ptreeView.Leave += new System.EventHandler(this.ptreeView_Leave);
            this.ptreeView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ptreeView_MouseUp);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "organization-128.png");
            this.imgList.Images.SetKeyName(1, "50_bar_chart_flow_diagram_organization_company_structure-128.png");
            this.imgList.Images.SetKeyName(2, "file_documents-07-128.png");
            // 
            // cmsTreeMenu
            // 
            this.cmsTreeMenu.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.cmsTreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewNode_ToolStripMenuItem,
            this.DelAccount_ToolStripMenuItem});
            this.cmsTreeMenu.Name = "contextMenuStrip1";
            this.cmsTreeMenu.Size = new System.Drawing.Size(106, 52);
            // 
            // NewNode_ToolStripMenuItem
            // 
            this.NewNode_ToolStripMenuItem.Name = "NewNode_ToolStripMenuItem";
            this.NewNode_ToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.NewNode_ToolStripMenuItem.Text = "新增";
            // 
            // DelAccount_ToolStripMenuItem
            // 
            this.DelAccount_ToolStripMenuItem.Name = "DelAccount_ToolStripMenuItem";
            this.DelAccount_ToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.DelAccount_ToolStripMenuItem.Text = "删除";
            // 
            // ParvaTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ParvaTreeView";
            this.Size = new System.Drawing.Size(633, 446);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cmsTreeMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView ptreeView;
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.ContextMenuStrip cmsTreeMenu;
        private System.Windows.Forms.ToolStripMenuItem NewNode_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelAccount_ToolStripMenuItem;
    }
}
