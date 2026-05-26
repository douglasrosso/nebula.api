namespace nebula.api.src.DTOs
{
    public class CartItemDto
    {
        public Guid GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public int? Discount { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
