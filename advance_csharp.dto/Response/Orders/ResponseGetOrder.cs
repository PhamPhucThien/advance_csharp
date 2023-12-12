namespace advance_csharp.dto.Response.Orders
{
    public class ResponseGetOrder
    {
        public class OrderModel
        {
            public Guid Id { get; set; }
            public long TotalPrice { get; set; }
            public DateTimeOffset CreatedAt { get; set; }
        }
        public List<OrderModel> Data = new();
    }
}
