namespace advance_csharp.dto.Response.Carts
{
    public class ResponseGetCart
    {
        public class ProductModelForCart
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Price { get; set; } = string.Empty;
            public int Quantity { get; set; } = 0;
            public string Unit { get; set; } = "VND";
            public string? Images { get; set; }
            public string Category { get; set; } = string.Empty;
        }

        public List<ProductModelForCart> Data { get; set; } = new();
    }
}
