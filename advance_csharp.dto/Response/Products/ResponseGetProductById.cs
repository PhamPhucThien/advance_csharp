using advance_csharp.database.Models;

namespace advance_csharp.dto.Response.Products
{
    public class ResponseGetProductById
    {
        public Product Data { get; set; } = new();
    }
}
