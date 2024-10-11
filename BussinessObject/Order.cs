using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Order : BaseEntity
    {
        public Order() 
        {
            OrderDetails = new HashSet<OrderDetail>();
        }  
        public int? AccountId { get; set; }
        public int? AddressId { get; set; }
        public int? PaymentId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public double? ShippedFee { get; set; }
        public string? ShippedAddress { get; set; }
        public DateTime? ShippedDate { get; set; }
        public virtual Account? Account { get; set; }
        public virtual Address? Address { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
