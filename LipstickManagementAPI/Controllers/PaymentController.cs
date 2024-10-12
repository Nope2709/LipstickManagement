using DataAccess.DTO.RequestModel;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Newtonsoft.Json;
using System.Text;
using HotPotToYou.Controllers.ResponseType;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PaymentController : ControllerBase
        {
            private readonly IHttpClientFactory _httpClientFactory;
        public static string clientId { get; } = "80893f10-f2a8-4a3e-953e-40c19f5934d9";
        public static string apiKey { get; } = "67456449-bb0f-4e28-a56f-4cfb32926ffe";
        public static string checksumKey { get; } = "026c2980283435d1f448f15db5f9b71e46b586429ce64a862b1c6234ef043363";

        public PaymentController(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            [HttpPost("payment")]
            public async Task<IActionResult> GenerateQRLink([FromBody] VietQRRequest order)
            {
                // Tạo request body theo format yêu cầu
                var requestBody = new
                {
                    accountNo = "19037808367019",
                    accountName = "DINH HOANG DUONG",
                    acqId = 970407,
                    amount = order.TotalPrice,
                    addInfo = "Thanh toan don hang",
                    format = "text",
                    template = "compact"
                };

                // Chuyển đối tượng thành JSON
                var jsonContent = JsonConvert.SerializeObject(requestBody);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                try
                {
                    // Tạo HttpClient và gửi request
                    var client = _httpClientFactory.CreateClient();
                    var response = await client.PostAsync("https://api.vietqr.io/v2/generate", httpContent);

                    // Đọc nội dung phản hồi
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Trả về kết quả OK với nội dung phản hồi
                    return Ok(responseContent);
                }
                catch (Exception ex)
                {
                    // Trả về lỗi nếu có vấn đề
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
        [HttpPost("payos_transfer_handler")]
        public IActionResult payOSTransferHandler(WebhookType body)
        {
            try
            {
                PayOS payOS = new PayOS(clientId, apiKey, checksumKey);
                var data = payOS.verifyPaymentWebhookData(body);

                if (data.description == "Ma giao dich thu nghiem" || data.description == "VQRIO123")
                {
                    return Ok(new JsonResponse<string>(null));
                }
                return Ok(new JsonResponse<string>(null));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500, $"Internal server error: {e.Message}");
            }

        }
    }

    }

