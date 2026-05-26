using Microsoft.EntityFrameworkCore;
using nebula.api.src.Data;
using nebula.api.src.DTOs;
using nebula.api.src.Entities;

namespace nebula.api.src.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly NebulaDbContext _context;

        public LibraryService(NebulaDbContext context)
        {
            _context = context;
        }

        public async Task<List<LibraryItemDto>> GetLibrary(Guid userId)
        {
            return await _context.UserLibrary
                .AsNoTracking()
                .Include(l => l.Game)
                .ThenInclude(g => g.GameGenres)
                .ThenInclude(gg => gg.Genre)
                .Where(l => l.UserId == userId)
                .Select(l => new LibraryItemDto
                {
                    GameId = l.GameId,
                    Title = l.Game.Title,
                    CoverImage = l.Game.CoverImage,
                    Developer = l.Game.Developer,
                    Genres = l.Game.GameGenres.Select(gg => gg.Genre.Name).ToArray(),
                    Rating = l.Game.Rating,
                    AcquiredAt = l.AcquiredAt
                })
                .ToListAsync();
        }

        public async Task AddToLibrary(Guid userId, Guid gameId, DateTime? acquiredAt = null)
        {
            var alreadyOwned = await _context.UserLibrary
                .AnyAsync(l => l.UserId == userId && l.GameId == gameId);

            if (alreadyOwned) return;

            _context.UserLibrary.Add(new UserLibraryEntity
            {
                UserId = userId,
                GameId = gameId,
                AcquiredAt = acquiredAt ?? DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsInLibrary(Guid userId, Guid gameId)
        {
            return await _context.UserLibrary
                .AnyAsync(l => l.UserId == userId && l.GameId == gameId);
        }

        public async Task<int> CountByUser(Guid userId)
        {
            return await _context.UserLibrary.CountAsync(l => l.UserId == userId);
        }
    }
}
