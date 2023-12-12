using System.ComponentModel.DataAnnotations.Schema;

namespace advance_csharp.database.Models
{
    [Table("Account")]

    public class Account : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("role")]
        public string Role { get; set; } = string.Empty;

        [Column("is_available")]
        public bool IsAvailable { get; set; } = true;
    }
}
