using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Account : BaseEntity
    {
        public Account()
        {
            Addresses = new HashSet<Address>();
            Feedbacks = new HashSet<Feedback>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public string? Gender { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
