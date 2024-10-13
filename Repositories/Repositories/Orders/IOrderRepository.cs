using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static DataAccess.DTO.ResponseModel.OrderResponseModel;

namespace Repositories.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<string> CreateOrder(CreateOrderRequestModel orderRequest);
        Task<bool> IsValidData(Transaction transaction, string transactionSignature);
        Task<string> UpdateOrder(UpdateOrderRequestModel order);
        Task<string> DeleteOrder(int id);
        Task<List<OrderResponseModel>> GetWaitForPayOrders(string? search, string? sortBy,
             DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetPendingOrders(string? search, string? sortBy,
              DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetInProcessOrders(string? search, string? sortBy,
             DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetDeliveredOrders(string? search, string? sortBy,
               DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetCanceledOrders(string? search, string? sortBy,
     DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetPendingOrdersByCustomerID(int accountID, string? search, string? sortBy,
             DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetInProcessOrdersByCustomerID(int accountID, string? search, string? sortBy,
              DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetDeliveredOrdersByCustomerID(int accountID, string? search, string? sortBy,
             DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<List<OrderResponseModel>> GetCanceledOrdersByCustomerID(int accountID, string? search, string? sortBy,
             DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize);
        Task<OrderDetailResponseModel> GetOrderByID(int id);
        Task<string> UpdateOrderToInProcess(int id);
        Task<string> UpdateOrderToDelivered(int id);
        Task<string> UpdateOrderToCanceled(int id);



    }
}
