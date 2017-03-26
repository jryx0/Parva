using Parva.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Infrastructure.Implementations.Repository.EntityFramework.Mapping 
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.HasMany(o => o.Users).WithMany(o => o.Roles);

            this.Map(m => {
                m.MapInheritedProperties();
                m.ToTable("Role");
            });


            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}