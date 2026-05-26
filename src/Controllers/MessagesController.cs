using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nebula.api.src.Common.Controllers;
using nebula.api.src.DTOs;
using nebula.api.src.Services;

namespace nebula.api.src.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : AuthorizedController
    {
        private readonly IMessageService _service;

        public MessagesController(IMessageService service)
        {
            _service = service;
        }

        [HttpGet("{friendId:guid}")]
        public async Task<ActionResult<List<MessageDto>>> GetConversation(Guid friendId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetConversation(userId.Value, friendId, page, pageSize));
        }

        [HttpPut("{friendId:guid}/read")]
        public async Task<IActionResult> MarkRead(Guid friendId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            await _service.MarkAsRead(userId.Value, friendId);
            return NoContent();
        }

        [HttpPost("{friendId:guid}")]
        public async Task<ActionResult<MessageDto>> SendMessage(Guid friendId, [FromBody] SendMessageDto dto)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            var message = await _service.SendMessage(userId.Value, friendId, dto.Content.Trim());
            return Ok(message);
        }
    }
}
