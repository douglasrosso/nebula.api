using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class OrderEntity : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "completed";

        public UserEntity User { get; set; } = null!;
        public ICollection<OrderItemEntity> Items { get; set; } = [];
    }
}
