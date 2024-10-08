using LipstickManagementAPI.DTO.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.RequestModel
{
    public class OrderDetailRequest
    {
        public int Id { get; set; } 
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public double? ShippedFee { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? quantity { get; set; }
        public OrderLipstick Lipstick { get; set; }  
        public AddressRequestModel Address { get; set; }   
        public OrderAccount OrderAccount { get; set; }
        
    }
    public class CancelOrderRequest
    {
        public int Id { get; set; }
    }
}
