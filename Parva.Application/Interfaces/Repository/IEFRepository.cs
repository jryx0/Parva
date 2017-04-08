using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Parva.Application.Interfaces.Repository
{
    public interface IEFRepository<T> where T : BaseEntity
    {
        T FindById(int id);
        IQueryable<T> Find();
        IQueryable<T> Find(Expression<Func<T, bool>> condition);

        void Insert(T entity);
        void Remove(T entity);
        int SaveChanges();
    }
}
