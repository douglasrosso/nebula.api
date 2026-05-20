namespace nebula.api.src.Entities
{
    public class OrderItemEntity
    {
        public Guid OrderId { get; set; }
        public Guid GameId { get; set; }
        public decimal PricePaid { get; set; }

        public OrderEntity Order { get; set; } = null!;
        public GameEntity Game { get; set; } = null!;
    }
}
