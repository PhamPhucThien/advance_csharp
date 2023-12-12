using advance_csharp.dto.Request.Accounts;
using advance_csharp.dto.Response.Accounts;
using advance_csharp.Extensions;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace advance_csharp.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _AccountService;
        private readonly ILoggingService _LoggingService;
        public AccountController()
        {
            _AccountService = new AccountService();
            _LoggingService = new LoggingService();
        }

        [Route("get-account-info")]
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Get()
        {
            try
            {
                ResponseGetAccount response = new();
                string username = HttpContext.GetName();
                response = await _AccountService.Get(username);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            } catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("get-account-by-id")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromQuery] Guid Id)
        {
            try
            {
                ResponseGetAccountById response = new();
                response = await _AccountService.GetById(Id);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("update-account")]
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update([FromBody] RequestUpdateAccount request)
        {
            try
            {
                ResponseUpdateAccount response = new();
                string username = HttpContext.GetName();
                response = await _AccountService.Update(username, request);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }

        [Route("delete-account-by-id")]
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete([FromQuery] Guid Id)
        {
            try
            {
                ResponseDeleteAccount response = new();
                response = await _AccountService.Delete(Id);
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
