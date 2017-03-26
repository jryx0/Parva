using Parva.Application;
using Parva.Application.Core.IoC;
//using Parva.Infrastructure.Implementations.Repository.EntityFramework.MySql;
using Parva.Infrastructure.Implementations.Repository.EntityFramework.Sqlite;
using System.Data.Entity;

namespace Parva.Infrastructure.Implementations.Repository
{
    public class RepositoryRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister<DbContext, SqliteDbContext>(Lifecycle.Singleton);
        }
    }
}