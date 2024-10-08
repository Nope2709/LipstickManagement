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
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public List<ImageURLResponseModel> imageURLs { get; set; }
        public List<LipstickIngredientRespnseModel> LipstickIngredients { get; set; }

    }
    public class UpdateLipstickRequestModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Usage { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        
        public List<ImageURLResponseModel> imageURLs { get; set; }

        public List<LipstickIngredientRespnseModel> LipstickIngredients { get; set; }
    }
    public class OrderLipstick
    {
        public int ? Id { get; set; }
        public string? Name { get; set; }
    }
}
