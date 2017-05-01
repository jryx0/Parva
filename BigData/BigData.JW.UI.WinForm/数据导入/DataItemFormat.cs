using BigData.JW.Services;
using BigData.JW.Models;
using Parva.Application;
using Parva.Application.Services.MasterDetail;
using Parva.Utility.WinForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Parva.Application.Core;
using Parva.Domain.Models;
using Parva.Application.Services;
using Parva.Domain.Core;
using Parva.Utility.Tools;

namespace BigData.JW.UI.WinForm 
{
    public partial class DataItemFormat : ParvaNodeDetail<CompareItem>
    {
        private ItemFormatServcie _formatService;
        private String _preFileName;
        private BaseDataType _basedatatype;
        private List<int> SelectedIndexList;

        private  BigDataMasterDetailService<ItemFormat, ItemDataFormat> _testformateService;

        private ItemFormat _currentFormat;

        public DataItemFormat()
        {
            InitializeComponent();
            _preFileName = String.Empty;

            _formatService = AppEngine.Container.GetInstance<ItemFormatServcie>();
            SelectedIndexList = new List<int>();


            _testformateService = AppEngine.Container.GetInstance<BigDataMasterDetailService<ItemFormat, ItemDataFormat>>();
            _testformateService.Find(null, x => x.DataFormats.AsQueryable(), y => y.ParentId);

        }

        public override void InitNodeDetail(CompareItem tag)
        {
            var bts = AppEngine.Container.GetInstance<BaseDataTypeService>();
            _basedatatype = bts.GetMaster(x => x.Id == 1).FirstOrDefault();//数据格式

            ColumnTypeName.DataSource = _basedatatype.HaveValue;
            ColumnTypeName.ValueMember = "Id";
            ColumnTypeName.DisplayMember = "Name";
            ColumnTypeName.Width = 150;
            ColumnValue.Width = 150;

            ColumnId.Visible = false;
            ColumnDataValueId.Visible = false;
            ColumnDataValueId.Visible = false;


            if (tag != null)
                _currentFormat = _currentDetail.Format;
            _currentDetail = tag;
        }

        public override void SetData(CompareItem argData)
        {
            if (argData == null) return;

            _currentDetail = argData;
            _currentFormat = argData.Format;
            if (_currentFormat == null)
            {//读数据库
                _currentFormat = _formatService.GetMaster(x => x.ParentId == argData.Id)?.FirstOrDefault();
                _currentDetail.Format = _currentFormat;
            }

            if (_currentFormat == null) return; //no Format    

            dgvFormat.DataSource = null;
            tbStartPos.Text = "";                 

            _currentFormat.ParentItem = _currentDetail;
            _currentFormat.ParentId = _currentDetail.Id;
            _currentFormat.DataFormats?.ForEach(x =>
                                                {
                                                    x.ColInfo = _basedatatype.HaveValue?
                                                                           .Where(y => y.Id == x.ColInfoId)
                                                                           .FirstOrDefault();
                                                    x.ParentId = _currentFormat.Id;
                                                    x.Parent = _currentFormat;
                                                });
            tbStartPos.Text = _currentFormat.StartPos.ToString();

            var list = from df in _currentFormat.DataFormats
                       select new
                       {
                           Id = df.Id,  //itemdataformat
                           值 = df.ColInfo.Name, //值
                           字段名 = df.ColInfo.Value, //字段名
                           值顺序 = df.ColInfo.Seq,     //值顺序
                           列顺序 = df.ColIndex,   //列顺序
                           dvid = df.ColInfo.Id,     //dataValue id
                           FormatId = df.ParentId,   //
                       };

            dgvFormat.AutoGenerateColumns = false;
            dgvFormat.DataSource = list.CopyToDT();

            ColumnId.DataPropertyName = "Id";
            ColumnTypeName.DataPropertyName = "dvid";
            ColumnValue.DataPropertyName = "字段名";
            ColumnSeq.DataPropertyName = "值顺序";
            ColumnColNumber.DataPropertyName = "列顺序";
            ColumnDataValueId.DataPropertyName = "dvid";
            ColumnItemDataFormat.DataPropertyName = "FormatId";



            SelectedIndexList.AddRange(list.Select(x => x.dvid));
        }

