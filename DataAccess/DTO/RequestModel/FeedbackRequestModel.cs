using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestModel
{
    public class FeedbackRequestModel
    {
        
    }
    public class CreateFeedbackRequestModel
    {      
        public int? AccountId { get; set; }
        public int? LipstickId { get; set; }
        public string? Content { get; set; }
    }
    public class UpdateFeedbackRequestModel
    {
        public int FeedbackId { get; set; }

       
        public string? Content { get; set; }
    }
}
