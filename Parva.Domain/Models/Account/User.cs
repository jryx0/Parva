using Parva.Domain.Core;
using System;
using System.Collections.Generic;

namespace Parva.Domain.Models
{
    public class User : RowVersionEntity
    {
        public User()
        {
            this.Roles = new List<Role>();
        }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string SaltKey { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public District UserRegion { set; get; }
    }
}