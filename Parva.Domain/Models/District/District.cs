using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parva.Domain.Models
{
    public class District  : TreeBase<District>
    {       
        public String IP { set; get; }
        public String RegionCode { set; get; }

        public bool HasModified;
        public District Original;

    }
}
