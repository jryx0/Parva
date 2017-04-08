using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Parva.Domain.Core;

namespace Parva.Domain.Models
{
    public abstract class TreeBase<T> : BaseEntity, IRowVersion
    {         
        public String Name { set; get; }

        public int? ParentId { set; get; }
        public virtual T Parent { set; get; }
        public virtual List<T> Child { set; get; }
       
        public int Level { set; get; }   
        public byte[] RowVersion { get; set; }


       // public int ModifyStauts;

        /// <summary>
        /// Only copy node value but did not copy the node relation(the Id ,Parent and child ).
        /// </summary>
        /// <param name="t">Value to copy</param>
        public virtual void CopyNodeValue(TreeBase<T> t)
        {
            if (t == null) return;

            this.Name = t.Name;
            this.Seq = t.Seq;
            this.Level = t.Level;            
        }
    }
}
