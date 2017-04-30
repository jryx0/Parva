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

        public ItemType DataType { set; get; }
        //Name = ShortName
      //  public string ItemShortName { set; get; }
        public DateTime LastModifiedDate { set; get; }

        public virtual List<CompareRule> Rules { set; get; }
        public ItemFormat Format { set; get; }


        public bool HasModified;
        public CompareItem Original;
    }


    public enum ItemType
    {
        Root = 0,
        Source,
        Compare,
        Item,
        Folder,
        Data,
        SourceItem, //文件
        CompareItem //文件
    }
}
