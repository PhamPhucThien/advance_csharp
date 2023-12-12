using System.ComponentModel.DataAnnotations.Schema;

namespace advance_csharp.database.Models
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Column("order_record")]
        public string OrderRecord { get; set; } = string.Empty;

        [Column("total_price")]
        public long TotalPrice { get; set; }

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
