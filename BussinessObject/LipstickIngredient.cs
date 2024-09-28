using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class LipstickIngredient
    {
        [Key]
        public int Id { get; set; }
        public int LipstickId { get; set; }
        public int IngredientId { get; set; }
        public Lipstick? Lipstick { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
