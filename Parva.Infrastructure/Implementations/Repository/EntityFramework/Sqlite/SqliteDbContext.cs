using Parva.Domain.Core;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.Sqlite
{
    public class SqliteDbContext : DbContext
    {
        public SqliteDbContext()
            : base("name=SqliteConnection")
        {
            this.Database.Log = o => System.Diagnostics.Debug.WriteLine(o);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // modelBuilder.Configurations.AddFromAssembly(typeof(SqliteDbContext).Assembly);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {                
                modelBuilder.Configurations.AddFromAssembly(assembly);
            }

            modelBuilder.Properties().Where(o => typeof(IRowVersion).IsAssignableFrom(o.DeclaringType) && o.PropertyType == typeof(byte[]) && o.Name == "RowVersion")
                .Configure(o => o.IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None));
            Database.SetInitializer(new SqliteDbInitializer(this.Database.Connection.ConnectionString, modelBuilder));
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