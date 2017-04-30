using Parva.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.Mapping 
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.Property(o => o.UserName).HasMaxLength(128)
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute() { IsUnique = true }));

            this.Map(m => {
                m.MapInheritedProperties();
                m.ToTable("User");
            });

            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);


        }
    }
}