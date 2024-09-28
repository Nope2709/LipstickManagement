using BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ResponseModel
{
    public class LipstickResponseModel
    {
        public int LipstickId { get; set; }
        public string? Name { get; set; }
        public string? Usage { get; set; }
        public string? Type { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }

      
        public List<FeedbackResponseModel> feedbacks { get; set; }
        public List<ImageURL> imageURLs { get; set; }
    }
}
