namespace BigData.JW.UI.WinForm 
{
    partial class DataItemFormat
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnPreView = new System.Windows.Forms.Button();
            this.tbStartPos = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvFormat = new System.Windows.Forms.DataGridView();
            this.ColumnId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTypeName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnColNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSeq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDataValueId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemDataFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tcShowFile = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgvPreView = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgvOriginalView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormat)).BeginInit();
            this.tcShowFile.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreView)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginalView)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.btnDel);
            this.splitContainer1.Panel1.Controls.Add(this.btnPreView);
            this.splitContainer1.Panel1.Controls.Add(this.tbStartPos);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dgvFormat);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tcShowFile);
            this.splitContainer1.Size = new System.Drawing.Size(730, 445);
            this.splitContainer1.SplitterDistance = 214;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(592, 152);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(93, 29);
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "清除格式";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnPreView
            // 
            this.btnPreView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreView.Location = new System.Drawing.Point(592, 98);
            this.btnPreView.Name = "btnPreView";
            this.btnPreView.Size = new System.Drawing.Size(93, 29);
            this.btnPreView.TabIndex = 3;
            this.btnPreView.Text = "预览";
            this.btnPreView.UseVisualStyleBackColor = true;
            this.btnPreView.Click += new System.EventHandler(this.btnPreView_Click);
            // 
            // tbStartPos
            // 
            this.tbStartPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStartPos.Location = new System.Drawing.Point(609, 25);
            this.tbStartPos.Name = "tbStartPos";
            this.tbStartPos.Size = new System.Drawing.Size(53, 30);
            this.tbStartPos.TabIndex = 2;
            this.tbStartPos.TextChanged += new System.EventHandler(this.tbStartPos_TextChanged);
            this.tbStartPos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(668, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "行开始";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(557, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "从第:";
            // 
            // dgvFormat
            // 
            this.dgvFormat.AllowUserToOrderColumns = true;
            this.dgvFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFormat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormat.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnId,
            this.ColumnTypeName,
            this.ColumnValue,
            this.ColumnColNumber,
            this.ColumnSeq,
            this.ColumnDataValueId,
            this.ColumnItemDataFormat});
            this.dgvFormat.Location = new System.Drawing.Point(0, 0);
            this.dgvFormat.Name = "dgvFormat";
            this.dgvFormat.RowTemplate.Height = 24;
            this.dgvFormat.Size = new System.Drawing.Size(551, 211);
            this.dgvFormat.TabIndex = 0;
            this.dgvFormat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormat_CellClick);
            this.dgvFormat.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormat_CellContentClick);
            this.dgvFormat.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFormat_CellEnter);
            this.dgvFormat.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFormat_ColumnHeaderMouseClick);
            this.dgvFormat.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvFormat_EditingControlShowing);
            // 
            // ColumnId
            // 
            this.ColumnId.HeaderText = "Id";
            this.ColumnId.Name = "ColumnId";
            this.ColumnId.ReadOnly = true;
            // 
            // ColumnTypeName
            // 
            this.ColumnTypeName.HeaderText = "值";
            this.ColumnTypeName.Name = "ColumnTypeName";
            // 
            // ColumnValue
            // 
            this.ColumnValue.HeaderText = "字段名";
            this.ColumnValue.Name = "ColumnValue";
            this.ColumnValue.ReadOnly = true;
            // 
            // ColumnColNumber
            // 
            this.ColumnColNumber.HeaderText = "列号";
            this.ColumnColNumber.Name = "ColumnColNumber";
            // 
            // ColumnSeq
            // 
            this.ColumnSeq.HeaderText = "顺序";
            this.ColumnSeq.Name = "ColumnSeq";
            this.ColumnSeq.ReadOnly = true;
            // 
            // ColumnDataValueId
            // 
            this.ColumnDataValueId.HeaderText = "DataValueId";
            this.ColumnDataValueId.Name = "ColumnDataValueId";
            // 
            // ColumnItemDataFormat
            // 
            this.ColumnItemDataFormat.HeaderText = "ItemDataFormatId";
            this.ColumnItemDataFormat.Name = "ColumnItemDataFormat";
            // 
            // tcShowFile
            // 
            this.tcShowFile.Controls.Add(this.tabPage1);
            this.tcShowFile.Controls.Add(this.tabPage2);
            this.tcShowFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcShowFile.Location = new System.Drawing.Point(0, 0);
            this.tcShowFile.Name = "tcShowFile";
            this.tcShowFile.SelectedIndex = 0;
            this.tcShowFile.Size = new System.Drawing.Size(730, 225);
            this.tcShowFile.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgvPreView);
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(722, 189);
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
            this.dgvPreView.Size = new System.Drawing.Size(716, 183);
            this.dgvPreView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgvOriginalView);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(722, 196);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "原始文件";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgvOriginalView
            // 
            this.dgvOriginalView.AllowUserToAddRows = false;
            this.dgvOriginalView.AllowUserToDeleteRows = false;
            this.dgvOriginalView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOriginalView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOriginalView.Location = new System.Drawing.Point(3, 3);
            this.dgvOriginalView.Name = "dgvOriginalView";
            this.dgvOriginalView.ReadOnly = true;
            this.dgvOriginalView.RowTemplate.Height = 24;
            this.dgvOriginalView.Size = new System.Drawing.Size(716, 190);
            this.dgvOriginalView.TabIndex = 0;
            // 
            // DataItemFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 445);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DataItemFormat";
            this.Text = "ItemFormat";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormat)).EndInit();
            this.tcShowFile.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOriginalView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbStartPos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvFormat;
        private System.Windows.Forms.Button btnPreView;
        private System.Windows.Forms.TabControl tcShowFile;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dgvPreView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvOriginalView;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnId;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnColNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSeq;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDataValueId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemDataFormat;
    }
}