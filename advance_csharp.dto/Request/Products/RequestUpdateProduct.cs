namespace advance_csharp.dto.Request.Products
{
    public class RequestUpdateProduct
    {
        public class UpdateProductModel
        {
            public Guid Id { get; set; }
            public string? Name { get; set; }
            public string? Price { get; set; }
            public int? Quantity { get; set; }
            public string? Images { get; set; }
            public string? Category { get; set; }
            public bool? IsAvailable { get; set; }
        }
        public List<UpdateProductModel> Data { get; set; } = new();
    }
}
