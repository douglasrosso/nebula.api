namespace nebula.api.src.DTOs
{
    public class LibraryItemDto
    {
        public Guid GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public string[] Genres { get; set; } = [];
        public decimal Rating { get; set; }
        public DateTime AcquiredAt { get; set; }
    }
}
