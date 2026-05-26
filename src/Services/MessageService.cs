using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Hubs;

namespace nebula.api.src.Services
{
    public class MessageService : IMessageService
    {
        private readonly NebulaDbContext _context;
        private readonly IHubContext<ChatHub> _hub;

        public MessageService(NebulaDbContext context, IHubContext<ChatHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<List<MessageDto>> GetConversation(Guid userId, Guid friendId, int page = 1, int pageSize = 50)
        {
            return await _context.Messages
                .AsNoTracking()
                .Where(m =>
                    (m.SenderId == userId && m.ReceiverId == friendId) ||
                    (m.SenderId == friendId && m.ReceiverId == userId))
                .OrderByDescending(m => m.SentAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(m => m.SentAt)
                .Select(m => new MessageDto
                {
                    Id = m.Id.ToString(),
                    SenderId = m.SenderId.ToString(),
                    ReceiverId = m.ReceiverId.ToString(),
                    Content = m.Content,
                    IsRead = m.IsRead,
                    SentAt = m.SentAt
                })
                .ToListAsync();
        }

        public async Task<MessageDto> SendMessage(Guid senderId, Guid receiverId, string content)
        {
            var now = DateTime.UtcNow;
            var msg = new MessageEntity
            {
                Id = Guid.NewGuid(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                SentAt = now,
                CreatedAt = now,
                UpdatedAt = now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            return new MessageDto
            {
                Id = msg.Id.ToString(),
                SenderId = msg.SenderId.ToString(),
                ReceiverId = msg.ReceiverId.ToString(),
                Content = msg.Content,
                IsRead = msg.IsRead,
                SentAt = msg.SentAt
            };
        }

        public async Task MarkAsRead(Guid userId, Guid senderId)
        {
            var affected = await _context.Messages
                .Where(m => m.SenderId == senderId && m.ReceiverId == userId && !m.IsRead)
                .ExecuteUpdateAsync(s => s.SetProperty(m => m.IsRead, true));

            if (affected > 0)
                await _hub.Clients.Group(senderId.ToString()).SendAsync("MessagesRead", userId.ToString());
        }
    }
}
