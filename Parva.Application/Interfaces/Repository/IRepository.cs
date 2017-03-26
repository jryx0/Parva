using Parva.Domain.Core;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Parva.Application.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        T FindById(int id);
        IQueryable<T> Find();
        IQueryable<T> Find(Expression<Func<T, bool>> condition);

        //T Mapor();

        //DataSet ExecuteDataset(string commandText);
        //int ExecuteNonQuery(string commandText);

        void Add(T entity);
        void Remove(T entity);
        int SaveChanges();
    }
}