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
      
    }
    public class CreateOrderRequestModel
    {
        public int AccountId { get; set; }
        public int PaymentId { get; set; }
        public int AddressId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public List<OrderItem> Items { get; set; }
    }
    public class UpdateOrderRequestModel : CreateOrderRequestModel
    {
        public int Id { get; set; }
        public string? Status { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public decimal Price { get; set; }
    }
    public class CancelOrderRequest
    {
        public int Id { get; set; }
    }
    public class VietQRRequest
    {
        public decimal? TotalPrice { get; set; }
    }
    public class ConfirmWebhook
    {
        public string Url { get; set; }
    }
}
