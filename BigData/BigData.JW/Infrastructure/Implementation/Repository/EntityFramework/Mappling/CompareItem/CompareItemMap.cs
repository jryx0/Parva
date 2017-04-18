using BigData.JW.Models;
using System.Data.Entity.ModelConfiguration;

namespace BigData.JW.Infrastructure.Implementations.Repository.EntityFramework.Mapping
{
    public class CompareItemMap : EntityTypeConfiguration<CompareItem>
    {
        public CompareItemMap()
        {
            this.Map(m => {
                m.MapInheritedProperties();
                m.ToTable("CompareItem");
            });


            //Many to Many CompareItem - CompareRule
            //Involved Item
            this.HasMany<CompareRule>(s => s.Rules)
                .WithMany(c => c.InvolvedItems)
                .Map(ri =>
                {
                    ri.MapLeftKey("ItemId");
                    ri.MapRightKey("RuleId");
                    ri.ToTable("ItemRule");
                });
        }
    }
}