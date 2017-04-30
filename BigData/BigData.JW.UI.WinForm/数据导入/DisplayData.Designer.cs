namespace BigData.JW.UI.WinForm
{
    partial class DisplayData
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.tcShowFile = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvPreView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvOriginalView = new System.Windows.Forms.DataGridView();
            this.cmsDisplayData = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ImportFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeletFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.tcShowFile.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginalView)).BeginInit();
            this.cmsDisplayData.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dgvFiles);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.progressBar);
            this.splitContainer1.Panel2.Controls.Add(this.tcShowFile);
            this.splitContainer1.Size = new System.Drawing.Size(809, 454);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.SplitterWidth = 7;
            this.splitContainer1.TabIndex = 0;
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Location = new System.Drawing.Point(3, 0);
            this.dgvFiles.Margin = new System.Windows.Forms.Padding(3, 3, 3, 5);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowTemplate.Height = 24;
            this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.Size = new System.Drawing.Size(809, 235);
            this.dgvFiles.TabIndex = 0;
            this.dgvFiles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFiles_CellContentClick);
            this.dgvFiles.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvFiles_RowPostPaint);
            this.dgvFiles.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvFiles_MouseUp);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(2, 181);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(806, 10);
            this.progressBar.TabIndex = 1;
            this.progressBar.Visible = false;
            // 
            // tcShowFile
            // 
            this.tcShowFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tcShowFile.Controls.Add(this.tabPage1);
            this.tcShowFile.Controls.Add(this.tabPage2);
            this.tcShowFile.Location = new System.Drawing.Point(3, 0);
            this.tcShowFile.Margin = new System.Windows.Forms.Padding(3, 13, 3, 3);
            this.tcShowFile.Name = "tcShowFile";
            this.tcShowFile.SelectedIndex = 0;
            this.tcShowFile.Size = new System.Drawing.Size(809, 182);
            this.tcShowFile.TabIndex = 0;
            this.tcShowFile.SelectedIndexChanged += new System.EventHandler(this.tcShowFile_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvPreView);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(801, 155);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "预览";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgvPreView
            // 
            this.dgvPreView.AllowUserToAddRows = false;
            this.dgvPreView.AllowUserToDeleteRows = false;
            this.dgvPreView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPreView.Location = new System.Drawing.Point(3, 3);
            this.dgvPreView.Name = "dgvPreView";
            this.dgvPreView.ReadOnly = true;
            this.dgvPreView.RowTemplate.Height = 24;
            this.dgvPreView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPreView.Size = new System.Drawing.Size(795, 149);
            this.dgvPreView.TabIndex = 0;
            this.dgvPreView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvFiles_RowPostPaint);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvOriginalView);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(801, 155);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "原始文件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvOriginalView
            // 
            this.dgvOriginalView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOriginalView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOriginalView.Location = new System.Drawing.Point(3, 3);
            this.dgvOriginalView.Name = "dgvOriginalView";
            this.dgvOriginalView.RowTemplate.Height = 24;
            this.dgvOriginalView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOriginalView.Size = new System.Drawing.Size(795, 149);
            this.dgvOriginalView.TabIndex = 0;
            this.dgvOriginalView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvFiles_RowPostPaint);
            // 
            // cmsDisplayData
            // 
            this.cmsDisplayData.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.cmsDisplayData.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ImportFileMenuItem,
            this.DeletFileMenuItem});
            this.cmsDisplayData.Name = "cmsDisplayData";
            this.cmsDisplayData.Size = new System.Drawing.Size(134, 52);
            // 
            // ImportFileMenuItem
            // 
            this.ImportFileMenuItem.Name = "ImportFileMenuItem";
            this.ImportFileMenuItem.Size = new System.Drawing.Size(133, 24);
            this.ImportFileMenuItem.Text = "导入文件";
            this.ImportFileMenuItem.Click += new System.EventHandler(this.ImportFileMenuItem_Click);
            // 
            // DeletFileMenuItem
            // 
            this.DeletFileMenuItem.Name = "DeletFileMenuItem";
            this.DeletFileMenuItem.Size = new System.Drawing.Size(133, 24);
            this.DeletFileMenuItem.Text = "删除文件";
            this.DeletFileMenuItem.Click += new System.EventHandler(this.DeletFileMenuItem_Click);
            // 
            // DisplayData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 454);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DisplayData";
            this.Text = "DisplayData";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.tcShowFile.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginalView)).EndInit();
            this.cmsDisplayData.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.TabControl tcShowFile;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvPreView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvOriginalView;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ContextMenuStrip cmsDisplayData;
        private System.Windows.Forms.ToolStripMenuItem ImportFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeletFileMenuItem;
    }
}