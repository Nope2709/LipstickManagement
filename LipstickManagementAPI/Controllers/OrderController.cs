using DataAccess;
using DataAccess.DTO.RequestModel;
using DataAccess.DTO.ResponseModel;
using HotPotToYou.Controllers.ResponseType;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
using Repositories.Repositories.Orders;
using Microsoft.EntityFrameworkCore;

namespace LipstickManagementAPI.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderService;
        public static string clientId { get; } = "80893f10-f2a8-4a3e-953e-40c19f5934d9";
        public static string apiKey { get; } = "67456449-bb0f-4e28-a56f-4cfb32926ffe";
        public static string checksumKey { get; } = "026c2980283435d1f448f15db5f9b71e46b586429ce64a862b1c6234ef043363";
        private readonly LipstickManagementContext _context = new();

        public OrderController(IOrderRepository orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestModel order)
        {
    
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _orderService.CreateOrder(order);
                if (order.PaymentId == 1)
                {
                    PayOS payOS = new PayOS(clientId, apiKey, checksumKey);
                    foreach (var i in order.Items)
                    {
                        var product = _context.Lipsticks.SingleOrDefault(x => x.Id == i.Id);
                        ItemData item = new ItemData(product.Name, i.Quantity ?? default(int), (int)order.TotalPrice);
                        List<ItemData> items = new List<ItemData>();
                        items.Add(item);
                        PaymentData paymentLinkRequest = new PaymentData(int.Parse(DateTimeOffset.Now.ToString("ffffff")), 1000, "Thanh toan don hang", items, "", "");
                        try
                        {

                            CreatePaymentResult resultPayos = await payOS.createPaymentLink(paymentLinkRequest);


                            // Trả về kết quả cho Frontend
                            return Ok(new JsonResponse<CreatePaymentResult>(resultPayos));
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }



                }
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new JsonResponse<string>(ex.Message));
            }
        }
        [HttpGet("order/payOS/{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            PayOS payOS = new PayOS(clientId, apiKey, checksumKey);
            try
            {
                PaymentLinkInformation paymentLinkInformation = await payOS.getPaymentLinkInformation(orderId);
                return Ok(new JsonResponse<PaymentLinkInformation>(paymentLinkInformation));
            }
            catch (Exception ex)
            {

                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
        [HttpPut("order/payOS/{orderId}")]
        public async Task<IActionResult> CancelOrder([FromRoute] int orderId)
        {
            try
            {
                PayOS payOS = new PayOS(clientId, apiKey, checksumKey);
                PaymentLinkInformation paymentLinkInformation = await payOS.cancelPaymentLink(orderId);
                return Ok(new JsonResponse<PaymentLinkInformation>(paymentLinkInformation));
            }
            catch (Exception ex)
            {

                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
        [HttpPost("order/payOS/confirm-webhook")]
        public async Task<IActionResult> ConfirmWebhook(ConfirmWebhook body)
        {
            PayOS payOS = new PayOS(clientId, apiKey, checksumKey);
            try
            {
                var result= await payOS.confirmWebhook(body.Url);
                return Ok(new JsonResponse<string>(result));
            }
            catch (Exception ex)
            {

                return BadRequest(new JsonResponse<string>(ex.Message));
            }

        }
        //[HttpPut("order")]
        //public async Task<IActionResult> UpdateOrder(int id, [FromBody] UpdateOrderRequestModel order)
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest(ModelState);

        //        if (id != order.ID)
        //            return BadRequest("Order ID mismatch");

        //        var result = await _orderService.UpdateOrder(order);
        //        return Ok(new JsonResponse<string>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}

        //[HttpDelete("order")]
        //public async Task<IActionResult> DeleteOrder(int id)
        //{
        //    try
        //    {
        //        var result = await _orderService.DeleteOrder(id);
        //        return Ok(new JsonResponse<string>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}

        //[HttpGet("get-wait-for-pay-orders")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetWaitForPayOrders(string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetWaitForPayOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }

        //}
        //[HttpGet("get-pending-orders")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetPendingOrders(string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetPendingOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-in-process-orders")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetInProcessOrders(string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetInProcessOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-delivered-orders")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetDeliveredOrders(string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetDeliveredOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-canceled-orders")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetCanceledOrders(string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetCanceledOrders(search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-pending-orders-by-customer-id")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetPendingOrdersByCustomerID(int customerID, string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetPendingOrdersByCustomerID(customerID, search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-in-process-orders-by-customer-id")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetInProcessOrdersByCustomerID(int customerID, string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetInProcessOrdersByCustomerID(customerID, search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-delivered-orders-by-customer-id")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetDeliveredOrdersByCustomerID(int customerID, string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetDeliveredOrdersByCustomerID(customerID, search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-canceled-orders-by-customer-id")]
        //public async Task<ActionResult<List<JsonResponse<OrderResponseModel>>>> GetCanceledOrdersByCustomerID(int customerID, string? search, string? sortBy,
        //    DateTime? fromDate, DateTime? toDate, int pageIndex, int pageSize)
        //{
        //    try
        //    {
        //        var orders = await _orderService.GetCanceledOrdersByCustomerID(customerID, search, sortBy, fromDate, toDate, pageIndex, pageSize);
        //        return Ok(new JsonResponse<List<OrderResponseModel>>(orders));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
        //[HttpGet("get-order-by-id")]
        //public async Task<ActionResult<JsonResponse<OrderDetailResponseModel>>> GetOrderByID(int id)
        //{
        //    try
        //    {
        //        var order = await _orderService.GetOrderByID(id);
        //        if (order == null)
        //            return NotFound("Order not found");

        //        return Ok(new JsonResponse<OrderDetailResponseModel>(order));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}

        //[HttpPut("update-order-to-in-process")]
        //public async Task<ActionResult<JsonResponse<string>>> UpdateOrderToInProcess(int id)
        //{
        //    try
        //    {
        //        var result = await _orderService.UpdateOrderToInProcess(id);
        //        return Ok(new JsonResponse<string>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}

        //[HttpPut("update-order-to-delivered")]
        //public async Task<ActionResult<JsonResponse<string>>> UpdateOrderToDelivered(int id)
        //{
        //    try
        //    {
        //        var result = await _orderService.UpdateOrderToDelivered(id);
        //        return Ok(new JsonResponse<string>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}

        //[HttpPut("update-order-to-canceled")]
        //public async Task<ActionResult<JsonResponse<string>>> UpdateOrderToCanceled(int id)
        //{
        //    try
        //    {
        //        var result = await _orderService.UpdateOrderToCanceled(id);
        //        return Ok(new JsonResponse<string>(result));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new JsonResponse<string>(ex.Message));
        //    }
        //}
    }
}
