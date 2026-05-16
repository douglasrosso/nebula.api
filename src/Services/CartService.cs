using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Services
{
    public class CartService : ICartService
    {
        private readonly NebulaDbContext _context;

        public CartService(NebulaDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItemDto>> GetCart(Guid userId)
        {
            return await _context.Cart
                .AsNoTracking()
                .Include(c => c.Game)
                .Where(c => c.UserId == userId)
                .Select(c => new CartItemDto
                {
                    GameId = c.GameId,
                    Title = c.Game.Title,
                    CoverImage = c.Game.CoverImage,
                    Price = c.Game.Price,
                    OriginalPrice = c.Game.OriginalPrice,
                    Discount = c.Game.Discount,
                    AddedAt = c.AddedAt
                })
                .ToListAsync();
        }

        public async Task<CartItemDto> AddToCart(Guid userId, Guid gameId)
        {
            var game = await _context.Games.FindAsync(gameId)
                ?? throw new KeyNotFoundException("Jogo não encontrado.");

            var alreadyInCart = await _context.Cart
                .AnyAsync(c => c.UserId == userId && c.GameId == gameId);

            if (alreadyInCart)
                throw new InvalidOperationException("Jogo já está no carrinho.");

            var alreadyOwned = await _context.UserLibrary
                .AnyAsync(l => l.UserId == userId && l.GameId == gameId);

            if (alreadyOwned)
                throw new InvalidOperationException("Você já possui este jogo.");

            var item = new CartItemEntity
            {
                UserId = userId,
                GameId = gameId,
                AddedAt = DateTime.UtcNow
            };

            _context.Cart.Add(item);
            await _context.SaveChangesAsync();

            return new CartItemDto
            {
                GameId = game.Id,
                Title = game.Title,
                CoverImage = game.CoverImage,
                Price = game.Price,
                OriginalPrice = game.OriginalPrice,
                Discount = game.Discount,
                AddedAt = item.AddedAt
            };
        }

        public async Task<bool> RemoveFromCart(Guid userId, Guid gameId)
        {
            var item = await _context.Cart
                .FirstOrDefaultAsync(c => c.UserId == userId && c.GameId == gameId);

            if (item is null) return false;

            _context.Cart.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task ClearCart(Guid userId)
        {
            var items = await _context.Cart
                .Where(c => c.UserId == userId)
                .ToListAsync();

            _context.Cart.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
