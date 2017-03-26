using Parva.Domain.Core;
using System.Collections.Generic;

namespace Parva.Domain.Models
{
    public class Role : BaseEntity
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public string Name { get; set; }

        public Permission Permisions { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}