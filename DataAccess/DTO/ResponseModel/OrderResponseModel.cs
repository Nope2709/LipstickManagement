using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO.ResponseModel
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public int AddressId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public string Phone { get; set; }
        public int PaymentId { get; set; }
        public class OrderDetailResponseModel
        {
            public int Id { get; set; }
            public DateTime? OrderDate { get; set; }
            public decimal? TotalPrice { get; set; }
            public string? Status { get; set; }
            public string Phone { get; set; }
            public int PaymentId { get; set; }
            public List<OrderItemResponse> Items { get; set; }

        }

        public class OrderItemResponse
        {
            public int? Id { get; set; }
            public int Quantity { get; set; }
            public decimal Total { get; set; }
            public string? ImageUrl { get; set; }
        }

    }
}
