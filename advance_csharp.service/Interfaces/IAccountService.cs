using advance_csharp.dto.Request.Accounts;
using advance_csharp.dto.Response.Accounts;

namespace advance_csharp.service.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseGetAccount> Get(string username);
        Task<ResponseGetAccountById> GetById(Guid Id);
        Task<ResponseUpdateAccount> Update(string username, RequestUpdateAccount requestUpdateAccount);
        Task<ResponseDeleteAccount> Delete(Guid Id);
    }
}
