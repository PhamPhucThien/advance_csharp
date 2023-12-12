using advance_csharp.dto.Request.Authentication;
using advance_csharp.dto.Response.Authentication;

namespace advance_csharp.service.Interfaces
{
    public interface IAuthenticationService
    {
        Task<ResponseLogin> Login(RequestLogin login);
        Task<ResponseRegister> Register(RequestRegister register);
    }
}
