using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class MessageEntity : BaseEntity
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        public UserEntity Sender { get; set; } = null!;
        public UserEntity Receiver { get; set; } = null!;
    }
}
