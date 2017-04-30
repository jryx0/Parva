namespace BigData.JW.UI.WinForm 
{
    partial class DataImporter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataImporter));
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDelFile = new System.Windows.Forms.Button();
            this.btnDataReprot = new System.Windows.Forms.Button();
            this.btnItemFormat = new System.Windows.Forms.Button();
            this.btnItemEdit = new System.Windows.Forms.Button();
            this.treeImageList = new System.Windows.Forms.ImageList(this.components);
            this.lbInfo = new System.Windows.Forms.Label();
            this.btnImportFile = new System.Windows.Forms.Button();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dataImpTreeView = new BigData.JW.UI.WinForm.DataImporterHelper();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(1040, 505);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 35);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDelFile
            // 
            this.btnDelFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelFile.Enabled = false;
            this.btnDelFile.Location = new System.Drawing.Point(451, 505);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(83, 35);
            this.btnDelFile.TabIndex = 2;
            this.btnDelFile.Text = "删除文件";
            this.btnDelFile.UseVisualStyleBackColor = true;
            this.btnDelFile.Click += new System.EventHandler(this.btnDelFile_Click);
            // 
            // btnDataReprot
            // 
            this.btnDataReprot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDataReprot.Location = new System.Drawing.Point(344, 505);
            this.btnDataReprot.Name = "btnDataReprot";
            this.btnDataReprot.Size = new System.Drawing.Size(83, 35);
            this.btnDataReprot.TabIndex = 2;
            this.btnDataReprot.Text = "数据统计";
            this.btnDataReprot.UseVisualStyleBackColor = true;
            this.btnDataReprot.Click += new System.EventHandler(this.btnDataReprot_Click);
            // 
            // btnItemFormat
            // 
            this.btnItemFormat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemFormat.Enabled = false;
            this.btnItemFormat.Location = new System.Drawing.Point(125, 505);
            this.btnItemFormat.Name = "btnItemFormat";
            this.btnItemFormat.Size = new System.Drawing.Size(83, 35);
            this.btnItemFormat.TabIndex = 2;
            this.btnItemFormat.Text = "编辑格式";
            this.btnItemFormat.UseVisualStyleBackColor = true;
            this.btnItemFormat.Click += new System.EventHandler(this.btnItemFormat_Click);
            // 
            // btnItemEdit
            // 
            this.btnItemEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnItemEdit.Location = new System.Drawing.Point(21, 505);
            this.btnItemEdit.Name = "btnItemEdit";
            this.btnItemEdit.Size = new System.Drawing.Size(83, 35);
            this.btnItemEdit.TabIndex = 2;
            this.btnItemEdit.Text = "编辑项目";
            this.btnItemEdit.UseVisualStyleBackColor = true;
            this.btnItemEdit.Click += new System.EventHandler(this.btnItemEdit_Click);
            // 
            // treeImageList
            // 
            this.treeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImageList.ImageStream")));
            this.treeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImageList.Images.SetKeyName(0, "project.png");
            this.treeImageList.Images.SetKeyName(1, "wenjian.png");
            this.treeImageList.Images.SetKeyName(2, "Floder.png");
            // 
            // lbInfo
            // 
            this.lbInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbInfo.AutoEllipsis = true;
            this.lbInfo.Location = new System.Drawing.Point(2, 470);
            this.lbInfo.Name = "lbInfo";
            this.lbInfo.Size = new System.Drawing.Size(1132, 24);
            this.lbInfo.TabIndex = 3;
            this.lbInfo.Text = "label1";
            this.lbInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnImportFile
            // 
            this.btnImportFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportFile.Enabled = false;
            this.btnImportFile.Location = new System.Drawing.Point(552, 505);
            this.btnImportFile.Name = "btnImportFile";
            this.btnImportFile.Size = new System.Drawing.Size(83, 35);
            this.btnImportFile.TabIndex = 2;
            this.btnImportFile.Text = "导入文件";
            this.btnImportFile.UseVisualStyleBackColor = true;
            this.btnImportFile.Click += new System.EventHandler(this.btnImportFile_Click);
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenDir.Location = new System.Drawing.Point(227, 505);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(83, 35);
            this.btnOpenDir.TabIndex = 2;
            this.btnOpenDir.Text = "打开目录";
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.btnOpenDir_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(883, 505);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 35);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dataImpTreeView
            // 
            this.dataImpTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataImpTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dataImpTreeView.Location = new System.Drawing.Point(2, 3);
            this.dataImpTreeView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataImpTreeView.Name = "dataImpTreeView";
            this.dataImpTreeView.Size = new System.Drawing.Size(1132, 462);
            this.dataImpTreeView.TabIndex = 4;
            this.dataImpTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dataImpTreeView_AfterSelect);
            this.dataImpTreeView.LogInfoEvent += new System.EventHandler(this.dataImpTreeView_LogInfoEvent);
            // 
            // DataImporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 548);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dataImpTreeView);
            this.Controls.Add(this.lbInfo);
            this.Controls.Add(this.btnItemEdit);
            this.Controls.Add(this.btnItemFormat);
            this.Controls.Add(this.btnDataReprot);
            this.Controls.Add(this.btnOpenDir);
            this.Controls.Add(this.btnImportFile);
            this.Controls.Add(this.btnDelFile);
            this.Controls.Add(this.btnExit);
            this.Font = new System.Drawing.Font("微软雅黑", 10.18868F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "DataImporter";
            this.Text = "DataImporter";
            this.ResumeLayout(false);

        }


        #endregion

      
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.Button btnDataReprot;
        private System.Windows.Forms.Button btnItemFormat;
        private System.Windows.Forms.Button btnItemEdit;
        private System.Windows.Forms.ImageList treeImageList;
        private System.Windows.Forms.Label lbInfo;
        private System.Windows.Forms.Button btnImportFile;
        private System.Windows.Forms.Button btnOpenDir;
        private DataImporterHelper dataImpTreeView;
        private System.Windows.Forms.Button btnSave;
    }
}