namespace advance_csharp.dto.Response.Orders
{
    public class ResponseGetOrder
    {
        public List<OrderModel> Data { get; set; } = new List<OrderModel>();
    }

    public class OrderModel
    {
        public Guid Id { get; set; }
        public long TotalPrice { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
