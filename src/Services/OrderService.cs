using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Services
{
    public class OrderService : IOrderService
    {
        private readonly NebulaDbContext _context;
        private readonly ILibraryService _libraryService;
        private readonly ICartService _cartService;

        public OrderService(NebulaDbContext context, ILibraryService libraryService, ICartService cartService)
        {
            _context = context;
            _libraryService = libraryService;
            _cartService = cartService;
        }

        public async Task<List<OrderDto>> GetOrders(Guid userId)
        {
            var orders = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .ThenInclude(i => i.Game)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return orders.Select(MapOrderToDto).ToList();
        }

        public async Task<OrderDto?> GetOrderById(Guid orderId, Guid userId)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .ThenInclude(i => i.Game)
                .FirstOrDefaultAsync(o => o.Id == orderId && o.UserId == userId);

            return order is null ? null : MapOrderToDto(order);
        }

        public async Task<OrderDto> Checkout(Guid userId)
        {
            var cartItems = await _context.Cart
                .Include(c => c.Game)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (cartItems.Count == 0)
                throw new InvalidOperationException("O carrinho está vazio.");

            var total = cartItems.Sum(c => c.Game.Price);
            var now = DateTime.UtcNow;

            var order = new OrderEntity
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                TotalAmount = total,
                Status = "completed",
                CreatedAt = now,
                UpdatedAt = now,
                Items = cartItems.Select(c => new OrderItemEntity
                {
                    OrderId = Guid.Empty,
                    GameId = c.GameId,
                    PricePaid = c.Game.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.Cart.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            foreach (var item in cartItems)
                await _libraryService.AddToLibrary(userId, item.GameId, now);

            var saved = await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .ThenInclude(i => i.Game)
                .FirstAsync(o => o.Id == order.Id);

            return MapOrderToDto(saved);
        }

        private static OrderDto MapOrderToDto(OrderEntity order) => new()
        {
            Id = order.Id,
            TotalAmount = order.TotalAmount,
            Status = order.Status,
            CreatedAt = order.CreatedAt,
            Items = order.Items.Select(i => new OrderItemDto
            {
                GameId = i.GameId,
                Title = i.Game?.Title ?? string.Empty,
                CoverImage = i.Game?.CoverImage ?? string.Empty,
                PricePaid = i.PricePaid
            }).ToList()
        };
    }
}
