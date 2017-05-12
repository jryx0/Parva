using Parva.Domain.Core;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class ItemDataFormat :  RowVersionEntity        
    {
        public int? ParentId { set; get; }
        public ItemFormat  Parent { set; get; }

        public int? ColInfoId { set; get; }
        public DataValue ColInfo { set; get; }        
        public int ColIndex { set; get; }

        public bool hasModified;
    }

    public class ItemFormat : RowVersionEntity
    {
        public int? ParentId;
        public CompareItem ParentItem { set; get; }

        public int StartPos { set; get; }
        public virtual List<ItemDataFormat> DataFormats { set; get; }

        public ItemFormat Original;
    }

}
