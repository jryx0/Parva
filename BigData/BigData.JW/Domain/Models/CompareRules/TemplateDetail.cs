using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class TemplateDetail : RowVersionEntity
    {
        public String Name { set; get; }
        public string Decrible { set; get; }

        public string Value { set; get; }
        public ItemType ValueType { set; get; }

        public int? RuleTempId { set; get; }
        public RuleTemplate Parent { set; get; }
    }
}
