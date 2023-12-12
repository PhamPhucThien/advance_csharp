using advance_csharp.dto.Request.Products;
using advance_csharp.dto.Response.Products;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace advance_csharp.Controllers
{
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _ProductService;
        private readonly ILoggingService _LoggingService;

        public ProductController()
        {
            _ProductService = new ProductService();
            _LoggingService = new LoggingService();
        }

        [Route("get-product-by-search")]
        [HttpGet]
        public async Task<IActionResult> GetBySearch([FromQuery] string? search, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                ResponseGetProductBySearch response = new();
                response = await _ProductService.GetBySearch(search, page, size);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("get-product-by-id")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            try
            {
                ResponseGetProductById response = new();
                response = await _ProductService.GetById(id);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("update-list-of-product")]
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] RequestUpdateProduct request)
        {
            try
            {
                ResponseUpdateProduct response = new();
                response = await _ProductService.Update(request);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }

        }

        [Route("add-list-of-product")]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] RequestAddProduct request)
        {
            try
            {
                ResponseAddProduct response = new();
                response = await _ProductService.Add(request);
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
