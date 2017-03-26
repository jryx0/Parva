using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parva.Domain.Core;

namespace Parva.Domain.Models
{
    public class BaseDataType : RowVersionEntity
    {
        public string BaseTypeName { get; set; }
        

        public string Comment { get; set; }
         
        public string LastModifier { get; set; }
        public DateTime LastModifyDate { get; set; }

        public virtual List<DataValue> HaveValue { get; set; }
    }
}
