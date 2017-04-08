using Parva.Application.Interfaces.Repository;
using System.Linq;
using System;
using System.Linq.Expressions;
using System.Data;
using Parva.Domain.Core;

namespace Parva.Application.Core
{
    public sealed class EFCommonService<T> : IEFCommonService<T> where T : BaseEntity, new()
    {
        private IEFRepository<T> _repo;

        public EFCommonService(IEFRepository<T> repo)
        {
            this._repo = repo;
        }

        public T FindById(int id)
        {
            return _repo.FindById(id);
        }

        public IQueryable<T> Find()
        {
            return _repo.Find();
        }

        public void Add(T entity)
        {
            _repo.Insert(entity);
            _repo.SaveChanges();
        }

        public void Update(T entity)
        {
            ///_repo.Update(entity);
            _repo.SaveChanges();
        }

        public void Remove(int id)
        {
            _repo.Remove(_repo.FindById(id));
            _repo.SaveChanges();
        }
    }
}