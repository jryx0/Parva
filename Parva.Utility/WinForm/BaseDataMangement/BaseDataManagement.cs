using Parva.Application.Core;
using Parva.Application.Services;
using Parva.Application.Services.MasterDetail;
using Parva.Domain.Models;
using Parva.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Parva.WinForm.BaseData
{
    public partial class BaseDataManagement : Form
    {
        private int maxtypeid = 0;
        private int maxitemid = 0;

        private IMasterDetailService<BaseDataType> _typeService;

        private List<BaseDataType> ModifiedList;

        public BaseDataManagement(IMasterDetailService<BaseDataType> typeservice)
        {
            this._typeService = typeservice;
            ModifiedList = new List<BaseDataType>();
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }

        protected override void OnLoad(EventArgs e)
        {             
            initGridView();
            this.Cursor = Cursors.Arrow;

            base.OnLoad(e);
        }

        public void initGridView()
        {
            //dgvMaster.DataSource = null;
            //dgvDetail.DataSource = null;

            //var _basetypeList = this._typeService.GetMaster();//(x => true);              

            //var dl = from bt in _basetypeList
            //         orderby bt.Seq
            //         select new  
            //         {
            //             basetypeid = bt.Id,
            //             项目名称 = bt.BaseTypeName,
            //             备注 = bt.Comment,
            //             顺序 = bt.Seq,
            //             状态 = bt.Status,                         
            //             修改人 = bt.LastModifier,
            //             修改时间 = bt.LastModifyDate
            //         };

            //if (dl != null && dl.Count() > 0)
            //    maxtypeid = dl.Max(x => x.basetypeid);

            //var il = from i in _basetypeList.Where(x => x.HaveValue != null).SelectMany(x => x.HaveValue)
            //         orderby i.DataParent.Seq, i.Seq
            //         select new
            //         {
            //             ItemId = i.Id,
            //             ParentId = i.DataParent.Id,
            //             值名称 = i.Name,
            //             值 = i.Value,
            //             值类型 = i.ValueType,
            //             备注 = i.Comment,
            //             顺序 = i.Seq,
            //             状态 = i.Status,
            //             创建人 = i.LastModifier,
            //             创建时间 = i.LastModifyDate
            //         };

            //if (il != null && il.Count() > 0)
            //    maxitemid = il.Max(x => x.ItemId);

            //var tblDataSet = new DataSet();
            //var tblBaseType = dl.CopyToDT();
            //tblBaseType.TableName = "Master";
            //var tblItemType = il.CopyToDT();
            //tblItemType.TableName = "Details1";

            //tblDataSet.Tables.Add(tblBaseType);
            //tblDataSet.Tables.Add(tblItemType);

            //tblDataSet.Relations.Add("BaseItemRelation",
            //    tblBaseType.Columns["basetypeid"], tblItemType.Columns["ParentId"]);


            //BindingSource bsBaseType = new BindingSource();
            //bsBaseType.DataSource = tblDataSet;
            //bsBaseType.DataMember = "BaseType";


            //BindingSource bsItemType = new BindingSource();
            //bsItemType.DataSource = bsBaseType;
            //bsItemType.DataMember = "BaseItemRelation";


            //dgvMaster.DataSource = bsBaseType;
            //dgvMaster.Columns[0].Visible = false;

            //dgvDetail.DataSource = bsItemType;
            //dgvDetail.Columns[0].Visible = false;
            //dgvDetail.Columns[1].Visible = false;
        }  

        //处理右键菜单
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dg = (DataGridView)sender;

            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >=0)
                {
                    //若行已是选中状态就不再进行设置
                    if (dg.Rows[e.RowIndex].Selected == false)
                    {
                        dg.ClearSelection();
                        dg.Rows[e.RowIndex].Selected = true;
                    }
                    //只选中一行时设置活动单元格
                    if (dg.SelectedRows.Count == 1)
                    {
                        dg.CurrentCell = dg.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    contextMenuStrip1.Tag = dg;
                    //弹出操作菜单
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);                     
                }
            }
        }
 
        //gridview1 项目删除
        private void Del_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dg = contextMenuStrip1.Tag as DataGridView;
            if (dg == null) return;

            String WarnningString = "将删除项目和所有类型";
            if (dg.Name == "dataGridView2")
                WarnningString = "将删除类型";

            if (MessageBox.Show(WarnningString, "删除警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                DataGridViewTools.DelGridData(dg);
        }

        //保存按钮
        private void button3_Click(object sender, EventArgs e)
        {
            //SavaGridData(dataGridView1); 
            this.Cursor = Cursors.WaitCursor;
            ChangeGridData();
            this.Cursor = Cursors.Arrow;

            var ds = (DataSet)((BindingSource)(dgvMaster.DataSource)).DataSource;
            var masterDT = ds.Tables["Master"];
            var detailsDT1 = ds.Tables["Details1"];

        }

        //保存数据
        public void ChangeGridData()
        {
            //var ds = (DataSet)((BindingSource)(dataGridView1.DataSource)).DataSource;
            //var bdt = ds.Tables["BaseType"];
            //var idt = ds.Tables["ItemType"];
            //var idts = ds.Relations[0];
            
            //ModifiedList = this._typeService.GetBaseDataType(x => 1 == 1);


            ///**
            // *  删除              修改              新增
            // * 1.删除子表       1.修改子表       1.新增主表
            // * 2.删除主表       2.修改主表       2.新增子表
            // */



            //SaveChanges(bdt, idt, idts);




            //ds.AcceptChanges();
            //initGridView();
        }


        private void SaveChanges(DataTable typeDT, DataTable valueDT, DataRelation relation)
        {
            ModifiedList.Clear();            
            
            //主表修改删除
            //var typechangeDT = typeDT.GetChanges(DataRowState.Modified);
            //var typechangelist = this._typeService.MapMaster(typechangeDT.Rows);


            ////主表新增
            //var newtypeDT = typeDT.GetChanges(DataRowState.Added);
            //var newtypelist = this._typeService.MapBaseDataType(newtypeDT.Rows);
            //ModifiedList.ToList().AddRange(newtypelist);




            ////子表增删改
            //var valuechangeDT = valueDT.GetChanges(DataRowState.Deleted | DataRowState.Modified | DataRowState.Added);
            //var valuechangelist = this._typeService.MapDataValue(valuechangeDT.Rows);


            //typechangelist.ForEach(x =>
            //{
            //    var v = valuechangelist.Where(y => y.BaseDataTypeId == x.Id);
            //    if (x.HaveValue == null)
            //        x.HaveValue = new List<DataValue>();
            //    x.HaveValue.AddRange(v);
            //});


            //ModifiedList.ToList().ForEach(x => x = typechangelist.Find(y => y.Id == x.Id));

            //_typeService.SaveChanges(ModifiedList);
        }
        


        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var dg = (DataGridView)sender;

            if (e.RowIndex < 0 || e.RowIndex >= dg.Rows.Count || e.ColumnIndex < 0 || e.ColumnIndex >= dg.Columns.Count)
                return;

            MessageBox.Show("格式错误 row = " + e.RowIndex.ToString() + "col = " + e.ColumnIndex.ToString());

            //dg.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Red;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dg = (DataGridView)sender;
            dg.CurrentRow.Cells[0].Value = ++maxtypeid;
        }
    }
    
}
