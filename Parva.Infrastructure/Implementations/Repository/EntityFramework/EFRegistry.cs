using Parva.Application;
using Parva.Application.Core.IoC;
using Parva.Application.Interfaces.Repository;
using Parva.Infrastructure.Implementations.Repository.EntityFramework.Sqlite;
using System.Data.Entity;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework
{
    public class EFRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister<DbContext, SqliteDbContext>(Lifecycle.Singleton);
            AppEngine.Container.Resgister(typeof(IEFRepository<>), typeof(EFRepository<>));
        }
    }
}
