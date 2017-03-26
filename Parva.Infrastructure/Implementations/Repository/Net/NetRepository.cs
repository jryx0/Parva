using Parva.Application.Interfaces.Repository;
using Parva.Domain.Core;
using System;
using System.Linq;
using System.Data;
using System.Linq.Expressions;

namespace Parva.Infrastructure.Implementations.Repository.Net
{
    public class NetRepository<T> : IRepository<T>  where T : BaseEntity
    {

        public NetRepository(MySqlite _sqliet)
        {

        }



        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public DataSet ExecuteDataset(string commandText)
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string commandText)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public T Mapor()
        {
            throw new NotImplementedException();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
