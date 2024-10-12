using DataAccess.DTO.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Repositories.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<string> CreateOrder(CreateOrderRequestModel orderRequest);
        Task<bool> IsValidData(Transaction transaction, string transactionSignature);
    }
}
