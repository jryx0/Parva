using System;

namespace Parva.Domain.Models
{
    [Flags]
    public enum Permission
    {
        None = 0,
        Admin = 1 << 0
    }
}