using nebula.api.src.DTOs;

namespace nebula.api.src.Services
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetConversation(Guid userId, Guid friendId, int page = 1, int pageSize = 50);
        Task<MessageDto> SendMessage(Guid senderId, Guid receiverId, string content);
        Task MarkAsRead(Guid userId, Guid senderId);
    }
}
