using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject
{
    public partial class ImageURL : BaseEntity
    {
      
        
        public string? URL { get; set; }
        public int? LipstickId { get; set; }
        public virtual Lipstick? Lipstick { get; set; }



    }
}
