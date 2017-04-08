using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.Mapping 
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            this.Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("District");
            })
            .HasMany(c => c.Child)
            .WithOptional(p =>  p.Parent)
            .HasForeignKey(t => t.ParentId)
            .WillCascadeOnDelete(true);

            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
                                    
        }
    }
}
