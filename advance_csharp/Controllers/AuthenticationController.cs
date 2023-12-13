using advance_csharp.dto.Request.Authentication;
using advance_csharp.dto.Response.Authentication;
using advance_csharp.Extensions;
using advance_csharp.service.Interfaces;
using advance_csharp.service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace advance_csharp.Controllers
{
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _AuthenticationService;
        private readonly ILoggingService _LoggingService;
        public AuthenticationController()
        {
            ConfigurationBuilder configurationBuilder = new();

            _ = configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder.Build();
            _AuthenticationService = new AuthenticationService(configuration);
            _LoggingService = new LoggingService();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(RequestLogin request)
        {
            try
            {
                ResponseLogin response = new();
                if (HttpContext.GetName() == string.Empty) response = await _AuthenticationService.Login(request);
                _LoggingService.LogInfo(JsonSerializer.Serialize(response));
                return new JsonResult(response);
            }
            catch (Exception ex)
            {
                _LoggingService.LogError(ex);
                return StatusCode(500, ex.Message);
            }
        }



        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RequestRegister request)
        {
            try
            {
                ResponseRegister response = new();
                if (HttpContext.GetName() == string.Empty) response = await _AuthenticationService.Register(request);
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
