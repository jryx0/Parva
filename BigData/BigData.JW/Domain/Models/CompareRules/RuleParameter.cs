using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class RuleParameter : RowVersionEntity
    {
        public string ParaName { set; get; }        
        public ParameterType ParaType { set; get; }

        public string ParaDescrible { set; get; }
        public double ParaValue { set; get; }

        public int? CompareRuleId { set; get; }
        public CompareRule Parent { set; get; }
    }

    public enum ParameterType
    {
        Filed,
        Condition,
        Group,
        Order
    }
}
