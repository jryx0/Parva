using BigData.JW.Models;
using Parva.Utility.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows.Forms;

namespace BigData.JW.UI.WinForm 
{
    public partial class DisplayData : ParvaNodeDetail<CompareItem>
    {       
        public DisplayData()
        {
            InitializeComponent();
        }

        public override bool AllowEdit()
        {
            return false;
        }
        public override void InitNodeDetail(CompareItem tag)
        {

        }

        #region 操作
        public override void NodeDetailOperation(object sender, EventArgs e)
        {
            var pe = e as ParvaEventArg;
            if (pe.ArgType == ParvaTreeViewEnum.Operation)
                switch (pe.EventName)
                {
                    case "DeleteFile":
                        DeleteFile(sender as CompareItem);
                        break;
                    case "ImportFile":
                        ImportFile(sender as CompareItem);
                        break;
                    case "OpenDirectory":
                        OpenDirectry(sender as CompareItem);
                        break;
                }
        }

        //设置数据显示
        public override void SetData(CompareItem argData)
        {
            if (argData == null) return;
            switch (argData.DataType)
            {
                case ItemType.SourceItem:
                case ItemType.CompareItem:
                    ShowSelectData(argData);
                    break;
                default:
                    ShowSelectItem(argData);
                    break;
            }

            this.Tag = argData;

        }
        //显示文件清单
        private void ShowSelectData(CompareItem ci)
        {
            if (ci == null)
                return;

            var path = GetNodePath(ci, x => x.Path, "\\");
            string dir = GlobalEnviroment.WorkDir + path + "\\";
            Utility.MakeSureDirectory(dir); //creat dir if it is not exit.

            if (!Directory.Exists(GlobalEnviroment.WorkDir + path))//Properties.Settings.Default.WorkDir))
            {
                MessageBox.Show("输入目录错误, 请重新设置输入目录!\r\nName=" + ci.Name + " ,Path=" + ci.Path);
                return;
            }

            this.dgvFiles.DataSource = null;
            this.dgvFiles.Columns.Clear();

            this.dgvPreView.DataSource = null;
            this.dgvPreView.Columns.Clear();

            this.dgvOriginalView.DataSource = null;
            this.dgvOriginalView.Columns.Clear();

            //SelectFileName = "";
           

            //init gridview read dir files
            this.dgvFiles.Columns.Add("FileName", "文件名");
            this.dgvFiles.Columns[0].Width = 380;
            this.dgvFiles.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

            this.dgvFiles.Columns.Add("Size", "大小");
            this.dgvFiles.Columns[1].Width = 100;

            this.dgvFiles.Columns.Add("date", "日期");
            this.dgvFiles.Columns[2].Width = 150;

            this.dgvFiles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DirectoryInfo folder = new DirectoryInfo(dir);
            int index = 0;
            foreach (FileInfo file in folder.GetFiles("*.xls"))
            {
                index = this.dgvFiles.Rows.Add();
                this.dgvFiles.Rows[index].Cells[0].Value = file.Name;
                this.dgvFiles.Rows[index].Cells[1].Value = file.Length;
                this.dgvFiles.Rows[index].Cells[2].Value = file.CreationTime;
            }

            foreach (FileInfo file in folder.GetFiles("*.csv"))
            {
                index = this.dgvFiles.Rows.Add();
                this.dgvFiles.Rows[index].Cells[0].Value = file.Name;
                this.dgvFiles.Rows[index].Cells[1].Value = file.Length;
                this.dgvFiles.Rows[index].Cells[2].Value = file.CreationTime;
            }
        }
        //显示文件列表
        private void ShowSelectItem(CompareItem ci)
        {
            if (ci == null)
                return;

            var path = GetNodePath(ci, x => x.Path, "\\");
           

            this.dgvFiles.DataSource = null;
            this.dgvFiles.Columns.Clear();

            this.dgvPreView.DataSource = null;
            this.dgvPreView.Columns.Clear();

            this.dgvOriginalView.DataSource = null;
            this.dgvOriginalView.Columns.Clear();

            if (ci.DataType == ItemType.Folder)
            {

            }
            else if(ci.DataType == ItemType.Compare || ci.DataType == ItemType.Source)
            {


            }
            else if(ci.DataType == ItemType.Root)
            {
                this.dgvFiles.Columns.Add("", "输入目录");
                this.dgvFiles.Columns[0].Width = 600;
                this.dgvFiles.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
                this.dgvFiles.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                int index = this.dgvFiles.Rows.Add();

                string dir = GlobalEnviroment.WorkDir + path + "\\";
                this.dgvFiles.Rows[index].Cells[0].Value = dir;
            }


            
        }
        //打开目录
        private void OpenDirectry(CompareItem ci)
        {
            if (ci == null) return;
            var path = GlobalEnviroment.WorkDir + GetNodePath(ci, x => x.Path, "\\");
            if (Directory.Exists(path))
            {
                ProcessStartInfo proc = new ProcessStartInfo("explorer.exe", path);
                Process.Start(proc);
            }

        }
        //删除文件
        private void DeleteFile(CompareItem ci)
        {
            if (ci == null || (ci.DataType != ItemType.CompareItem && ci.DataType != ItemType.SourceItem))
            {
                MessageBox.Show("请选择源数据或比对数据项目。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (this.dgvFiles.SelectedRows.Count <= 0)
                return;

            if (MessageBox.Show("确认删除文件吗?", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                foreach (DataGridViewRow r in this.dgvFiles.SelectedRows)
                {
                    string selectedfilename = GlobalEnviroment.WorkDir + GetNodePath(ci, x => x.Path, "\\") + "\\" + r.Cells[0].Value.ToString();
                    if (File.Exists(selectedfilename))
                        try
                        {
                            File.Delete(selectedfilename);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                }
                ShowSelectData(ci);
                this.ParentTreeView.LogInfo("删除成功！", null);
            }
        }
        //导入文件
        private void ImportFile(CompareItem ci)
        {
            if (ci == null || (ci.DataType != ItemType.CompareItem && ci.DataType != ItemType.SourceItem))
            {
                MessageBox.Show("请选择源数据或比对数据项目。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OpenFileDialog of = new OpenFileDialog();
            of.Multiselect = true;
            of.Filter = "Excel文件(*.xls;*.csv)|*.xls;*.csv";
            if (of.ShowDialog() == DialogResult.OK)
            {
                string targetDir = GlobalEnviroment.WorkDir + GetNodePath(ci, x => x.Path, "\\");

                Cursor _preCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
                foreach (string fn in of.FileNames)
                {
                    try
                    {
                        string targetFile = targetDir + "\\" + Path.GetFileName(fn);
                        File.Copy(fn, targetFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                this.SetData(ci);
                this.Cursor = _preCursor;

                this.ParentTreeView.LogInfo("导入成功！", null);
            }

        }
        #endregion

        #region datagridview select row
        private void dgvFiles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var ci = this.ParentTreeView.GetCurrentData();
            if (ci == null) return;

            switch (ci.DataType)
            {
                case ItemType.SourceItem:
                case ItemType.CompareItem:
                    ShowFileContent(ci);
                    break;
                case ItemType.Compare:
                case ItemType.Source:
                case ItemType.Folder:
                    //   ShowFileList(ci);
                    break;
            }
        }

        private void ShowFileList(CompareItem ci)
        {
            throw new NotImplementedException();
        }

    

        #endregion

        private void dgvFiles_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void tcShowFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ci = this.ParentTreeView.GetCurrentData();
            if (ci == null) return;

            if (ci.DataType != ItemType.CompareItem && ci.DataType != ItemType.SourceItem)
                return;

            ShowFileContent(ci);
        }

        private void ShowFileContent(CompareItem ci)
        {
            if (this.dgvFiles.SelectedRows.Count == 0) return;
            DataGridViewRow r = this.dgvFiles.SelectedRows[0];
            if (r == null) return;

            string selectedfilename = GlobalEnviroment.WorkDir + GetNodePath(ci, x => x.Path, "\\") + "\\" + r.Cells[0].Value.ToString();

            if (tcShowFile.SelectedIndex == 0)
                ShowFormattedFile(ci, selectedfilename);
            else ShowOrginalFile(ci, selectedfilename);
        }

        private void ShowOrginalFile(CompareItem ci, string FileName)
        {
            string showfilename = dgvOriginalView.Tag == null ? "" : dgvOriginalView.Tag.ToString();
            //判断选择的文件是否发生变化，避免反复显示同一文件
            if (showfilename == FileName) return;
            dgvOriginalView.Tag = FileName;

            //Fill datagridview            
            if (File.Exists(FileName))
            {
                this.Cursor = Cursors.WaitCursor;
                if (Path.GetExtension(FileName) == ".xls")
                    this.dgvOriginalView.DataSource = Parva.Utility.Tools.ExcelUtility.ReadXLSToDataTable(FileName, 500);
                else
                    this.dgvOriginalView.DataSource = Parva.Utility.Tools.ExcelUtility.ReadCSVToDataTable(FileName, 500);
                this.Cursor = Cursors.Arrow;
            }
        }

        private void ShowFormattedFile(CompareItem ci, String FileName)
        {
            string showfilename = dgvPreView.Tag == null ? "" : dgvPreView.Tag.ToString();
            //判断选择的文件是否发生变化，避免反复显示同一文件
            if (showfilename == FileName) return;
            dgvPreView.Tag = FileName;

            //Fill datagridview            
            if (File.Exists(FileName))
            {
                this.Cursor = Cursors.WaitCursor;
                                   
                    //if (Path.GetExtension(selectedfilename) == ".xls")
                    //    dt = Parva.Utility.Tools.ExcelUtility.ReadXLSToDataTable(selectedfilename, 500);
                    //else
                    //    dt = Parva.Utility.Tools.ExcelUtility.ReadCSVToDataTable(selectedfilename, 500);
                    
                 
                this.Cursor = Cursors.Arrow;
            }
        }

        private void ImportFileMenuItem_Click(object sender, EventArgs e)
        {
            var ci = this.ParentTreeView.GetCurrentData();

            ImportFile(ci);
        }

        private void DeletFileMenuItem_Click(object sender, EventArgs e)
        {
            var ci = this.ParentTreeView.GetCurrentData();
            DeleteFile(ci);
        }

        private void dgvFiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var info = dgvFiles.HitTest(e.X, e.Y);

                if (info.RowIndex < 0)
                    cmsDisplayData.Items[1].Enabled = false;
                else cmsDisplayData.Items[1].Enabled = true; 

                var ci = this.ParentTreeView.GetCurrentData();
                if(ci.DataType == ItemType.CompareItem || ci.DataType == ItemType.SourceItem)                 
                    cmsDisplayData.Items[0].Enabled = true;                 
                else
                    cmsDisplayData.Items[0].Enabled = false;

                cmsDisplayData.Show(this, e.Location);
            }
        }
    }
}
