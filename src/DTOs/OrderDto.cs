namespace nebula.api.src.DTOs
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = [];
    }

    public class OrderItemDto
    {
        public Guid GameId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public decimal PricePaid { get; set; }
    }
}
