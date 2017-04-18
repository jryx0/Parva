namespace Parva.Utility.WinForm 
{
    partial class DistrictDetail
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDistrictName = new System.Windows.Forms.TextBox();
            this.cmbParentDistrict = new System.Windows.Forms.ComboBox();
            this.tbDistrictCode = new System.Windows.Forms.TextBox();
            this.tbDistrictIP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tbSeq = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "地区名称:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "上级:";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "编码:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 146);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "IP地址:";
            // 
            // tbDistrictName
            // 
            this.tbDistrictName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDistrictName.Location = new System.Drawing.Point(94, 65);
            this.tbDistrictName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDistrictName.Name = "tbDistrictName";
            this.tbDistrictName.Size = new System.Drawing.Size(198, 27);
            this.tbDistrictName.TabIndex = 4;
            this.tbDistrictName.TextChanged += new System.EventHandler(this.tbDistrictName_TextChanged);
            // 
            // cmbParentDistrict
            // 
            this.cmbParentDistrict.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbParentDistrict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbParentDistrict.FormattingEnabled = true;
            this.cmbParentDistrict.Location = new System.Drawing.Point(94, 25);
            this.cmbParentDistrict.Name = "cmbParentDistrict";
            this.cmbParentDistrict.Size = new System.Drawing.Size(198, 28);
            this.cmbParentDistrict.TabIndex = 5;
            this.cmbParentDistrict.Visible = false;
            // 
            // tbDistrictCode
            // 
            this.tbDistrictCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDistrictCode.Location = new System.Drawing.Point(94, 104);
            this.tbDistrictCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDistrictCode.Name = "tbDistrictCode";
            this.tbDistrictCode.Size = new System.Drawing.Size(198, 27);
            this.tbDistrictCode.TabIndex = 4;
            this.tbDistrictCode.TextChanged += new System.EventHandler(this.tbDistrictCode_TextChanged);
            // 
            // tbDistrictIP
            // 
            this.tbDistrictIP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDistrictIP.Location = new System.Drawing.Point(94, 143);
            this.tbDistrictIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDistrictIP.Name = "tbDistrictIP";
            this.tbDistrictIP.Size = new System.Drawing.Size(198, 27);
            this.tbDistrictIP.TabIndex = 4;
            this.tbDistrictIP.TextChanged += new System.EventHandler(this.tbDistrictIP_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(144, 238);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 32);
            this.button1.TabIndex = 6;
            this.button1.Text = "恢复原值";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 185);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 7;
            this.label5.Text = "顺序:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(64, 4);
            // 
            // tbSeq
            // 
            this.tbSeq.Location = new System.Drawing.Point(94, 182);
            this.tbSeq.Name = "tbSeq";
            this.tbSeq.Size = new System.Drawing.Size(198, 27);
            this.tbSeq.TabIndex = 9;
            // 
            // DistrictDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.ClientSize = new System.Drawing.Size(492, 282);
            this.Controls.Add(this.tbSeq);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbParentDistrict);
            this.Controls.Add(this.tbDistrictIP);
            this.Controls.Add(this.tbDistrictCode);
            this.Controls.Add(this.tbDistrictName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DistrictDetail";
            this.Load += new System.EventHandler(this.DistrictDetail_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDistrictName;
        private System.Windows.Forms.ComboBox cmbParentDistrict;
        private System.Windows.Forms.TextBox tbDistrictCode;
        private System.Windows.Forms.TextBox tbDistrictIP;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox tbSeq;
    }
}
