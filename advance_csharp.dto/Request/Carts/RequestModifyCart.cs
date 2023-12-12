namespace advance_csharp.dto.Request.Carts
{
    public class RequestModifyCart
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
