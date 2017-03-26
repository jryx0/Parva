using Parva.Domain.Core;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Parva.Application.Core
{
    public interface ICommonService<T> : IBaseService where T : BaseEntity
    {
        void Add(T entity);

        IQueryable<T> Find(bool bTracking = true);
        IQueryable<T> Find(Expression<Func<T, bool>> condition, bool bTracking = true);

        

        T FindById(int id);

        void Remove(int id);

        void Update(T entity);

        void SaveChanges();
    }
}