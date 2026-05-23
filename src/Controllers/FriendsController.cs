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
    public class FriendsController : AuthorizedController
    {
        private readonly IFriendshipService _service;

        public FriendsController(IFriendshipService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<FriendDto>>> GetFriends()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetFriends(userId.Value));
        }

        [HttpGet("requests")]
        public async Task<ActionResult<List<FriendDto>>> GetPendingRequests()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetPendingRequests(userId.Value));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<FriendDto>>> Search([FromQuery] string q = "")
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.SearchUsers(q, userId.Value));
        }

        [HttpPost("{targetUserId:guid}")]
        public async Task<IActionResult> SendRequest(Guid targetUserId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            await _service.SendRequest(userId.Value, targetUserId);
            return NoContent();
        }

        [HttpPut("{requesterId:guid}/accept")]
        public async Task<IActionResult> AcceptRequest(Guid requesterId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            await _service.AcceptRequest(userId.Value, requesterId);
            return NoContent();
        }

        [HttpDelete("{friendId:guid}")]
        public async Task<IActionResult> RemoveFriend(Guid friendId)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            await _service.RemoveFriend(userId.Value, friendId);
            return NoContent();
        }
    }
}
