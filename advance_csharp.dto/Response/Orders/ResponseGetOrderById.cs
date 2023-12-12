namespace advance_csharp.dto.Response.Orders
{
    public class ResponseGetOrderById
    {
        public class ProductInOrder
        {
            public string Id { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Price { get; set; } = string.Empty;
            public string Quantity { get; set; } = string.Empty;
        }
        public List<ProductInOrder> Data { get; set; } = new();
        public long Total { get; set; }
    }
}
