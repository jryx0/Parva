using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parva.Domain.Core;

namespace Parva.Domain.Models
{
    public class DataValue : RowVersionEntity
    {        
        public int BaseDataTypeId { set; get; }
        public BaseDataType DataParent { get; set; }
        
        public string Name { get; set; }
                 
        public string Value { get; set; }        
        public string ValueType { get; set; }

        
        public string LastModifier { get; set; }
        public DateTime LastModifyDate { get; set; }
       
        public string Comment { get; set; }
    }
}
