﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public partial class Lipstick
    {
        public Lipstick()
        {
            Customizations = new HashSet<Customization>();
            Feedbacks = new HashSet<Feedback>();
            OrderDetails = new HashSet<OrderDetail>();
            ImageURLs = new HashSet<ImageURL>();
            LipstickIngredients = new HashSet<LipstickIngredient>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LipstickId { get; set; }
        public string? Name { get; set; }
        public string? Usage { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        
        public virtual ICollection<ImageURL> ImageURLs { get; set; }   
        public virtual ICollection<Customization> Customizations { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<LipstickIngredient> LipstickIngredients { get; set; }
    }
}
