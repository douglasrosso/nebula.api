namespace nebula.api.src.Entities
{
    public class WishlistItemEntity
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public DateTime AddedAt { get; set; }

        public UserEntity User { get; set; } = null!;
        public GameEntity Game { get; set; } = null!;
    }
}
