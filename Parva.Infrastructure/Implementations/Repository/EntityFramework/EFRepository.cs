using Parva.Application.Interfaces.Repository;
using Parva.Domain.Core;
using System.Data.Entity;
using System.Linq;
using System;
using System.Data;
using System.Linq.Expressions;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework
{
    public class EFRepository<T> : IEFRepository<T> where T : BaseEntity
    {
        static EFRepository()
        {
            //using (var context = AppEngine.Container.GetInstance<DbContext>())
            //{
            //    var objectContext = ((IObjectContextAdapter)context).ObjectContext;
            //    var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
            //    mappingCollection.GenerateViews(new List<EdmSchemaError>());
            //}
        }

        private DbContext _context;
        private IDbSet<T> _entities;

        public DbContext _dbContext
        {
            get
            {
                return _context;
            }
            private set
            {
                _context = value;
            }       
        }

        public EFRepository(DbContext context)
        {
            this._dbContext = context;
            this._entities = context.Set<T>();
        }

        public T FindById(int id)
        { 
            return this._entities.Find(id);
        }

        public T FindByIdAsNoTracking(int id)
        {
            return this._entities.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
        }

        public IQueryable<T> FindAsNoTracking()
        {
            return this._entities.AsNoTracking();
        }

        public IQueryable<T> Find()
        {
            return this._entities;
        }

        public void Insert(T entity)
        {
            this._entities.Add(entity);
        }

        public void Remove(T entity)
        {             
            this._entities.Remove(entity);
        }

        public int SaveChanges()
        {
            return this._dbContext.SaveChanges();
        }

        public T Attach(T entity)
        {
            return this._entities.Attach(entity);
        }

        public IQueryable<T> GetChangeEntity()
        {
            if(this._dbContext.ChangeTracker != null)
            {
                var changeList = _dbContext.ChangeTracker.Entries<T>().Select(x => x.Entity);
                if(changeList != null)
                    return changeList.AsQueryable();
            }

            return null;
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> condition)
        {
            return Find().Where( x => condition.Compile().Invoke(x));
        }
    }
}