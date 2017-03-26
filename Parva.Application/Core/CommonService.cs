using Parva.Application.Interfaces.Repository;
using Parva.Domain.Core;
using System.Data.Entity;
using System.Linq;
using System;
using System.Linq.Expressions;

namespace Parva.Application.Core
{
    public sealed class CommonService<T> : BaseService, ICommonService<T> where T : BaseEntity
    {
        private IRepository<T> _repo;

        public CommonService(IRepository<T> repo)
        {
            this._repo = repo;
        }

        public T FindById(int id)
        {
            // return _repo.Find().Where( x => x.Id == id).FirstOrDefault();
            return _repo.FindById(id);
        }

        public IQueryable<T> Find(bool bTracking = true)
        {             
            return bTracking ? _repo.Find() : _repo.Find().AsNoTracking();           
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> condition, bool bTracking = true)
        {
            return bTracking ? _repo.Find(condition) : _repo.Find(condition).AsNoTracking();
        }


        public void Add(T entity)
        {
            _repo.Add(entity);
             
        }

        public void Update(T entity)
        {
            ///_repo.Update(entity);            
        }

        public void Remove(int id)
        {
            _repo.Remove(_repo.FindById(id));            
        }

        public void SaveChanges()
        {
            _repo.SaveChanges();
        }
    }
}