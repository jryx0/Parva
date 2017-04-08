using Parva.Application.Interfaces.Repository;
using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parva.Application.Core
{
    public interface IBaseObjectService<T> where T : BaseEntity
    {
        void Insert(T entity);
        T FindById(int id);

        string Name { set; get; }
        
        IQueryable<T> Find(Expression<Func<T, bool>> predict);
        IQueryable<T> Find(IQueryable<BaseEntity> entities);

        void Remove(int id);
        void Remove(T entity);
        void Update(T entity);


        void SaveChanges();
        void SaveChanges(IEnumerable<T> entities);

        void BeginTrans();
        void Commit();
        void Rollback();

        ISystemDataRepository _systemRepo { set; get; }

        ///// <summary>
        ///// 获取主表所有对象的一个明细
        ///// </summary>
        ///// <param name="entities"></param>
        ///// <param name="name"></param>
        ///// <param name="_type"></param>
        //IQueryable<BaseEntity> IncludeDetail(IQueryable<T> entities, string name, Type _type); 
        ///// <summary>
        ///// 获取主表一个对象的一个明细
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="name"></param>
        ///// <param name="_type"></param>
        //IQueryable<BaseEntity> IncludeDetail(T entity, string name, Type _type);
        ///// <summary>
        ///// 获取一个主表对象的一个明细
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <param name="name"></param>
        ///// <param name="_type"></param>
        //IQueryable<BaseEntity> IncludeDetail(string name, Type _type);
        //IQueryable<TDetail> IncludeDetail<TDetail>(string detailName);


        DataTable GetDataTable();      
    }
}
