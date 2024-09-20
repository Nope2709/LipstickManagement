using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Payment
    {
        public Payment()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int PaymentId { get; set; }
        public string? Method { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
