using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Address : BaseEntity
    {
        
        public int? AccountId { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? ZipCode { get; set; }
        public virtual Account? Account { get; set; }
    }
}
