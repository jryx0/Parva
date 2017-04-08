using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Parva.Application.Core
{
    public interface IBaseObject<T> where T : BaseEntity
    {
        T Entity { set; get; }

        IQueryable<T> Mapor(IDataReader reader);
        
        IDbDataParameter[] getParameter();

        string InsertSql { get; }
        string UpdateSql { get; }
        string SelectSql { get; }
        string DeleteSql { get; }
        string Name { get; set; }

        //IQueryable<BaseEntity> IncludeDetail(IQueryable<T> entities, string name, Type _type);
        //IQueryable<BaseEntity> IncludeDetail(T entity, string name, Type _type);
        //IQueryable<BaseEntity> IncludeDetail(string name, Type _type);
        //IQueryable<TDetail> IncludeDetail<TDetail>(string detailName);
        
        
    }
}
