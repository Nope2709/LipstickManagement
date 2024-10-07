using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using HotPotToYou.Controllers.ResponseType;
using BussinessObject;
using Microsoft.EntityFrameworkCore;
using DataAccess;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PayOSController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly LipstickManagementContext _context = new();
        public static string clientId { get; } = "80893f10-f2a8-4a3e-953e-40c19f5934d9";
        public static string apiKey { get; } = "67456449-bb0f-4e28-a56f-4cfb32926ffe";
        public static string checksumKey { get; } = "026c2980283435d1f448f15db5f9b71e46b586429ce64a862b1c6234ef043363";

        public PayOSController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("payment/payos")]
        public async Task<IActionResult> Create([FromBody] OrderDetail order)
        {
            // Keep your PayOS key protected by including it by an env variable
            
            var paymentLinkRequest = new PaymentData(
               order.Id,
               (int) order.TotalPrice,
               description: "Thanh toan don hang",
               items: [new(order.Lipstick.Name, (int) order.Lipstick.Price, 2000)],
               returnUrl: "/success.html",
               cancelUrl: "/cancel.html"
           );
             
            PayOS payOS = new PayOS(clientId, apiKey, checksumKey);


            
            try
            {
                CreatePaymentResult result = await payOS.createPaymentLink(paymentLinkRequest);
                return Ok(result.qrCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("payment/payos/order-cancel")]
        public async Task<IActionResult> CancelOrder([FromBody] OrderDetail order)
        {
            PayOS payOS = new PayOS(clientId, apiKey, checksumKey);
            // If you want to cancel the payment link without reason:
            try
            {
                var orderDetail = await _context.OrderDetails.SingleOrDefaultAsync(x => x.Id == order.Id); // Giả sử bạn lấy thông tin order từ database

                if (orderDetail == null)
                {
                    return NotFound(); // Hoặc xử lý trường hợp không tìm thấy order
                }

                // Chờ cho đến khi tác vụ hoàn thành và gán kết quả cho biến
                PaymentLinkInformation cancelledPaymentLinkInfo = await payOS.cancelPaymentLink(orderDetail.Id);

                // ... xử lý kết quả ...
                return Ok(cancelledPaymentLinkInfo);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return BadRequest(ex.Message);
            }
        }
    }
}

