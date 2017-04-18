using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class ItemFormat :  RowVersionEntity        
    {
        public int? CompareItemId { set; get; }
        public CompareItem Parent{ set; get; }
        public string DisplayName { set; get; }
        
        public string ColName { set; get; }
        public int ColIndex { set; get; }

        public FormatType ValueType { set; get; }   
    }

    public enum FormatType
    {
        StartPos,
        ColIndex
    }
}
