using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.JW.Models
{
    public class RuleDetail : RowVersionEntity
    {
        public String Name { set; get; }
        public string Describle { set; get; }
        public ItemType DetailType { set; get; }
        public string Value { set; get; }

        public int? CompareRuleId { set; get; }
        public CompareRule Parent {set;get;}
    }

    public enum ItemType
    {
        Detail,
        Summuay,
        PreProcess,
        Final
    }

}
