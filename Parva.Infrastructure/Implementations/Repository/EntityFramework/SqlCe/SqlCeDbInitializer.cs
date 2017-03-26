using Application;
using Application.Services;
using System.Data.Entity;

namespace Infrastructure.Implementations.Repository.EntityFramework.SqlCe
{
    public class SqlCeDbInitializer : DropCreateDatabaseIfModelChanges<SqlCeDbContext>
    {
        protected override void Seed(SqlCeDbContext context)
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