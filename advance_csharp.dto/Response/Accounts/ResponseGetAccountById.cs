namespace advance_csharp.dto.Response.Accounts
{
    public class ResponseGetAccountById
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public bool IsAvailable { get; set; } = true;
    }
}
