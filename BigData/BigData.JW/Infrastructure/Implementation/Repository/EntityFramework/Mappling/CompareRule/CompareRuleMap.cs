using BigData.JW.Models;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.JW.Repository.EntityFramework.Mapping
{
    public class CompareRuleMap : EntityTypeConfiguration<CompareRule> 
    {
        public CompareRuleMap()
        {
            this.Map(m => {
                m.MapInheritedProperties();
                m.ToTable("CompareRule");
            });
        }
    }

    
}
