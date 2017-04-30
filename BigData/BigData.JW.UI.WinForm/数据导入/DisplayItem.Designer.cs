namespace BigData.JW.UI.WinForm 
{
    partial class DisplayItem
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbItemType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbShortName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbTableName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSeq = new System.Windows.Forms.TextBox();
            this.cbStatus = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDescrible = new System.Windows.Forms.TextBox();
            this.btnRestore = new System.Windows.Forms.Button();
            this.cmsItemDetail = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewItemMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DelItemStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.cmsItemDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "类型:";
            // 
            // cbItemType
            // 
            this.cbItemType.DisplayMember = "0";
            this.cbItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbItemType.FormattingEnabled = true;
            this.cbItemType.Location = new System.Drawing.Point(96, 80);
            this.cbItemType.Name = "cbItemType";
            this.cbItemType.Size = new System.Drawing.Size(368, 28);
            this.cbItemType.TabIndex = 1;
            this.cbItemType.SelectedIndexChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "项目名称:";
            // 
            // tbName
            // 
            this.tbName.ImeMode = System.Windows.Forms.ImeMode.On;
            this.tbName.Location = new System.Drawing.Point(96, 12);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(368, 27);
            this.tbName.TabIndex = 2;
            this.tbName.TextChanged += new System.EventHandler(this.ModifyChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "项目简称:";
            // 
            // tbShortName
            // 
            this.tbShortName.Location = new System.Drawing.Point(96, 46);
            this.tbShortName.Name = "tbShortName";
            this.tbShortName.Size = new System.Drawing.Size(368, 27);
            this.tbShortName.TabIndex = 2;
            this.tbShortName.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "存储路径:";
            // 
            // tbPath
            // 
            this.tbPath.Location = new System.Drawing.Point(96, 114);
            this.tbPath.Name = "tbPath";
            this.tbPath.Size = new System.Drawing.Size(368, 27);
            this.tbPath.TabIndex = 2;
            this.tbPath.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 21);
            this.label5.TabIndex = 0;
            this.label5.Text = "存储表名:";
            // 
            // tbTableName
            // 
            this.tbTableName.Location = new System.Drawing.Point(96, 148);
            this.tbTableName.Name = "tbTableName";
            this.tbTableName.Size = new System.Drawing.Size(368, 27);
            this.tbTableName.TabIndex = 2;
            this.tbTableName.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "顺序:";
            // 
            // tbSeq
            // 
            this.tbSeq.Location = new System.Drawing.Point(96, 182);
            this.tbSeq.Name = "tbSeq";
            this.tbSeq.Size = new System.Drawing.Size(368, 27);
            this.tbSeq.TabIndex = 2;
            this.tbSeq.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // cbStatus
            // 
            this.cbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbStatus.AutoSize = true;
            this.cbStatus.Checked = true;
            this.cbStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbStatus.Location = new System.Drawing.Point(96, 298);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(62, 25);
            this.cbStatus.TabIndex = 3;
            this.cbStatus.Text = "启用";
            this.cbStatus.UseVisualStyleBackColor = true;
            this.cbStatus.CheckedChanged += new System.EventHandler(this.ModifyChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(44, 217);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "描述:";
            // 
            // tbDescrible
            // 
            this.tbDescrible.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbDescrible.Location = new System.Drawing.Point(96, 221);
            this.tbDescrible.Multiline = true;
            this.tbDescrible.Name = "tbDescrible";
            this.tbDescrible.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbDescrible.Size = new System.Drawing.Size(368, 71);
            this.tbDescrible.TabIndex = 2;
            this.tbDescrible.TextChanged += new System.EventHandler(this.ItemChanged);
            // 
            // btnRestore
            // 
            this.btnRestore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestore.Location = new System.Drawing.Point(181, 334);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(122, 35);
            this.btnRestore.TabIndex = 4;
            this.btnRestore.Text = "恢复原值";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // cmsItemDetail
            // 
            this.cmsItemDetail.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.cmsItemDetail.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewItemMenuItem,
            this.DelItemStripMenuItem});
            this.cmsItemDetail.Name = "cmsItemEdit";
            this.cmsItemDetail.Size = new System.Drawing.Size(131, 48);
            // 
            // NewItemMenuItem
            // 
            this.NewItemMenuItem.Name = "NewItemMenuItem";
            this.NewItemMenuItem.Size = new System.Drawing.Size(130, 22);
            this.NewItemMenuItem.Text = "新增项目";
            this.NewItemMenuItem.Click += new System.EventHandler(this.NewItemMenuItem_Click);
            // 
            // DelItemStripMenuItem
            // 
            this.DelItemStripMenuItem.Name = "DelItemStripMenuItem";
            this.DelItemStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.DelItemStripMenuItem.Text = "删除项目";
            this.DelItemStripMenuItem.Click += new System.EventHandler(this.DelItemStripMenuItem_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // DisplayItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 387);
            this.Controls.Add(this.btnRestore);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.tbDescrible);
            this.Controls.Add(this.tbSeq);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbTableName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbShortName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.cbItemType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "DisplayItem";
            this.Text = "DisplayItem";
            this.Load += new System.EventHandler(this.DisplayItem_Load);
            this.cmsItemDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbItemType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbShortName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbTableName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSeq;
        private System.Windows.Forms.CheckBox cbStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDescrible;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.ContextMenuStrip cmsItemDetail;
        private System.Windows.Forms.ToolStripMenuItem NewItemMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DelItemStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}