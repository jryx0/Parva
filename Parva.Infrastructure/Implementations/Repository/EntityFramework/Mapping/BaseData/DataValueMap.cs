using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Parva.Infrastructure.Implementations.Repository.EntityFramework.Mapping 
{
    public class DataValueMap : EntityTypeConfiguration<DataValue>
    {
        public DataValueMap()
        {
            this.Map(m =>
            {
                m.MapInheritedProperties();
                m.ToTable("DataValue");
                
            });

            this.Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            this.HasRequired(x => x.DataParent).WithMany(x => x.HaveValue).HasForeignKey(x => x.BaseDataTypeId).WillCascadeOnDelete(false);
            //this.Property(p => p.BaseDataTypeId).HasColumnType("Integer");
        }
    }
}
