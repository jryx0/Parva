using Parva.Application;
using Parva.Application.Services;
using SQLite.CodeFirst;
//using Parva.Infrastructure.Libraries.Sqlite;
using System.Data.Entity;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.Sqlite
{
    public class SqliteDbInitializer : SqliteDropCreateDatabaseWhenModelChanges<SqliteDbContext>
    {
        public SqliteDbInitializer(string connectionString, DbModelBuilder modelBuilder)
            : base(modelBuilder)
        {
            
        }

        protected override void Seed(SqliteDbContext context)
        {
            //var entities = AppEngine.Container.GetInstance<ISeedService>().GetSeeds();
            //if (entities != null)
            //{
            //    foreach (var item in entities)
            //    {
            //        context.Set(item.GetType()).Resgister(item);
            //    }
            //}
        }
    }
}