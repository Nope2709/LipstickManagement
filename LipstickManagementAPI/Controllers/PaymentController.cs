using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class PaymentController : ControllerBase
        {
            private readonly IHttpClientFactory _httpClientFactory;

            public PaymentController(IHttpClientFactory httpClientFactory)
            {
                _httpClientFactory = httpClientFactory;
            }

            [HttpPost("payment")]
            public async Task<IActionResult> GenerateQRLink()
            {
                // Tạo request body theo format yêu cầu
                var requestBody = new
                {
                    accountNo = "19037808367019",
                    accountName = "DINH HOANG DUONG",
                    acqId = 970407,
                    amount = 10000,
                    addInfo = "Kiem tra ma qr thanh toan",
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
        }

    }

