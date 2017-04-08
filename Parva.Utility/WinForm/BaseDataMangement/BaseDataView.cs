using Parva.Application.Services;
using Parva.Domain.Models;
using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Parva.Domain.Core;

namespace Parva.Utility.WinForm
{
    public class BaseDataView : MasterDetailView<BaseDataType>
    {
        public BaseDataView(BaseDataTypeService masterservice) : base(masterservice)
        {
            this.Text = "基础数据管理";
        }

        protected override void InitDisplayCol(DataGridView datagridView, string name)
        {
            if (datagridView == null)
                return;

            if (name.IndexOf("BaseType") != -1)
            {
                datagridView.Columns[0].Visible = false;
                datagridView.Columns[1].HeaderText = "项目名称";
                datagridView.Columns[2].HeaderText = "说明";               
                datagridView.Columns[3].HeaderText = "修改人";
                datagridView.Columns[4].HeaderText = "修改时间";
                datagridView.Columns[5].HeaderText = "顺序";
                datagridView.Columns[6].HeaderText = "状态";
            }
            else if (name.IndexOf("HaveValue") != -1)
            {
                datagridView.Columns[0].Visible = false;
                datagridView.Columns[1].Visible = false;
                datagridView.Columns[2].HeaderText = "名称";
                datagridView.Columns[3].HeaderText = "值";
                datagridView.Columns[4].HeaderText = "值类型";
                datagridView.Columns[5].HeaderText = "修改人";
                datagridView.Columns[6].HeaderText = "修改时间";
                datagridView.Columns[7].HeaderText = "说明";
                datagridView.Columns[8].HeaderText = "顺序";
                datagridView.Columns[9].HeaderText = "状态";
            }            
        }

        protected override IEnumerable<BaseDataType> MapMaster(DataTable masterRows)
        {
            if (masterRows == null) return null;

            List<BaseDataType> basetype = new List<BaseDataType>();
            foreach (System.Data.DataRow r in masterRows?.Rows)
                basetype.Add(MapMaster(r));

            return basetype;
        }
        protected override BaseDataType MapMaster(DataRow row)
        {
            BaseDataType bt = new BaseDataType();

            if (row.HasVersion(DataRowVersion.Original))
                bt.Id = Convert.ToInt32(row[0, DataRowVersion.Original]);

            SetStatus(bt, row);
            if (bt.ModifyStatus == Domain.Core.BaseEntityStatus.Deleted)
                return bt;

            bt.BaseTypeName = row.Field<String>(1);
            bt.Comment = row.Field<String>(2);
            bt.LastModifier = row.Field<String>(3);
            bt.LastModifyDate = DBNull.Value.Equals(row[4]) ? System.DateTime.Now : row.Field<DateTime>(4);
            bt.Seq = DBNull.Value.Equals(row[5]) ? 0 : row.Field<long>(5);
            bt.Status = DBNull.Value.Equals(row[6]) ? false : row.Field<Boolean>(6);

            return bt;
        }

        protected override void MapDetail(IEnumerable<BaseDataType> masterList, string key, DataTable detailChangeDT)
        {            
            if(key == "HaveValue")
            {
                List<DataValue> datavalue = new List<DataValue>();
                foreach(DataRow r in detailChangeDT.Rows)
                    datavalue.Add(MapDataValue(r));

                foreach(var dv in datavalue)
                {
                    var bt = masterList.Where(x => x.Id == dv.BaseDataTypeId).FirstOrDefault();
                    if (bt != null)
                    {
                        if (bt.HaveValue == null)
                            bt.HaveValue = new List<DataValue>();
                        bt.HaveValue.Add(dv);
                    }
                    //else
                    //{
                    //    bt = new BaseDataType();
                    //    if (dv.BaseDataTypeId.HasValue)
                    //    {
                    //        bt.ModifyStatus = Domain.Core.BaseEntityStatus.Unchanged;
                    //        bt.Id = dv.BaseDataTypeId.Value;
                    //    }
                    //    else
                    //    {
                    //        bt.ModifyStatus = Domain.Core.BaseEntityStatus.NewEntity;
                    //    }

                    //    bt.HaveValue = new List<DataValue>();
                    //    bt.HaveValue.Add(dv);
                    //    masterList.ToList().Add(bt);
                    //}
                }                      
            }
        }
        protected override void MapDetail(BaseDataType master, string key, DataRow row)
        {
            if (master == null)  return;

            if(key == "HaveValue")
            {
                if (master.HaveValue == null)
                    master.HaveValue = new List<DataValue>();                
                 
                //删除主表，子表全部删除
                if (master.ModifyStatus == BaseEntityStatus.Deleted)
                    row.Delete();

                master.HaveValue.Add(MapDataValue(row));
            }
        }
        private DataValue MapDataValue(DataRow row)
        {
            DataValue dv = new DataValue();

            if (row.HasVersion(DataRowVersion.Original))
            {
                dv.Id = Convert.ToInt32(row[0, DataRowVersion.Original]);
                dv.BaseDataTypeId = Convert.ToInt32(row[1, DataRowVersion.Original]);
            }

            SetStatus(dv, row);
            if (dv.ModifyStatus == BaseEntityStatus.Deleted)
                return dv;

            dv.Name = row.Field<String>(2);
            dv.Value = row.Field<String>(3);
            dv.ValueType = row.Field<String>(4);
            dv.LastModifier = row.Field<String>(5);
            dv.LastModifyDate = DBNull.Value.Equals(row[6]) ? System.DateTime.Now : row.Field<DateTime>(6);
            dv.Comment = row.Field<String>(7);
            dv.Seq = DBNull.Value.Equals(row[8]) ? 0 : row.Field<long>(8);
            dv.Status = DBNull.Value.Equals(row[9]) ? false : row.Field<Boolean>(9); 

            return dv;
        }

        protected override DataRelation CreateRelation(string detailName, string tablename)
        {
            if (detailName == "HaveValue") 
                //sqlite codefirt bug : 子表外键和主表主键类型不一致  
                return new DataRelation(_masterService.Name + detailName,
                            _masterDetailDataSet.Tables[_masterService.Name].Columns["Id"],
                            _masterDetailDataSet.Tables[tablename].Columns["BaseDataTypeId"]);

            throw new NotImplementedException();
        }
    }
}
