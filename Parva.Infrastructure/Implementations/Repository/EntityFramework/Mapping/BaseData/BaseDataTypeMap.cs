using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.Mapping 
{
    public class BaseDataTypeMap : EntityTypeConfiguration<BaseDataType>
    {
        public BaseDataTypeMap()
        {
            this.Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("BaseDataType");
            });

            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            // this.HasMany(x => x.HaveValue).WithRequired(x => x.DataParent).HasForeignKey(x => x.BaseDataTypeId);
           // this.Property(x => x.Id).HasColumnType("int");
            
        }
    }
}
