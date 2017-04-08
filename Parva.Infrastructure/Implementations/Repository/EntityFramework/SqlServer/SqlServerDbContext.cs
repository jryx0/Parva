using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infrastructure.Implementations.Repository.EntityFramework.SqlServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext()
            : base("name=SqlServerConnection")
        {
            this.Database.Log = o => System.Diagnostics.Debug.WriteLine(o);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(SqlServerDbContext).Assembly);
            modelBuilder.Properties()
                .Where(o => o.Name == "RowVersion").Configure(o => o.IsRowVersion());
            Database.SetInitializer(new SqlServerDbInitializer());
        }
    }
}