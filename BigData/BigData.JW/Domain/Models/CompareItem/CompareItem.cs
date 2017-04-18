using Parva.Domain.Core;
using Parva.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigData.JW.Models
{
    public class CompareItem : TreeBase<CompareItem>
    {
        /// <summary>
        /// Item Full Name
        /// </summary>
        public string ItemName { set; get; }

        public string TableName { set; get; }
        public string Path { set; get; }
        public string Describle { set; get; }

        public string Reserved1 { set; get; }
        public string Reserved2 { set; get; }

        public string ItemShortName { set; get; }
        public DateTime CreateTime { set; get; }

        public virtual List<CompareRule>  Rules { set; get; }
        public virtual List<ItemFormat> Formats { set; get; }
    }
}
