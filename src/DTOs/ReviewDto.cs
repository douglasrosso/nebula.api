namespace nebula.api.src.DTOs
{
    public class ReviewDto
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string? UserAvatar { get; set; }
        public string Rating { get; set; } = string.Empty;
        public int HoursPlayed { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Date { get; set; } = string.Empty;
        public int Helpful { get; set; }
        public int Funny { get; set; }
    }
}
