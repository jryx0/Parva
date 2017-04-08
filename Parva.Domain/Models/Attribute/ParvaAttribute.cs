using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parva.Domain.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ParvaAttribute : Attribute
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public int Index { set; get; }

        public ParvaAttribute() { }

        public ParvaAttribute(string label, string name)
        {
            this.Label = label;
            this.Name = name;
        }

        public ParvaAttribute(string name, int index )
        {             
            this.Name = name;
            this.Index = index;
        }
    }
}
