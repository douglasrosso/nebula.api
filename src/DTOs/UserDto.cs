namespace nebula.api.src.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public int FriendCount { get; set; }
        public int GamesOwned { get; set; }
        public string[] Badges { get; set; } = [];
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
