using Parva.Application;
using Parva.Application.Core.IoC;
using Parva.Application.Interfaces.Encryption;
//using Parva.Infrastructure.Implementations.Repository.EntityFramework.MySql;
using Parva.Infrastructure.Implementations.Repository.EntityFramework.Sqlite;
using Parva.Infrastructure.Implementations.Repository.SystemData;
using System.Data.Entity;

namespace Parva.Infrastructure.Implementations.Repository
{
    public class RepositoryRegistry : IIoCRegistry
    {
        public void Register()
        {
            AppEngine.Container.Resgister<DbContext, SqliteDbContext>(Lifecycle.Singleton);
            AppEngine.Container.Resgister<IEncryptionService, EncryptionService>(Lifecycle.Transient);

            AppEngine.Container.Resgister<MySqlite>();
            
        }
    }
}