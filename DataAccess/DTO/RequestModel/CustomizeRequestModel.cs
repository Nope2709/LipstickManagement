using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestModel
{
    public class CustomizeRequestModel
    {
    }
    public class CreateCustomizeRequestModel
    {
        
        public int? LipstickId { get; set; }
        public string? EngravingText { get; set; }
        public string? QrCodeUrl { get; set; }
    }
    public class UpdateCustomizeRequestModel
    {
        public int Id { get; set; }
        public int? LipstickId { get; set; }
        public string? EngravingText { get; set; }
        public string? QrCodeUrl { get; set; }
    }
}
