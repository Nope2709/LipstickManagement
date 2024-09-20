using System;
using System.Collections.Generic;

namespace BussinessObject
{
    public partial class Customization
    {
        public int CustomizationId { get; set; }
        public int? LipstickId { get; set; }
        public string? EngravingText { get; set; }
        public string? QrCodeUrl { get; set; }

        public virtual Lipstick? Lipstick { get; set; }
    }
}
