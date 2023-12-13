using advance_csharp.database.Models;

namespace advance_csharp.dto.Response.Products
{
    public class ResponseGetProductBySearch
    {
        public List<ProductModelBySearch> Data { get; set; } = new();
    }

    public class ProductModelBySearch
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public string Unit { get; set; } = "VND";
        public string? Images { get; set; }
        public string Category { get; set; } = string.Empty;
    }
}
