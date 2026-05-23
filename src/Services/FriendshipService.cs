using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;
using nebula.api.src.Hubs;

namespace nebula.api.src.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly NebulaDbContext _context;
        private readonly IHubContext<ChatHub> _hub;

        public FriendshipService(NebulaDbContext context, IHubContext<ChatHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        public async Task<List<FriendDto>> GetFriends(Guid userId)
        {
            return await _context.Friendships
                .AsNoTracking()
                .Where(f => (f.RequesterId == userId || f.ReceiverId == userId) && f.Status == "accepted")
                .Select(f => new FriendDto
                {
                    Id = (f.RequesterId == userId ? f.Receiver.Id : f.Requester.Id).ToString(),
                    Username = f.RequesterId == userId ? f.Receiver.Username : f.Requester.Username,
                    DisplayName = f.RequesterId == userId ? f.Receiver.DisplayName : f.Requester.DisplayName,
                    Avatar = f.RequesterId == userId ? f.Receiver.Avatar : f.Requester.Avatar,
                    Status = "accepted",
                    IsRequester = f.RequesterId == userId
                })
                .ToListAsync();
        }

        public async Task<List<FriendDto>> GetPendingRequests(Guid userId)
        {
            return await _context.Friendships
                .AsNoTracking()
                .Where(f => f.ReceiverId == userId && f.Status == "pending")
                .Select(f => new FriendDto
                {
                    Id = f.Requester.Id.ToString(),
                    Username = f.Requester.Username,
                    DisplayName = f.Requester.DisplayName,
                    Avatar = f.Requester.Avatar,
                    Status = "pending",
                    IsRequester = false
                })
                .ToListAsync();
        }

        public async Task SendRequest(Guid requesterId, Guid receiverId)
        {
            if (requesterId == receiverId)
                throw new InvalidOperationException("Você não pode adicionar a si mesmo.");

            if (!await _context.Users.AnyAsync(u => u.Id == receiverId))
                throw new KeyNotFoundException("Usuário não encontrado.");

            var existing = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.RequesterId == requesterId && f.ReceiverId == receiverId) ||
                    (f.RequesterId == receiverId && f.ReceiverId == requesterId));

            if (existing is not null)
                throw new InvalidOperationException("Solicitação já existe ou vocês já são amigos.");

            var now = DateTime.UtcNow;
            _context.Friendships.Add(new FriendshipEntity
            {
                Id = Guid.NewGuid(),
                RequesterId = requesterId,
                ReceiverId = receiverId,
                Status = "pending",
                CreatedAt = now,
                UpdatedAt = now
            });

            await _context.SaveChangesAsync();

            var requester = await _context.Users.FindAsync(requesterId);
            if (requester is not null)
            {
                var dto = new FriendDto
                {
                    Id = requester.Id.ToString(),
                    Username = requester.Username,
                    DisplayName = requester.DisplayName,
                    Avatar = requester.Avatar,
                    Status = "pending",
                    IsRequester = false
                };
                await _hub.Clients.Group(receiverId.ToString()).SendAsync("FriendRequestReceived", dto);
            }
        }

        public async Task AcceptRequest(Guid userId, Guid requesterId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.RequesterId == requesterId && f.ReceiverId == userId && f.Status == "pending")
                ?? throw new KeyNotFoundException("Solicitação não encontrada.");

            friendship.Status = "accepted";
            friendship.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var accepter = await _context.Users.FindAsync(userId);
            if (accepter is not null)
            {
                var dto = new FriendDto
                {
                    Id = accepter.Id.ToString(),
                    Username = accepter.Username,
                    DisplayName = accepter.DisplayName,
                    Avatar = accepter.Avatar,
                    Status = "accepted",
                    IsRequester = false
                };
                await _hub.Clients.Group(requesterId.ToString()).SendAsync("FriendRequestAccepted", dto);
            }
        }

        public async Task RemoveFriend(Guid userId, Guid friendId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f =>
                    (f.RequesterId == userId && f.ReceiverId == friendId) ||
                    (f.RequesterId == friendId && f.ReceiverId == userId));

            if (friendship is null) return;

            _context.Friendships.Remove(friendship);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FriendDto>> SearchUsers(string query, Guid currentUserId)
        {
            var term = query.Trim().ToLowerInvariant();
            if (term.Length < 2)
                return [];

            var users = await _context.Users
                .AsNoTracking()
                .Where(u => u.Id != currentUserId &&
                    (u.Email.ToLower().Contains(term) ||
                     u.Username.ToLower().Contains(term) ||
                     u.DisplayName.ToLower().Contains(term)))
                .Take(10)
                .Select(u => new { u.Id, u.Username, u.DisplayName, u.Avatar })
                .ToListAsync();

            if (users.Count == 0) return [];

            var userIds = users.Select(u => u.Id).ToList();

            var friendships = await _context.Friendships
                .AsNoTracking()
                .Where(f =>
                    (f.RequesterId == currentUserId && userIds.Contains(f.ReceiverId)) ||
                    (f.ReceiverId == currentUserId && userIds.Contains(f.RequesterId)))
                .Select(f => new { f.RequesterId, f.ReceiverId, f.Status })
                .ToListAsync();

            return users.Select(u =>
            {
                var fs = friendships.FirstOrDefault(f =>
                    f.RequesterId == u.Id || f.ReceiverId == u.Id);

                var status = "none";
                var isRequester = false;

                if (fs is not null)
                {
                    if (fs.Status == "accepted")
                    {
                        status = "accepted";
                    }
                    else if (fs.Status == "pending")
                    {
                        status = fs.RequesterId == currentUserId ? "pending_sent" : "pending_received";
                        isRequester = fs.RequesterId == currentUserId;
                    }
                }

                return new FriendDto
                {
                    Id = u.Id.ToString(),
                    Username = u.Username,
                    DisplayName = u.DisplayName,
                    Avatar = u.Avatar,
                    Status = status,
                    IsRequester = isRequester
                };
            }).ToList();
        }
    }
}
