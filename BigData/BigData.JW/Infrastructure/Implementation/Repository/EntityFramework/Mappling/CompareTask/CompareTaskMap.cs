
using BigData.JW.Models;
using Parva.Domain.Models;
using System.Data.Entity.ModelConfiguration;

namespace BigData.JW.Repository.EntityFramework.Mapping
{
    class CompareTaskMap : EntityTypeConfiguration<CompareTask> 
    {
        public CompareTaskMap()
        {
            this.Map(m => {
                m.MapInheritedProperties();
                m.ToTable("Task");
            });
        }
    }
}
