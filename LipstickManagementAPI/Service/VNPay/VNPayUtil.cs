using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace HotPotToYou.Service.VNPay
{
    public class VNPayUtil
    {
        private static readonly Random random = new Random();

        public static string HmacSHA512(string key, string data)
        {
            try
            {
                if (key == null || data == null)
                {
                    throw new ArgumentNullException();
                }

                using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
                {
                    byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string GetIpAddress(HttpContext context)
        {
            string ipAddress = context.Connection.RemoteIpAddress.ToString();
            if (context.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                ipAddress = context.Request.Headers["X-Forwarded-For"];
            }
            return ipAddress;
        }

        public static string GetRandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetPaymentURL(Dictionary<string, string> paramsMap, bool encodeKey)
        {
            var sortedParams = paramsMap.OrderBy(kv => kv.Key);

            var queryString = string.Join("&", sortedParams.Select(kv =>
                $"{(encodeKey ? Uri.EscapeDataString(kv.Key) : kv.Key)}={Uri.EscapeDataString(kv.Value)}"));

            return queryString;
        }

        public static Dictionary<string, string> ConvertParameterMap(Dictionary<string, string[]> parameterMap)
        {
            var paramsDict = new Dictionary<string, string>();
            foreach (var entry in parameterMap)
            {
                paramsDict[entry.Key] = entry.Value.FirstOrDefault();
            }
            return paramsDict;
        }

        public static string BuildHashData(Dictionary<string, string> paramsDict)
        {
            var hashData = new StringBuilder();
            foreach (var entry in paramsDict)
            {
                if (hashData.Length > 0)
                {
                    hashData.Append('&');
                }
                hashData.Append(entry.Key).Append('=').Append(entry.Value);
            }
            return hashData.ToString();
        }
    }
}
