using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Payment : BaseEntity
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

       
        public string? Method { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
