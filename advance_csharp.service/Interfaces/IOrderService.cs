using advance_csharp.dto.Response.Orders;

namespace advance_csharp.service.Interfaces
{
    public interface IOrderService
    {
        Task<ResponseAddOrder> Add(string username);
        Task<ResponseGetOrder> Get(string username);
        Task<ResponseGetOrderById> GetById(string username, Guid Id);
    }
}
