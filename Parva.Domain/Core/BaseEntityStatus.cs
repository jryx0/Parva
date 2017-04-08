using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parva.Domain.Core
{    
    [Flags]
    public enum BaseEntityStatus 
    {
        Unchanged,
        NewEntity,
        Modefied,
        Deleted,
        Unkonwn
    }
}