        public override bool SaveChanges(List<CompareItem> ChangeList)
        {
            // _formatService
            List<CompareItem> treeList = this.ParentTreeView.Tag as List<CompareItem>;

            var changlist = treeList.Select(x => x.Format).Where(x =>
                               {
                                   if (x.ModifyStatus != BaseEntityStatus.Unchanged)
                                       return true;
                                   else if (x.DataFormats != null)
                                   {
                                       var df = x.DataFormats.Find(y => y.ModifyStatus != BaseEntityStatus.Unchanged);
                                       if (df != null)
                                           return true;
                                   }
                                   return false;

                               });

            //_formatService.SaveChanges(changlist);
            return false;
        }

        public override bool SaveModify()
        {
            if (_currentFormat == null)
            {
                _currentFormat = new ItemFormat();
                _currentFormat.ModifyStatus = Parva.Domain.Core.BaseEntityStatus.NewEntity;
            }

            if (_currentFormat.Original == null)
            {
                _currentFormat.Original = new ItemFormat();
                _currentFormat.Original.Id = _currentFormat.Id;
                _currentFormat.Original.ModifyStatus = _currentFormat.ModifyStatus;
                _currentFormat.Original.ParentId = _currentFormat.ParentId;
                _currentFormat.Original.Seq = _currentFormat.Seq;
                _currentFormat.Original.StartPos = _currentFormat.StartPos;
                _currentFormat.Original.Status = _currentFormat.Status;
                _currentFormat.Original.ParentItem = _currentDetail;
                _currentFormat.Original.DataFormats = new List<ItemDataFormat>();

                if (_currentFormat.DataFormats != null)
                    _currentFormat.Original.DataFormats.AddRange(_currentFormat.DataFormats);
            }

            if (tbStartPos.Text.Length != 0)
                _currentFormat.StartPos = int.Parse(tbStartPos.Text);
            else _currentFormat.StartPos = 0;

            DataTable dt = (DataTable)dgvFormat.DataSource;
            if (dt == null)
                return true;

            var drs = dt.GetChanges();
            if (drs == null || drs.Rows == null) return true;
            foreach (DataRow dr in drs?.Rows)
            {   
                _currentFormat.ParentItem = _currentDetail;
                _currentFormat.ParentId = _currentDetail?.Id;


                if (_currentFormat.DataFormats == null)
                    _currentFormat.DataFormats = new List<ItemDataFormat>();

                ItemDataFormat idf = new ItemDataFormat();
                idf.ModifyStatus = dr.RowState == DataRowState.Added ? 
                                             BaseEntityStatus.NewEntity : dr.RowState == DataRowState.Deleted ?
                                             BaseEntityStatus.Deleted 
                                             : BaseEntityStatus.Modefied;

                idf.ParentId = _currentFormat.Id;
                idf.Parent = _currentFormat;

                if (dr[5] != null && dr[5].GetType() != typeof(DBNull))
                    idf.ColInfoId = (int)dr[5];
                else idf.ColInfoId = 0;
                if (dr[5] != null && dr[5].GetType() != typeof(DBNull))
                    idf.ColIndex = (int)dr[5];
                else idf.ColIndex = 0;

                _currentFormat.DataFormats.Add(idf);
            }

            return true;
        }

        #region 处理点击事件
        private void dgvFormat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //var ci = this.ParentTreeView.GetCurrentData();
            //if (ci == null) return;

            //switch (ci.DataType)
            //{
            //    case ItemType.SourceItem:
            //    case ItemType.CompareItem:
            //        ShowFileContent(ci);
            //        break;
            //    case ItemType.Compare:
            //    case ItemType.Source:
            //    case ItemType.Folder:
            //        //   ShowFileList(ci);
            //        break;
            //}
        }
  
