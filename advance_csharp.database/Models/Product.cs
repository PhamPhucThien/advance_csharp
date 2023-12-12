using System.ComponentModel.DataAnnotations.Schema;

namespace advance_csharp.database.Models
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("price")]
        public string Price { get; set; } = string.Empty;

        [Column("quantity")]
        public int Quantity { get; set; } = 0;

        [Column("unit")]
        public string Unit { get; set; } = "VND";

        [Column("images")]
        public string? Images { get; set; }

        [Column("category")]
        public string Category { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

        [Column("is_available")]
        public bool IsAvailable { get; set; }
    }
}
