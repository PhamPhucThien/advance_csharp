namespace advance_csharp.dto.Request.Products
{
    public class RequestAddProduct
    {
        public class AddProductModel
        {
            public string Name { get; set; } = string.Empty;
            public string Price { get; set; } = string.Empty;
            public int Quantity { get; set; } = 0;
            public string? Images { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
        }
        public List<AddProductModel> Data { get; set; } = new();
    }
}
