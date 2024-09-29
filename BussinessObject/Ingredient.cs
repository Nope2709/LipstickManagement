using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Ingredient
    {
        public Ingredient() { 
            LipstickIngredients = new HashSet<LipstickIngredient>();
        }
        [Key]
        public int IngredientId {  get; set; }
        public string? Name { get; set; }
        public decimal? Percentage { get; set; }
        public virtual ICollection<LipstickIngredient> LipstickIngredients { get; set;}
    }
}
