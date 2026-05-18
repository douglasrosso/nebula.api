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
                .Where(w => w.UserId == userId)
                .Select(w => new WishlistItemDto { GameId = w.GameId })
                .ToListAsync();
        }

        public async Task<WishlistItemDto> AddToWishlist(Guid userId, Guid gameId)
        {
            var exists = await _context.Games.AnyAsync(g => g.Id == gameId);
            if (!exists)
                throw new KeyNotFoundException("Jogo não encontrado.");

            var alreadyIn = await _context.Wishlist
                .AnyAsync(w => w.UserId == userId && w.GameId == gameId);

            if (alreadyIn)
                throw new InvalidOperationException("Jogo já está na lista de desejos.");

            _context.Wishlist.Add(new WishlistItemEntity
            {
                UserId = userId,
                GameId = gameId,
                AddedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return new WishlistItemDto { GameId = gameId };
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
