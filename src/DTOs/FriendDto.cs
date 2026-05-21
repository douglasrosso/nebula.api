namespace nebula.api.src.DTOs
{
    public class FriendDto
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsRequester { get; set; }
    }
}
