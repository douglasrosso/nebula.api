using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class FriendshipEntity : BaseEntity
    {
        public Guid RequesterId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Status { get; set; } = "pending"; // pending | accepted

        public UserEntity Requester { get; set; } = null!;
        public UserEntity Receiver { get; set; } = null!;
    }
}
