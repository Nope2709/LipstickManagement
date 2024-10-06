using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Payment : BaseEntity
    {
        public Payment()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

       
        public string? Method { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
