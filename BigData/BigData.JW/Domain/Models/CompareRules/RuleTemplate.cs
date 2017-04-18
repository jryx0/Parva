using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class RuleTemplate : RowVersionEntity
    {
        public string TempName { set; get; }
        public string TempDecrible { set; get; }
        
        public virtual List<TemplateDetail> TempItems { set; get; }
    }
}
