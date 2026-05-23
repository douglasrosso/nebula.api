using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using nebula.api.src.Services;

namespace nebula.api.src.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            var userId = GetUserId();
            if (userId is not null)
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = GetUserId();
            if (userId is not null)
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);

            await base.OnDisconnectedAsync(exception);
        }

        public async Task SendMessage(string receiverId, string content)
        {
            var senderIdStr = GetUserId();
            if (senderIdStr is null) return;

            if (string.IsNullOrWhiteSpace(content)) return;

            if (!Guid.TryParse(receiverId, out var receiverGuid)) return;
            if (!Guid.TryParse(senderIdStr, out var senderGuid)) return;

            var message = await _messageService.SendMessage(senderGuid, receiverGuid, content.Trim());

            await Clients.Group(receiverId).SendAsync("ReceiveMessage", message);
            await Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        private string? GetUserId() =>
            Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
