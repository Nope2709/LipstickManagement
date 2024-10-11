using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class OrderDetail : BaseEntity
    {
       
        public int? AccountId { get; set; }
        public int? AddressId { get; set; }
        public int? PaymentId { get; set; }
        public int? LipstickId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public double? ShippedFee { get; set; }
        public string? ShippedAddress { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? quantity { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Address? Address { get; set; }
        public virtual Lipstick? Lipstick { get; set; }
        public virtual Payment? Payment { get; set; }
    }
}
