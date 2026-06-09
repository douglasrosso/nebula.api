using nebula.api.src.Common.Entities;

namespace nebula.api.src.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public int Level { get; set; } = 1;
        public int Xp { get; set; } = 0;
        public string? Country { get; set; }
        public string? Bio { get; set; }
        public int FriendCount { get; set; } = 0;
        public string[] Badges { get; set; } = [];

        public ICollection<ReviewEntity> Reviews { get; set; } = [];
        public ICollection<UserLibraryEntity> Library { get; set; } = [];
        public ICollection<WishlistItemEntity> Wishlist { get; set; } = [];
        public ICollection<FriendshipEntity> SentRequests { get; set; } = [];
        public ICollection<FriendshipEntity> ReceivedRequests { get; set; } = [];
        public ICollection<MessageEntity> SentMessages { get; set; } = [];
        public ICollection<MessageEntity> ReceivedMessages { get; set; } = [];
    }
}
