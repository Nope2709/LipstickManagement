using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

       
        public string? RoleName { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
