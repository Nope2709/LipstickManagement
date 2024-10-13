using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using DataAccess.Orders;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDAO orderDAO;
       

        public OrderRepository(OrderDAO orderDAO)
        {
            this.orderDAO = orderDAO;
        }

        public async Task<string> CreateOrder(CreateOrderRequestModel orderRequest) => await orderDAO.CreateOrder(orderRequest);

        public async Task<string> DeleteOrder(int id) => await orderDAO.DeleteOrder(id);
        

        public async Task<List<OrderResponseModel>> GetCanceledOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize) => await orderDAO.GetCanceledOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);



        public async Task<List<OrderResponseModel>> GetCanceledOrdersByCustomerID(int accountID, string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
=> await orderDAO.GetCanceledOrdersByCustomerID(accountID, search, sortBy, fromDate, toDate, pageIndex, pageSize);


        public async Task<List<OrderResponseModel>> GetDeliveredOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        => await orderDAO.GetDeliveredOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);


        public async Task<List<OrderResponseModel>> GetDeliveredOrdersByCustomerID(int accountID, string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        => await orderDAO.GetDeliveredOrdersByCustomerID(accountID, search, sortBy, fromDate, toDate, pageIndex, pageSize);     


        public async Task<List<OrderResponseModel>> GetInProcessOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        => await orderDAO.GetInProcessOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);


        public async Task<List<OrderResponseModel>> GetInProcessOrdersByCustomerID(int accountID, string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        => await orderDAO.GetInProcessOrdersByCustomerID(accountID, search, sortBy, fromDate, toDate, pageIndex, pageSize);

        public async Task<OrderResponseModel.OrderDetailResponseModel> GetOrderByID(int id) => await orderDAO.GetOrderByID(id);



        public async Task<List<OrderResponseModel>> GetPendingOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize) => await orderDAO.GetPendingOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);



        public async Task<List<OrderResponseModel>> GetPendingOrdersByCustomerID(int accountID, string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize) => await GetPendingOrdersByCustomerID(accountID, search, sortBy, fromDate, toDate, pageIndex, pageSize);


        public async Task<List<OrderResponseModel>> GetWaitForPayOrders(string? search, string? sortBy, DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize) => await orderDAO.GetWaitForPayOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        

        public async Task<bool> IsValidData(Transaction transaction, string transactionSignature) => await orderDAO.IsValidData(transaction, transactionSignature);

        public async Task<string> UpdateOrder(UpdateOrderRequestModel order) => await orderDAO.UpdateOrder(order);
        

        public async Task<string> UpdateOrderToCanceled(int id) => await orderDAO.UpdateOrderToCanceled(id);
        

        public async Task<string> UpdateOrderToDelivered(int id) => await orderDAO.UpdateOrderToInProcess(id);
       

        public async Task<string> UpdateOrderToInProcess(int id) => await orderDAO.UpdateOrderToInProcess(id);
        
    }
}
