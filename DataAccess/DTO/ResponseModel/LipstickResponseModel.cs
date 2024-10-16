﻿using BussinessObject;
using DataAccess.DTO.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ResponseModel
{
    public class LipstickResponseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Usage { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public CategoryResponseModel? Category { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountPrice { get; set; }

        public string? ExpiredDate { get; set; }
        public List<FeedbackResponseModel> feedbacks { get; set; }
        public List<ImageURLResponseModel> imageURLs { get; set; }
        public List<LipstickIngredientRespnseModel> LipstickIngredients { get; set; }
    }
}
