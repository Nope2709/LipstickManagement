using AutoMapper;
using BussinessObject;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
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
using static DataAccess.DTO.ResponseModel.OrderResponseModel;

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
        public async Task<string> DeleteOrder(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");

            _context.Orders.Remove(order);
            if (await _context.SaveChangesAsync() > 0)
                return "Delete Order Successfully";
            else
                return "Delete Order Failed";
        }
        //   public async Task<string> UpdateOrderAfterPaying()
        //   {
        //       var userID = Convert.ToInt32(_currentUserService.UserId);
        //       var order = await _context.Order.FirstOrDefaultAsync(x => x.CustomerID == userID);
        //       var activity = await _context.ActivityType.FirstOrDefaultAsync(x => x.Name.Equals("Đang chờ xác nhận"));
        //       order.OrderActivity.ActivityTypeID = activity.ID;

        //       if (await _context.SaveChangesAsync() > 0)
        //           return "Update Order Successfully";
        //       else
        //           return "Update Order Failed";
        //   }
        public async Task<List<OrderResponseModel>> GetWaitForPayOrders(string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            var query = _context.Orders
                .Include(o => o.Account)
                .Include(o => o.Address)
                .Include(o => o.Payment)
                .Include(o => o.OrderDetails)
                .Where(o => o.Status  == "Đang chờ thanh toán")
                .Select(o => new OrderResponseModel
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    AddressId = o.AddressId ?? default(int),
                    TotalPrice = o.TotalPrice,
                    Status = o.Status,
                    Phone = o.Account.Phone,
                    PaymentId = o.PaymentId ?? default(int)// Assuming Payment.Name is the property representing PaymentMethod
                });

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(o => o.Phone.Contains(search));
            }

            // Apply date range filter
            if (fromDate.HasValue)
            {
                query = query.Where(o => o.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(o => o.OrderDate <= toDate.Value);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    query = query.OrderBy(o => o.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    query = query.OrderByDescending(o => o.OrderDate);
                }
            }

            var orders = await query.ToListAsync();
            return _mapper.Map<List<OrderResponseModel>>(orders);


   }

        public async Task<List<OrderResponseModel>> GetPendingOrders(string? search, string? sortBy,
      DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                .Include(x => x.Account)
                .Include(x => x.Payment)
                .Include(x=>x.Address)
                .Where(x => x.Status == "Đang chờ xác nhận");

            foreach (var order in orders)
            {
                _context.Entry(order).Reference(x => x.Account).Load();
            }

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var orderEntities = await query.ToListAsync();
            var orderResponseModels = _mapper.Map<List<OrderResponseModel>>(orderEntities);

            return orderResponseModels;
        }


        public async Task<List<OrderResponseModel>> GetInProcessOrders(string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                .Include(x => x.Account)
                .Include(x => x.Payment)
                .Include(x => x.Address)
                .Where(x => x.Status == "Đang vận chuyển");

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var orderResponseModels = await query.ToListAsync();

            return orderResponseModels;
        }

        public async Task<List<OrderResponseModel>> GetDeliveredOrders(string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                .Include(x => x.Account)
                .Include(x => x.Payment)
                .Include(x => x.Address)
                .Where(x => x.Status == "Đã giao hàng");

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var orderResponseModels = await query.ToListAsync();

            return orderResponseModels;
        }

        public async Task<List<OrderResponseModel>> GetCanceledOrders(string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                .Include(x => x.Account)
                .Include(x => x.Payment)
                .Include(x => x.Address)
                .Where(x => x.Status == "Đã hủy");

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var orderResponseModels = await query.ToListAsync();

            return orderResponseModels;
        }

        public async Task<List<OrderResponseModel>> GetPendingOrdersByCustomerID(int accountID, string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                   .Include(x => x.AccountId)
                   .Include(x => x.Payment)
                   .Where(x => x.AccountId == accountID && x.Status.Equals("Đang chờ xác nhận"));

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var orderResponseModels = await query.ToListAsync();

            return orderResponseModels;
        }

        public async Task<List<OrderResponseModel>> GetInProcessOrdersByCustomerID(int accountID, string? search, string? sortBy,
      DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                   .Include(x => x.AccountId)
                   .Include(x => x.Payment)
                   .Where(x => x.AccountId == accountID && x.Status.Equals("Đang vận chuyển"));

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var or = await query.ToListAsync();
            var orderResponseModels = _mapper.Map<List<OrderResponseModel>>(or);

            return orderResponseModels;
                
             }

        public async Task<List<OrderResponseModel>> GetDeliveredOrdersByCustomerID(int accountID, string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                    .Include(x => x.AccountId)
                    .Include(x => x.Payment)
                    .Where(x => x.AccountId == accountID && x.Status.Equals("Đã giao hàng"));

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var or = await query.ToListAsync();
            var orderResponseModels = _mapper.Map<List<OrderResponseModel>>(or);

            return orderResponseModels;
        }

        public async Task<List<OrderResponseModel>> GetCanceledOrdersByCustomerID(int accountID, string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        {
            IQueryable<Order> orders = _context.Orders
                   .Include(x => x.AccountId)
                   .Include(x => x.Payment)
                   .Where(x => x.AccountId == accountID && x.Status.Equals("Đã hủy"));

            // Search by address
            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(x => x.Account.Phone.Contains(search));
            }

            // Filter by date range
            if (fromDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                orders = orders.Where(x => x.OrderDate <= toDate.Value);
            }

            // Sort by specified field
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("ascDate"))
                {
                    orders = orders.OrderBy(x => x.OrderDate);
                }
                else if (sortBy.Equals("descDate"))
                {
                    orders = orders.OrderByDescending(x => x.OrderDate);
                }
            }

            var query = orders.Select(o => new OrderResponseModel
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                AddressId = o.AddressId ?? default(int),
                TotalPrice = o.TotalPrice,
                Status = o.Status,
                Phone = o.Account.Phone,
                PaymentId = o.PaymentId ?? default(int)
            });

            var or = await query.ToListAsync();
            var orderResponseModels = _mapper.Map<List<OrderResponseModel>>(or);

            return orderResponseModels;
        }

        public async Task<OrderDetailResponseModel> GetOrderByID(int id)
        {
            var order = await _context.Orders
                .Include(x => x.Account)
                .Include(x => x.Payment)
                .Include(x => x.Address)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (order == null)
                throw new Exception("Order is not found");

            var orderResponse = new OrderDetailResponseModel
            {
                
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Status = order.Status,
                Phone = order.Account.Phone,
                PaymentId = order.Payment.Id,
                 Items = new List<OrderItemResponse>()
             
            };

            foreach (var hotpotPackage in order.OrderDetails)
            {
                orderResponse.Items.Add(new OrderItemResponse
                {
                   
                    Id = hotpotPackage.LipstickId,
                    Quantity = hotpotPackage.Quantity ?? default(int),
                    Total = hotpotPackage.TotalPrice ?? default(int),
                    
                });
            }

           



            return _mapper.Map<OrderDetailResponseModel>(orderResponse);
        }

        public async Task<string> UpdateOrderToInProcess(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");

            order.Status = "Đang vận chuyển";
            
            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }

        public async Task<string> UpdateOrderToDelivered(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");
            order.Status = "Đã giao hàng";
            
            if (await _context.SaveChangesAsync() > 0)
                return "Update Order Successfully";
            else
                return "Update Order Failed";
        }

        public async Task<string> UpdateOrderToCanceled(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(x => x.Id == id);
            if (order == null)
                throw new InvalidDataException("Order is not found");
            order.Status = "Đã hủy";
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
