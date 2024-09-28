using BussinessObject;
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
        public List<ImageURL> imageURLs { get; set; }

    }
    public class UpdateLipstickRequestModel
    {
        public int LipstickId { get; set; }
        public string? Name { get; set; }
        public string? Usage { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public List<ImageURL> imageURLs { get; set; }
    }
}
