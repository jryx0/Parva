using Parva.Domain.Core;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Parva.Application.Core
{
    public interface IEFCommonService<T> where T : BaseEntity
    {
        void Add(T entity);

        IQueryable<T> Find();

        T FindById(int id);

        void Remove(int id);

        void Update(T entity);
    }
}