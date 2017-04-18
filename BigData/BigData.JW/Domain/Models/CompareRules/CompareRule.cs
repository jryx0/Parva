using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class CompareRule : RowVersionEntity
    {
        public string RuleName { set; get; }
        public string RuleDescrible { set; get; }
        public CompareItem SourceItem { set; get; }
        public string TableName { set; get; }

        public virtual List<CompareItem> InvolvedItems { set; get; }
        public virtual List<RuleParameter> Parameteres { set; get; }
        public virtual List<RuleDetail> RuleDetails { set; get; }         

        public RuleTemplate Template { set; get; }
        public RuleType CompareType { set; get; }
    }

    public enum RuleType
    {
        None = -1,
        Compare = 0,
        Check = 1,
        Preprocess = 2
    }
}
