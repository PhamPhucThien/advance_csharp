using advance_csharp.dto.Request.Carts;
using advance_csharp.dto.Response.Carts;

namespace advance_csharp.service.Interfaces
{
    public interface ICartService
    {
        Task<ResponseChangeOnCart> Add(string username, RequestModifyCart requestModifyCart);
        Task<ResponseGetCart> Get(string username);
        Task<ResponseChangeOnCart> Update(string username, RequestModifyCart requestModifyCart);
        Task<ResponseChangeOnCart> Delete(string username, Guid ProductId);
    }
}
