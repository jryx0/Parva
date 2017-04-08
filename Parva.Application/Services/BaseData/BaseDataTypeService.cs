using Parva.Application.Core;
using Parva.Domain.Models;
using Parva.Domain.Core;
using System.Linq;
using System;
using Parva.Application.Interfaces.Repository;
using System.Linq.Expressions;
using System.Data;
using System.Collections.Generic;

namespace Parva.Application.Services
{
    public class BaseDataTypeService : MasterDetail.MasterDetailService<BaseDataType>
    {
        public BaseDataTypeService(IBaseObjectService<BaseDataType> masterService) : base(masterService)
        {

        }

        public override DataTable GetDetailTable(string detailName)
        {
            if (!_detailServices.Keys.Contains(detailName))
                return null;

            var service = AppEngine.Container.GetInstance(typeof(IBaseObjectService<>).MakeGenericType(_detailServices[detailName]));

            if (detailName == "HaveValue")
            {
                var objectService = service as BaseObjectService<DataValue>;
                var detailTable = objectService.GetDataTable();

                detailTable.TableName = detailName;// objectService.Name;

                return detailTable;
            }

            return null;
        }

        public override void IncludeDetail(BaseDataType entity, string detailName)
        {
            throw new NotImplementedException();
        }

        public override void IncludeDetail(IQueryable<BaseDataType> entities, string detailName)
        {
            throw new NotImplementedException();
        }

        public override IQueryable<TDetail> GetDetail<TDetail>(string detailName)
        {
            throw new NotImplementedException();
        }

        protected override void SaveDetailChanges(List<BaseDataType> changeList, string detailName)
        {
            if (!_detailServices.Keys.Contains(detailName) || changeList == null || changeList.Count == 0)
                return;

            var service = AppEngine.Container.GetInstance(typeof(IBaseObjectService<>).MakeGenericType(_detailServices[detailName]));
            if (detailName == "HaveValue")
            {
                var objectService = service as BaseObjectService<DataValue>;

                //ToDo: 修改IoC实例适用范围
                objectService._systemRepo = this._masterService._systemRepo; // btService._systemRepo;

                var datavaluelist = changeList
                    .Where(x => x.HaveValue != null)
                    .SelectMany(x =>{
                                        foreach (var dv in x.HaveValue.Where(y => y.ModifyStatus == BaseEntityStatus.NewEntity))
                                            if (dv.ModifyStatus == BaseEntityStatus.NewEntity)
                                                dv.BaseDataTypeId = x.Id;
                                        return x.HaveValue;
                    })
                     .Where(x => x.ModifyStatus != (BaseEntityStatus.Unchanged | BaseEntityStatus.Unkonwn));


                if (datavaluelist != null && datavaluelist.Count() != 0)
                    objectService.SaveChanges(datavaluelist);

            }
        }
    }
}


/*
         //private IBaseObjectService<BaseDataType> _basedatatypeService;
        //private IBaseObjectService<DataValue> _datavalueService;

        //public BaseDataTypeService(IBaseObjectService<BaseDataType> baseDatatypeService, IBaseObjectService<DataValue> datavalueServie)
        //{
        //    _basedatatypeService = baseDatatypeService;
        //    _datavalueService = datavalueServie;
        //}

        //public IQueryable<BaseDataType> GetBaseDataType()
        //{
        //    return _basedatatypeService.Find(x => x.Status).OrderBy(x => x.Seq);
        //}

        //public IQueryable<DataValue> GetDataValue()
        //{
        //    return _datavalueService.Find(x => x.Status).OrderBy(x => x.Seq);
        //}

        //public IQueryable<BaseDataType> GetBaseDataType(Expression<Func<BaseDataType, bool>> predict)
        //{
        //    var _basetypelist = _basedatatypeService.Find(x => x.Status || predict.Compile().Invoke(x)).OrderBy(x => x.Seq);
        //    var _datalist = _datavalueService.Find(_basetypelist);

        //    foreach (var basetype in _basetypelist)
        //    {
        //        var datavalues = _datalist.Where(x => x.BaseDataTypeId == basetype.Id);
        //        if (basetype.HaveValue == null)
        //            basetype.HaveValue = new System.Collections.Generic.List<DataValue>();
        //        basetype.HaveValue.AddRange(datavalues);
        //    }
        //    return _basetypelist;
        //}

        //public void SaveChanges(IQueryable<BaseDataType> basedatatypeList)
        //{
        //    _basedatatypeService.BeginTrans();
        //    try
        //    {
        //        _basedatatypeService.SaveChanges(basedatatypeList);

        //        var datavaluelist = basedatatypeList.SelectMany(x => x.HaveValue);
        //        _datavalueService.SaveChanges(datavaluelist);
        //    }
        //    catch(Exception ex)
        //    {
        //        _basedatatypeService.Rollback();
        //    }

        //    _basedatatypeService.Commit();
        //}

        //public List<BaseDataType> MapBaseDataType(DataRowCollection rows)
        //{
        //    List<BaseDataType> typelist = new List<BaseDataType>();

        //    foreach (DataRow r in rows)
        //    {
        //        var bdt = new BaseDataType();
        //        bdt.Id = 0;
        //        bdt.BaseTypeName = r.Field<String>(1);
        //        bdt.Comment = r.Field<String>(2);
        //        bdt.Seq = DBNull.Value.Equals(r[3]) ? 0 : r.Field<int>(3);
        //        bdt.Status = DBNull.Value.Equals(r[4]) ? false : r.Field<Boolean>(4);
        //        bdt.LastModifier = r.Field<String>(5);
        //        bdt.LastModifyDate = DBNull.Value.Equals(r[6]) ? System.DateTime.Now : r.Field<DateTime>(6);
        //        bdt.ModifyStatus =
        //            r.RowState == DataRowState.Modified ? Domain.Core.BaseEntityStatus.Modefied :
        //            r.RowState == DataRowState.Added ? Domain.Core.BaseEntityStatus.NewEntity :
        //                                               Domain.Core.BaseEntityStatus.Deleted;

        //        typelist.Add(bdt);
        //    }
        //    return typelist;
        //}

        //public List<DataValue> MapDataValue(DataRowCollection rows)
        //{
        //    List<DataValue> dataList = new List<DataValue>();

        //    foreach (DataRow r in rows)
        //    {
        //        var dv = new DataValue();
        //        dv.Id = r.Field<int>(0);
        //        dv.BaseDataTypeId = r.Field<int>(1);
        //        dv.Name = r.Field<String>(2);
        //        dv.Value = r.Field<String>(3);
        //        dv.ValueType = r.Field<String>(4);
        //        dv.Comment = r.Field<String>(5);
        //        dv.Seq = DBNull.Value.Equals(r[6]) ? 0 : r.Field<int>(6);
        //        dv.Status = DBNull.Value.Equals(r[7]) ? false : r.Field<Boolean>(7);
        //        dv.LastModifier = r.Field<String>(8);
        //        dv.LastModifyDate = DBNull.Value.Equals(r[9]) ? System.DateTime.Now : r.Field<DateTime>(9);

        //        dv.ModifyStatus =
        //            r.RowState == DataRowState.Modified ? Domain.Core.BaseEntityStatus.Modefied :
        //            r.RowState == DataRowState.Added ? Domain.Core.BaseEntityStatus.NewEntity :
        //                                               Domain.Core.BaseEntityStatus.Deleted;

        //        dataList.Add(dv);
        //    }

        //    return dataList;
        //}
     
     
     
     */
