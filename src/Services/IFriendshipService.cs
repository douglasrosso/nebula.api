using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IFriendshipService
    {
        Task<List<FriendDto>> GetFriends(Guid userId);
        Task<List<FriendDto>> GetPendingRequests(Guid userId);
        Task SendRequest(Guid requesterId, Guid receiverId);
        Task AcceptRequest(Guid userId, Guid requesterId);
        Task RemoveFriend(Guid userId, Guid friendId);
        Task<List<FriendDto>> SearchUsers(string query, Guid currentUserId);
    }
}
