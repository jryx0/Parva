using Domain.Core;
using MySql.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Infrastructure.Implementations.Repository.EntityFramework.MySql
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlDbContext : DbContext
    {
        public MySqlDbContext()
            : base("name=MySqlConnection")
        {
            this.Database.Log = o => System.Diagnostics.Debug.WriteLine(o);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(MySqlDbContext).Assembly);
            modelBuilder.Properties().Where(o => typeof(IRowVersion).IsAssignableFrom(o.DeclaringType) && o.PropertyType == typeof(byte[]) && o.Name == "RowVersion")
                .Configure(o => o.IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None));
            Database.SetInitializer(new MySqlDbInitializer());
        }

        public override int SaveChanges()
        {
            this.ChangeTracker.DetectChanges();
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;
            foreach (ObjectStateEntry entry in objectContext.ObjectStateManager.GetObjectStateEntries(EntityState.Modified | EntityState.Added))
            {
                var v = entry.Entity as IRowVersion;
                if (v != null)
                {
                    v.RowVersion = System.Text.Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
                }
            }
            return base.SaveChanges();
        }
    }
}