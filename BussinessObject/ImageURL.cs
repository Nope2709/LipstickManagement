using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject
{
    public partial class ImageURL
    {
        [Key]
        public int ImageId { get; set; }
        
        public string? URL { get; set; }

        
        
    }
}
