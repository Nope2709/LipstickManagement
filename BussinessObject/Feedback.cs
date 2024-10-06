using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Feedback : BaseEntity
    {
        
        public int? AccountId { get; set; }
        public int? LipstickId { get; set; }
        public string? Content { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Lipstick? Lipstick { get; set; }
    }
}
