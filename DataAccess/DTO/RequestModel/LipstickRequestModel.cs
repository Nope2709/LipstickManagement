using BussinessObject;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestModel
{
    public class LipstickRequestModel
    {
    }
    public class CreateLipstickRequestModel
    {
        public string? Usage { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal DiscountPrice { get; set; }
        public int? CategoryId { get; set; }
        public string? ExpiredDate { get; set; }
        public List<ImageURLResponseModel> imageURLs { get; set; }
        public List<LipstickIngredientResquestModel> LipstickIngredients { get; set; }

    }
    public class UpdateLipstickRequestModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Usage { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public int? CategoryId { get; set; }
        public string? ExpiredDate { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountPrice { get; set; }
        public List<ImageURLResponseModel> imageURLs { get; set; }

        public List<LipstickIngredientResquestModel> LipstickIngredients { get; set; }
    }
    public class OrderLipstick
    {
        public int ? Id { get; set; }
        public string? Name { get; set; }
    }
}
