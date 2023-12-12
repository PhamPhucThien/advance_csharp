using advance_csharp.database.Models;

namespace advance_csharp.dto.Response.Products
{
    public class ResponseGetProductBySearch
    {
        public List<Product> Data { get; set; } = new();
    }
}
