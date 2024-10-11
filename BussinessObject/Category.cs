using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }    
        public Lipstick? Lipstick { get; set; }
    }
}
