using System.ComponentModel.DataAnnotations.Schema;

namespace advance_csharp.database.Models
{
    [Table("Cart")]
    public class Cart : BaseEntity
    {
        [Column("account_id")]
        public Guid AccountId { get; set; }

        [Column("cart_record")]
        public string CartRecord { get; set; } = string.Empty;
    }
}
