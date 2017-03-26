using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parva.Domain.Core;

namespace Parva.Domain.Models
{
    public class DataValue : RowVersionEntity
    {
        public int? BaseDataTypeId { set; get; }
        public BaseDataType ItemParent { get; set; }
        
        public string ItemName { get; set; }
                 
        public string ItemValue { get; set; }        
        public string ItemValueType { get; set; }

        
        public string LastModifier { get; set; }
        public DateTime LastModifyDate { get; set; }
       
        public string Comment { get; set; }
    }
}
