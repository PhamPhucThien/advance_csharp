using advance_csharp.dto.Request.Products;
using advance_csharp.dto.Response.Products;

namespace advance_csharp.service.Interfaces
{
    public interface IProductService
    {
        Task<ResponseGetProductBySearch> GetBySearch(string? search, int page, int size);
        Task<ResponseGetProductById> GetById(Guid id);
        Task<ResponseUpdateProduct> Update(RequestUpdateProduct requestUpdateProduct);
        Task<ResponseAddProduct> Add(RequestAddProduct requestAddProduct);
    }
}
