using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly NebulaDbContext _context;

        public WishlistService(NebulaDbContext context)
        {
            _context = context;
        }

        public async Task<List<WishlistItemDto>> GetWishlist(Guid userId)
        {
            return await _context.Wishlist
                .AsNoTracking()
                .Include(w => w.Game)
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistItemDto
                {
                    GameId = w.GameId,
                    Title = w.Game.Title,
                    CoverImage = w.Game.CoverImage,
                    Price = w.Game.Price,
                    OriginalPrice = w.Game.OriginalPrice,
                    Discount = w.Game.Discount,
                    AddedAt = w.AddedAt
                })
                .ToListAsync();
        }

        public async Task<WishlistItemDto> AddToWishlist(Guid userId, Guid gameId)
        {
            var game = await _context.Games.FindAsync(gameId)
                ?? throw new KeyNotFoundException("Jogo não encontrado.");

            var alreadyIn = await _context.Wishlist
                .AnyAsync(w => w.UserId == userId && w.GameId == gameId);

            if (alreadyIn)
                throw new InvalidOperationException("Jogo já está na lista de desejos.");

            var item = new WishlistItemEntity
            {
                UserId = userId,
                GameId = gameId,
                AddedAt = DateTime.UtcNow
            };

            _context.Wishlist.Add(item);
            await _context.SaveChangesAsync();

            return new WishlistItemDto
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

        public async Task<bool> RemoveFromWishlist(Guid userId, Guid gameId)
        {
            var item = await _context.Wishlist
                .FirstOrDefaultAsync(w => w.UserId == userId && w.GameId == gameId);

            if (item is null) return false;

            _context.Wishlist.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
