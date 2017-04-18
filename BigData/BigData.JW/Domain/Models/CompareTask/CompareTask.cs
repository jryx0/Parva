using Parva.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class CompareTask : RowVersionEntity
    {
        public string TaskName { set; get; }
        public DateTime CreateDate { set; get; }

        public string TaskDecrible { set; get; }
        
        public string TaskToken { set; get; }
        public bool IsReported { set; get; }
    }
}
