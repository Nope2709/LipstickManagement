using System;

namespace HotPotToYou.Service.VNPay
{
    public class VNPayConfig
    {
        public static string VnpPayUrl { get; } = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        public static string VnpReturnUrl { get; } = "http://localhost:8088/payment/return"; // Replace with your actual return URL after successful payment
        public static string VnpTmnCode { get; } = "P8Y3QRZ3"; // Replace with your VNP_TmnCode
        public static string SecretKey { get; } = "BDH8UD3Z9R70XJLIE5DGLLVNMOZFJTH2";
        public string VnpVersion { get; set; } = "2.1.0";
        public string VnpCommand { get; set; } = "pay";
        public string OrderType { get; set; } = "your_order_type";
    }
}
