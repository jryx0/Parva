using Parva.Application.Core;
using Parva.Application.Services.MasterDetail;
using Parva.Domain.Core;
using Parva.Domain.Models;
using Parva.Utility.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Parva.Utility.WinForm
{
    public abstract partial class MasterDetailView<T> : Form where T : BaseEntity
    {
        private BindingSource _masterBindingSource;

        protected DataSet _masterDetailDataSet;
        protected IMasterDetailService<T> _masterService;
        protected Dictionary<string, DataGridView> _detailView;
        private Dictionary<string ,int> maxIds = new Dictionary<string, int>();

        public MasterDetailView(IMasterDetailService<T> masterservice)
        {
            _masterService = masterservice;

            _masterDetailDataSet = new DataSet();
            _masterBindingSource = new BindingSource();
            _detailView = new Dictionary<string, DataGridView>();
 
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            InitMasterView();
            InitPageView();
            this.Cursor = Cursors.Arrow;

            base.OnLoad(e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        protected virtual void InitMasterView()
        {
            this.dgvMaster.DataSource = null;
            _masterDetailDataSet.Tables.Add(_masterService.GetMasterTable().Copy());

            this.dgvMaster.DataSource = _masterBindingSource;
            _masterBindingSource.DataSource = _masterDetailDataSet;
            _masterBindingSource.DataMember = _masterService.Name;

            var maxRowId = _masterDetailDataSet.Tables[_masterService.Name].Compute("Max(Id)", "true");
            if (DBNull.Value.Equals(maxRowId))
                maxIds[dgvMaster.Name] = 0;
            else maxIds[dgvMaster.Name] = Convert.ToInt32(maxRowId);
 
            InitDisplayCol(this.dgvMaster, _masterService.Name);
        }

        private void InitPageView()
        {
            foreach (var d in _masterService.GetDetailInfo())
            {
                var dt = _masterService.GetDetailTable(d.Key);

                AddDetailView(d.Key, dt);
            }
        }

        protected abstract DataRelation CreateRelation(string detailName, string tableName);
        

        /// <summary>
        /// 增加Tab页
        /// </summary>
        /// <param name="DetailName">Detail的显示名称</param>
        /// <param name="detaildataTable">Detail数据</param>
        protected virtual void AddDetailView(string DetailName, DataTable detaildataTable)
        {
            _masterDetailDataSet.Tables.Add(detaildataTable.Copy());

            //Create Relation
             DataRelation relation = CreateRelation(DetailName, detaildataTable.TableName);
            _masterDetailDataSet.Relations.Add(relation);

            //Binding
            BindingSource detailBindingSource = new BindingSource();
            detailBindingSource.DataSource = _masterBindingSource; //bindingsource
            detailBindingSource.DataMember = _masterService.Name + DetailName;//detaildataTable.TableName; //关系名称
            
            //创建DataGridView
            DataGridView dgv;

            if (_detailView.Keys.Contains(DetailName))
            {
                dgv = _detailView[DetailName];
                dgv.DataSource = null;
            }
            else
            {
                dgv = new DataGridView();

                TabPage Page = new TabPage();
                Page.Name = detaildataTable.TableName;

                //设置Tab名称
                var attr = _masterService.GetCustomAttribute(DetailName);
                Page.Text = attr == null ? DetailName : attr.Name;
                Page.Controls.Add(dgv);

                //添加Page
                this.tabDetail.Controls.Add(Page);
            }

            dgv.Dock = DockStyle.Fill;
            dgv.Location = new System.Drawing.Point(0, 0);
            dgv.Name = DetailName;
            dgv.DataSource = detailBindingSource;
            dgv.RowPostPaint += dgvMaster_RowPostPaint;

            //保存GridView 和 Detail 对应关系
            _detailView[DetailName] = dgv;
            var maxRowId = _masterDetailDataSet.Tables[detaildataTable.TableName]
                 .Compute("Max(Id)", "true");
            if (DBNull.Value.Equals(maxRowId))
                maxIds[dgv.Name] = 0;
            else maxIds[dgv.Name] = Convert.ToInt32(maxRowId);
 

            //添加事件
            dgv.CellMouseClick += new DataGridViewCellMouseEventHandler(this.dgvMaster_CellMouseClick);
            InitDisplayCol(dgv, DetailName);
        }

        protected abstract void InitDisplayCol(DataGridView datagridView, string name);
          
        private List<T> GetChangeData()
        {
            List<T> changedData = new List<T>();
            foreach (var dgv in _detailView)
            {
                var relation = _masterDetailDataSet.Relations[_masterService.Name + dgv.Key];
                var parentDT = relation?.ParentTable;
                foreach (DataRow pr in parentDT?.Rows)
                {
                    DataRow[] childrow;
                    if (pr.RowState != DataRowState.Deleted)
                        childrow = pr.GetChildRows(_masterService.Name + dgv.Key)
                                         .Where(x => (
                                                       x.RowState == DataRowState.Added
                                                       || x.RowState == DataRowState.Modified
                                                       || x.RowState == DataRowState.Deleted)).ToArray();
                    else childrow = pr.GetChildRows(_masterService.Name + dgv.Key, DataRowVersion.Original);

                    if (pr.RowState == DataRowState.Unchanged &&
                        (childrow == null || childrow.Count() == 0))
                        continue;

                    T master = MapMaster(pr);
                    changedData.Add(master);

                    foreach (var cr in childrow)
                        MapDetail(master, dgv.Key, cr);
                }
            }

            return changedData;
        } 
      
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            var changedData = GetChangeData();           

            _masterService.SaveChanges(changedData);
            _masterDetailDataSet.AcceptChanges();
            this.Cursor = Cursors.Arrow;
        }
        
        protected void SetStatus(BaseEntity t, DataRow row)
        {
            switch (row.RowState)
            {
                case DataRowState.Added:
                    t.ModifyStatus = Domain.Core.BaseEntityStatus.NewEntity;
                    break;
                case DataRowState.Deleted:
                    t.ModifyStatus = Domain.Core.BaseEntityStatus.Deleted;
                    break;
                case DataRowState.Modified:
                    t.ModifyStatus = Domain.Core.BaseEntityStatus.Modefied;
                    break;
                case DataRowState.Unchanged:
                    t.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;
                    break;
                default:
                    t.ModifyStatus = Domain.Core.BaseEntityStatus.Unkonwn;
                    break;
            }
        }

        protected abstract void MapDetail(T master, string key, DataRow row);
        protected abstract void MapDetail(IEnumerable<T> masterList, string key, DataTable detailChangeDT);
        protected abstract IEnumerable<T> MapMaster(DataTable masterRows);
        protected abstract T MapMaster(DataRow row);

        private void dgvMaster_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var dg = (DataGridView)sender;

            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
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

        private void Del_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dg = contextMenuStrip1.Tag as DataGridView;
            if (dg == null) return;

            String WarnningString = "将删除项目和所有类型";
            if (dg.Name == "dgvMaster")
                WarnningString = "确认删除类型及其所有值?";

            if (MessageBox.Show(WarnningString, "删除警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk) == DialogResult.OK)
                DataGridViewTools.DelGridData(dg);
        }

        private void dgvMaster_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var dg = (DataGridView)sender;
            dg.CurrentRow.Cells[0].Value = ++maxIds[dg.Name];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            this._masterDetailDataSet = new DataSet();        
            InitMasterView();
            InitPageView();

            this.Cursor = Cursors.Arrow;
        }

        private void dgvMaster_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var dgv = sender as DataGridView;
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dgv.RowHeadersWidth - 4, e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                dgv.RowHeadersDefaultCellStyle.Font,
                rectangle,
                dgv.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
    }
}
