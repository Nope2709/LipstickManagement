using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Customization : BaseEntity
    {
       
        public int? LipstickId { get; set; }
        public string? EngravingText { get; set; }
        public string? QrCodeUrl { get; set; }

        public virtual Lipstick? Lipstick { get; set; }
    }
}
