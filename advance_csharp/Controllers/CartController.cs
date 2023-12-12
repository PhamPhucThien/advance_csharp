using advance_csharp.dto.Request.Carts;
using advance_csharp.dto.Response.Carts;
using advance_csharp.Extensions;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace advance_csharp.Controllers
{
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService _CartService;
        private readonly ILoggingService _LoggingService;

        public CartController()
        {
            _CartService = new CartService();
            _LoggingService = new LoggingService();
        }

        [Route("get-cart-info")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseGetCart response = new();
                string username = HttpContext.GetName();
                response = await _CartService.Get(username);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("add-product-to-cart")]
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Add([FromBody] RequestModifyCart request)
        {
            try
            {
                ResponseChangeOnCart response = new();
                string username = HttpContext.GetName();
                response = await _CartService.Add(username, request);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("update-product-quantity-to-cart")]
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update([FromBody] RequestModifyCart request)
        {
            try
            {
                ResponseChangeOnCart response = new();
                string username = HttpContext.GetName();
                response = await _CartService.Update(username, request);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("delete-product-on-cart")]
        [HttpDelete]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            try
            {
                ResponseChangeOnCart response = new();
                string username = HttpContext.GetName();
                response = await _CartService.Delete(username, Id);
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