        private void ShowFileContent(CompareItem ci)
        {
            if (String.IsNullOrEmpty(_preFileName)) return;

            if (tcShowFile.SelectedIndex == 0)
                ShowFormattedFile(ci, _preFileName);
            else ShowOrginalFile(ci, _preFileName);
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
        #endregion

        #region 处理下拉框和输入列号
        private void dgvFormat_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //实现单击一次显示下拉列表框
            //if (dgvFormat.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn && e.RowIndex != -1)
            //{
            //    SendKeys.Send("{F4}");
            //}
            //else 
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                SendKeys.Send("{F2}");
            }
        }
        private void dgvFormat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;     // Header
            }
            if (e.ColumnIndex != 1)
            {
                return;     // Filter out other columns
            }

            dgvFormat.BeginEdit(true);
            ComboBox comboBox = (ComboBox)dgvFormat.EditingControl;
            comboBox.DroppedDown = true;
        }

        private void dgvFormat_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //判断要处理的DataGridViewComboBoxColumn名称，若符合条件，编辑控件被强制转换为ComboBox以处理，添加SelectedIndexChanged事件
            if (this.dgvFormat.CurrentCell.OwningColumn.Name == "ColumnTypeName" && dgvFormat.CurrentCell.RowIndex != -1)
            {
                System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)e.Control;
                // if (!isBind)
                {
                    cb.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                    cb.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
                    
                    cb.DrawItem -= new System.Windows.Forms.DrawItemEventHandler(this.Combobox_DrawItem);
                    cb.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Combobox_DrawItem);
                    cb.DrawMode = DrawMode.OwnerDrawFixed;
                   
                   // isBind = true;
                }
            }
           // else isBind = false;

            if (this.dgvFormat.CurrentCell.OwningColumn.Name == "ColumnColNumber" && dgvFormat.CurrentCell.RowIndex != -1)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(TextBox_KeyPress);
                e.Control.KeyPress += new KeyPressEventHandler(TextBox_KeyPress);
            }
        }

        private void Combobox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;

            ComboBox cb = sender as ComboBox;
            var dv = cb.Items[e.Index] as DataValue;      
            
            if (SelectedIndexList.FindIndex(x => x == dv.Id) != -1)
            {                
                e.Graphics.DrawString(dv.Name, Font, Brushes.LightGray, e.Bounds);
            }
            else
            {
                e.DrawBackground();

                Graphics g = e.Graphics;
                Brush brush = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ?
                              Brushes.DarkBlue : new SolidBrush(e.BackColor);

                g.FillRectangle(brush, e.Bounds);
                e.Graphics.DrawString(dv.Name, e.Font,
                         new SolidBrush(e.ForeColor), e.Bounds, StringFormat.GenericDefault);

                e.DrawFocusRectangle();
            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                var tb = sender as TextBox;
                if (tb.Text.Length == 0 && e.KeyChar == '0') e.Handled = true;
                else if (tb.Text.Length > 1) e.Handled = true;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.ComboBox cb = (System.Windows.Forms.ComboBox)sender;
            
            if (cb.SelectedValue != null && cb.SelectedValue.GetType() == typeof(int) &&SelectedIndexList?.FindIndex(x => x == (int)cb.SelectedValue) != -1)
            {
                cb.SelectedIndex = -1;
            }

            if (cb.SelectedIndex > -1 && cb.SelectedValue != null && cb.SelectedValue.ToString() != "System.Data.DataRowView" && cb.SelectedValue.GetType() == typeof(int))
            {
                if (_basedatatype != null && _basedatatype.HaveValue != null)
                {
                    var dv = _basedatatype.HaveValue.Find(x => x.Id == (int)cb.SelectedValue);

                    // dgvFormat.Rows[cb.SelectedIndex].Cells[]
                    dgvFormat.CurrentRow.Cells["ColumnValue"].Value = dv.Value;
                    dgvFormat.CurrentRow.Cells["ColumnSeq"].Value = dv.Seq;

                    //var predv = dgvFormat.CurrentRow.Tag as DataValue;
                    //if(predv != null)
                    //    SelectedIndexList.Remove(predv.Id);
                    //SelectedIndexList.Add((int)cb.SelectedValue);
                    //dgvFormat.CurrentRow.Tag = dv;

                    GetTabValue();
                    SelectedIndexList.Add((int)cb.SelectedValue);
                }

                //if (isBind)
                //{
                //    cb.SelectedIndexChanged -= new EventHandler(ComboBox_SelectedIndexChanged);
                //    isBind = false;
                //}
            }
        }


        private void GetTabValue()
        {
            SelectedIndexList.Clear();
            foreach (DataGridViewRow dr in dgvFormat.Rows)
            {
                var dc = dr.Cells[1] as DataGridViewComboBoxCell;
                if (dc.Value != null &&  dc.Value.GetType() != typeof(DBNull))
                    SelectedIndexList.Add((int)dc.Value);
            }
        }

        #endregion

        private void dgvFormat_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
        
        private void btnPreView_Click(object sender, EventArgs e)
        {

        }

        private void tbStartPos_TextChanged(object sender, EventArgs e)
        {
            if (_currentFormat != null)
                this._currentFormat.ModifyStatus = BaseEntityStatus.Modefied;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }
    }
}
