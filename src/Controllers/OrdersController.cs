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
    public class OrdersController : AuthorizedController
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            return Ok(await _service.GetOrders(userId.Value));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderDto>> GetById(Guid id)
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            var order = await _service.GetOrderById(id, userId.Value);
            if (order is null) return NotFound(new { message = "Pedido não encontrado." });

            return Ok(order);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<OrderDto>> Checkout()
        {
            var userId = GetUserId();
            if (userId is null) return Unauthorized();

            try
            {
                var order = await _service.Checkout(userId.Value);
                return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
