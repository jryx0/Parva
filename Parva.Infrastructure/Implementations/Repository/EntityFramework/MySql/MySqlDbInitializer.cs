using Parva.Application;
using Parva.Application.Services;
using System.Data.Entity;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.MySql
{
    public class MySqlDbInitializer : DropCreateDatabaseIfModelChanges<MySqlDbContext>
    {
        public override void InitializeDatabase(MySqlDbContext context)
        {
            base.InitializeDatabase(context);
        }

        protected override void Seed(MySqlDbContext context)
        {
            var entities = AppEngine.Container.GetInstance<ISeedService>().GetSeeds();
            if (entities != null)
            {
                foreach (var item in entities)
                {
                    context.Set(item.GetType()).Add(item);
                }
            }
            context.SaveChanges();
        }
    }
}