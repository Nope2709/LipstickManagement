using DataAccess.DTO.RequestModel;
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


        public async Task<bool> IsValidData(Transaction transaction, string transactionSignature) => await orderDAO.IsValidData(transaction, transactionSignature);  
       
    }
}
