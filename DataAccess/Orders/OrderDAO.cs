using AutoMapper;
using BussinessObject;
using DataAccess.DTO.RequestModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccess.Orders
{
    public class OrderDAO
    {
        private readonly LipstickManagementContext _context;
        private IMapper _mapper;
        private static readonly string checksumKey = "1a54716c8f0efb2744fb28b6e38b25da7f67a925d98bc1c18bd8faaecadd7675";
        public OrderDAO(LipstickManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> CreateOrder(CreateOrderRequestModel orderRequest)
        {
            if (orderRequest == null || orderRequest.Items == null || !orderRequest.Items.Any())
            {
                throw new ArgumentException("Invalid order data.");
            }

            var order = new Order()
            {
               AccountId = orderRequest.AccountId,
               PaymentId = orderRequest.PaymentId,
               AddressId = orderRequest.AddressId,
               TotalPrice = orderRequest.TotalPrice,
               OrderDate = DateTime.Now,
               Status= "Đang chờ xác nhận",
              
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            // Lấy thông tin sản phẩm dựa trên ProductIds
           

            // Tạo các order detail
            
            foreach (var item in orderRequest.Items)
            {
                var hotPotEntity = await _context.Lipsticks.SingleOrDefaultAsync(x => x.Id == item.Id);
                if (hotPotEntity == null)
                    throw new InvalidDataException("Lipstick is not found");
                var lip = new OrderDetail()
                    {
                        OrderId = order.Id,
                        LipstickId = item.Id,
                        Quantity = item.Quantity,
                        TotalPrice=item.Price,
                        CreatedDate = DateTime.Now,
                    };
                    _context.OrderDetails.Add(lip);
                
              
            }

           

           
            if (await _context.SaveChangesAsync() > 0)
                return "Create Order Successfully";
            else
                return "Create Order Failed";
        }
        public async Task<string> UpdateOrder(UpdateOrderRequestModel order)
        {


            var orderEntity = await _context.Orders.SingleOrDefaultAsync(x => x.Id == order.Id);
            if (orderEntity == null)
                throw new InvalidDataException("Order is not found");

            orderEntity.OrderDate = order.OrderDate;
            orderEntity.AccountId = order.AccountId;
            orderEntity.AddressId = order.AddressId;
            orderEntity.TotalPrice = order.TotalPrice;
            orderEntity.Status = order.Status;
            orderEntity.PaymentId = order.PaymentId;
            // Update additional properties if needed

            _context.Orders.Update(orderEntity);
            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }
        public async Task<bool> IsValidData(Transaction transaction, string transactionSignature)
        {
            try
            {
                string transactionJson = JsonConvert.SerializeObject(transaction);

                // Parse the JSON string into a JObject
                JObject jsonObject = JObject.Parse(transactionJson);
           

                // Sort the keys and build the transaction string
                var sortedKeys = jsonObject.Properties().OrderBy(p => p.Name);
                StringBuilder transactionStr = new StringBuilder();

                foreach (var property in sortedKeys)
                {
                    transactionStr.Append($"{property.Name}={property.Value}");
                    // Append '&' if there are more properties to come
                    if (property != sortedKeys.Last())
                    {
                        transactionStr.Append("&");
                    }
                }

                // Generate the HMAC SHA256 signature
                string generatedSignature = GenerateHmacSha256(transactionStr.ToString(), checksumKey);
                return generatedSignature.Equals(transactionSignature, StringComparison.OrdinalIgnoreCase);
                // Calculate checksum

            }
            catch (Exception ex)
            {
                // Handle exception
                Console.WriteLine(ex.Message);
                return false;
            }

        }
        private static string GenerateHmacSha256(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant(); // Convert to hex string
            }
        }
    }
}
