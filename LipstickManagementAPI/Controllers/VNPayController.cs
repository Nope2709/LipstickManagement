using HotPotToYou.Service.VNPay;
using Microsoft.AspNetCore.Mvc;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class VNPayController : ControllerBase
    {
       

        // VNPay configuration
        public static string VnpPayUrl { get; } = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public static string VnpReturnUrl { get; } = "https://localhost:7035/api/VNPay/handleCallback"; // Replace with your actual return URL after successful payment
        public static string VnpTmnCode { get; } = "QVZTXZMI"; // Replace with your actual VNPay TmnCode
        public static string SecretKey { get; } = "U4MOFMFC0IDFWSHM9U02MOT5ASUU5F1U";
        public string VnpVersion { get; set; } = "2.1.0";
        public string VnpCommand { get; set; } = "pay";
        public string OrderType { get; set; } = "other"; // Update to match your order type logic

        private IHttpContextAccessor _httpContextAccessor;

        public VNPayController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            
        }

        [HttpPost("vnpay-payment")]
        public IActionResult Payment([FromBody] VNPayPaymentRequest request)
        {
            string clientIPAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            PayLib pay = new PayLib();
            decimal amount = (decimal)(request.Price * 100);
            pay.AddRequestData("vnp_Version", VnpVersion);
            pay.AddRequestData("vnp_Command", VnpCommand);
            pay.AddRequestData("vnp_TmnCode", VnpTmnCode);
            pay.AddRequestData("vnp_Amount", amount.ToString());
            pay.AddRequestData("vnp_BankCode", "");
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", "VND");
            pay.AddRequestData("vnp_IpAddr", clientIPAddress);
            pay.AddRequestData("vnp_Locale", "vn");
            pay.AddRequestData("vnp_OrderInfo", request.OrderInfo);
            pay.AddRequestData("vnp_OrderType", OrderType);
            pay.AddRequestData("vnp_ReturnUrl", VnpReturnUrl);
            pay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString()); // Generate a unique transaction reference

            string paymentUrl = pay.CreateRequestUrl(VnpPayUrl, SecretKey);

            return Ok(new { PaymentUrl = paymentUrl });


        }

        [HttpGet("vnpay-handleCallback")]
        public IActionResult HandleCallback()
        {
            try
            {
                var request = _httpContextAccessor.HttpContext.Request;
                var status = request.Query["vnp_ResponseCode"].ToString();
                var paymentCode = request.Query["vnp_TxnRef"].ToString();


                if (status == "00")
                {
                    // Handle successful payment, update the order/payment status in your database
                    return Ok("Success");
                }
                else
                {
                    // Handle failed payment, update the order/payment status in your database
                    return Ok("Failed");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private bool ValidateSignature(string rspraw, string inputHash, string secretKey)
        {
            string myChecksum = PayLib.HmacSHA512(secretKey, rspraw);
            return myChecksum.Equals(inputHash, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public class VNPayPaymentRequest
    {
        public int Price { get; set; }
        public string OrderInfo { get; set; }
        // Add more properties as needed for your payment request
    }
}


