using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Address
    {
        public Address()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int AddressId { get; set; }
        public int? AccountId { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
