using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class ReviewEntity : BaseEntity
    {
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public bool IsPositive { get; set; }
        public int HoursPlayed { get; set; }
        public string Content { get; set; } = string.Empty;
        public int HelpfulCount { get; set; }
        public int FunnyCount { get; set; }

        public GameEntity Game { get; set; } = null!;
        public UserEntity User { get; set; } = null!;
    }
}
