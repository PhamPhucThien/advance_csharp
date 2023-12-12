using advance_csharp.dto.Response.Orders;
using advance_csharp.Extensions;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace advance_csharp.Controllers
{
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _OrderService;
        private readonly ILoggingService _LoggingService;

        public OrderController()
        {
            _OrderService = new OrderService();
            _LoggingService = new LoggingService();
        }

        [Route("get-order-info")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseGetOrder response = new();
                string username = HttpContext.GetName();
                response = await _OrderService.Get(username);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("add-order")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Add()
        {
            try
            {
                ResponseAddOrder response = new();
                string username = HttpContext.GetName();
                response = await _OrderService.Add(username);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("get-order-by-id")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetById([FromQuery] Guid Id) 
        {
            try
            {
                ResponseGetOrderById response = new();
                string username = HttpContext.GetName();
                response = await _OrderService.GetById(username, Id);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }
    }
}
