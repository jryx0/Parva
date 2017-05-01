using Parva.Application.Interfaces.Repository;
using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Parva.Application.Core
{
    public class BaseObjectService<T> : IBaseObjectService<T> where T : BaseEntity
    {
        public IBaseObject<T> _baseObject;
        public ISystemDataRepository _systemRepo { set; get; }

        public string Name
        {
            get
            {
                return _baseObject.Name;
            }

            set
            {
                _baseObject.Name = value;
            }
        }

        public BaseObjectService(IBaseObject<T> baseObject, ISystemDataRepository systemRepo)
        {
            _baseObject = baseObject;
            _systemRepo = systemRepo;
        }

        public void BeginTrans()
        {
            this._systemRepo.BeginTran();
        }
        public void Commit()
        {
            this._systemRepo.Commit();
        }
        public void Rollback()
        {
            this._systemRepo.Rollback();
        }

        public IQueryable<T> FindByParentId(IQueryable<int> Ids )
        {
            var sql = _baseObject.SelectSql;
            var inset = "";
            foreach (var id in Ids)
            {
                inset += id + ",";
            }

            sql += " and ParentId in (" + inset.TrimEnd(',') + ")";
            var reader = _systemRepo.ExecuteReader(sql);

            return _baseObject.Mapor(reader);
        }

        public IQueryable<T> Find(IQueryable<BaseEntity> entities)
        {
            var sql = _baseObject.SelectSql;
            var inset = "";
            foreach (var entity in entities)
            {
                inset += entity.Id + ",";
            }

            sql += " and id in (" + inset.TrimEnd(',') + ")";
            var reader = _systemRepo.ExecuteReader(sql);

            return _baseObject.Mapor(reader);

        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predict)
        {
            var reader = _systemRepo.ExecuteReader(_baseObject.SelectSql);

            return _baseObject.Mapor(reader)
               .Where(x => predict.Compile().Invoke(x));
        }

        public T FindById(int id)
        {
            var sql = _baseObject.SelectSql + " and Id = " + id;
            var reader = _systemRepo.ExecuteReader(sql);

            return _baseObject.Mapor(reader).FirstOrDefault();

        }

        public void Insert(T entity)
        {
            _baseObject.Entity = entity;

            _systemRepo.ExecuteNonQuery(_baseObject.InsertSql, _baseObject.getParameter());
            var o = _systemRepo.ExecuteScalar("select last_insert_rowid()");
            _baseObject.Entity.Id = Convert.ToInt32(o);
        }

        public void Remove(int id)
        {
            var sql = _baseObject.DeleteSql + " and id = " + id;
            _systemRepo.ExecuteNonQuery(sql);
        }

        public void Remove(T entity)
        {
            _baseObject.Entity = entity;
            var sql = _baseObject.DeleteSql;
            _systemRepo.ExecuteNonQuery(_baseObject.DeleteSql, _baseObject.getParameter());
        }

        public void SaveChanges()
        {
            switch (_baseObject.Entity?.ModifyStatus)
            {
                case BaseEntityStatus.Deleted:
                    Remove(_baseObject.Entity);
                    break;
                case BaseEntityStatus.NewEntity:
                    Insert(_baseObject.Entity);
                    break;
                case BaseEntityStatus.Modefied:
                    Update(_baseObject.Entity);
                    break;
            }
        }

        public void SaveChanges(IEnumerable<T> entities)
        {
            if (entities == null || entities.Count() == 0)
                return;

            foreach (var entity in entities)
            {
                _baseObject.Entity = entity;
                SaveChanges();
            }
        }

        public void Update(T entity)
        {
            _baseObject.Entity = entity;

            _systemRepo.ExecuteNonQuery(_baseObject.UpdateSql, _baseObject.getParameter());
        }

        //#region include detail
        //public IQueryable<BaseEntity> IncludeDetail(IQueryable<T> entities, string name, Type _type)
        //{
        //    return this._baseObject.IncludeDetail(entities, name, _type);
        //}

        //public IQueryable<BaseEntity> IncludeDetail(T entity, string name, Type _type)
        //{
        //    return this._baseObject.IncludeDetail(entity, name, _type);
        //}

        //public IQueryable<BaseEntity> IncludeDetail(string name, Type _type)
        //{
        //    return this._baseObject.IncludeDetail(name, _type);

        //}

        //public IQueryable<TDetail> IncludeDetail<TDetail>(string detailName)
        //{
        //    return this._baseObject.IncludeDetail<TDetail>(detailName);
        //}
        //#endregion


        public DataTable GetDataTable()
        {
            var ds = this._systemRepo.ExecuteDataset(_baseObject.SelectSql + " order by seq");
            if (ds == null)
                return null;

            ds.Tables[0].TableName = this.Name;
            return ds.Tables[0];
        }

        
    }
}
