using Application;
using Application.Services;
using System.Data.Entity;

namespace Infrastructure.Implementations.Repository.EntityFramework.SqlServer
{
    public class SqlServerDbInitializer : DropCreateDatabaseIfModelChanges<SqlServerDbContext>
    {
        protected override void Seed(SqlServerDbContext context)
        {
            var entities = AppEngine.Container.GetInstance<ISeedService>().GetSeeds();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    context.Set(item.GetType()).Add(item);
                }
            }
        }
    }
}