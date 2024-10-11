using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class OrderDetail : BaseEntity
    {
        public int? Quantity { get; set; }
        public int? LipstickId { get; set; }
        public int? OrderId { get; set;}
    }
}
